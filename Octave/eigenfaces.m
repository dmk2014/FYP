#Load database
#M = loadYaleTrainingDatabase("C:/Users/t00157675/Desktop/fyp/FYP/YaleTrainingDatabase/");
M = loadYaleTrainingDatabase("~/Desktop/FYP/YaleTrainingDatabase/");

#Reduce matrix using mean
averageFace = calculateMean(M);
reducedFaces = M - averageFace;

#Calculate covariance matrix
#C = cov(reducedFaces);
C = reducedFaces' * reducedFaces;

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

#Retrieve k higher dimensional eigenvectors
U = getHigherDimensionalEigenvectors(V,reducedFaces,30);

#Represent each image in terms of the k eignenfaces
#Find a weight vector for each training set image
#Wj = Uj' * ReducedFacesi
weights = findWeights(reducedFaces, U)