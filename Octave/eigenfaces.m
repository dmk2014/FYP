#Load database
#M = loadYaleTrainingDatabase("C:/Users/t00157675/Desktop/fyp/FYP/YaleTrainingDatabase/");
M = loadYaleTrainingDatabase("~/Desktop/FYP/YaleTrainingDatabase/");

#Reduce matrix using mean
#averageFace = calculateMean(M);
averageFace = mean(M,2); 
reducedFaces = reduceFaces(M,averageFace);

#Calculate covariance matrix
#C = A * A' will fail, ridiculously inefficient
#Trying to calculate n^2 * n^2 matrix = 32256*32256
#Solution is to use an MxM numbers, where M is number of training images (2432), or C = A' * A
#Octave's cov() function will handle this process for us
C = cov(reducedFaces);

#Next step is PCA - calculates Eigenvectors and Eigenvalues
#Achieved via decomposition of the covariance matrix C
#where
#V is eigenvectors
#D is diagonal matrix of eigenvalues of C

#D(i,i) is the eigenvalue relative to V(:,i)
#D(1,1) for V(:,1) -> every row for 1st column, so entire first column
[V,D] = eig(C);

#Sort the columns of the eigenvector matrix V and eigenvalue matrix D in order of decreasing eigenvalue
[D,i] = sort(diag(D), 'descend');
V = V(:,i);

#for h=1:columns(V)
#  V(:,h) = normalize(V(:,h),0,255);
#endfor

######
#[uselessVariable,permutation]=sort(diag(D),'descend');
#D=D(permutation,permutation);
#V=V(:,permutation);
######

#Retrieve k higher dimensional eigenvectors & normalize data [0-255]
U = getHigherDimensionalEigenvectors(V,reducedFaces,100);

for h=1:columns(U)
  U(:,h) = normalize(U(:,h),0,255);
endfor

#Represent each image in terms of the k eignenfaces
#Find a weight vector for each training set image
#Wj = Uj' * ReducedFacesi
weights = findWeights(reducedFaces, U);

#Normalize weights
for h=1:columns(weights)
  weights(:,h) = normalize(weights(:,h),0,255);
endfor