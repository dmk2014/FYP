#Reduce matrix using mean
reducedFaces = M - averageValues;

#Calculate covariance matrix
C = cov(reducedFaces);

#C = A * A' will fail, ridiculously inefficient
#Trying to calculate n^2 * n^2 matrix = 32256*32256
#Solution is to use an MxM numbers, where M is number of training images (2432), or C = A' * A
#Octave's cov() function will handle this process for us

#Next step is PCA - calculates Eigenvectors and Eigenvalues
#Achieved via decomposition of the covariance matrix C
[V,D] = eig(C);

#where
#V is eigenvectors
#D is diagonal matrix of eigenvalues of C

#D(i,i) is the eigenvalue relative to V(:,i)
#D(1,1) for V(:,1) -> every row for 1st column, so entire first column

#Sort the columns of the eigenvector matrix V and eigenvalue matrix D in order of decreasing eigenvalue
[D,i] = sort(diag(D), "descend");
V = V(:,i);