#Reduce matrix using mean
reducedFaces = matrixOfColumnVectors - averageValues;

#Calculate covariance matrix
C = cov(reducedFaces);

#C = A * A' will fail, ridiculously inefficient
#Trying to calculate n^2 * n^2 matrix = 32256*32256
#Solution is to use an MxM numbers, where M is number of training images (2432)
#Octave's cov function will handle this process for us

#Next step is PCA to calculate Eigenvectors
#Achieved via decomposition of the covariance matrix C
[V,D] = eig(C);

#where
#V is eigenvectors
#D is diagonal matrix of eigenvalues of C

#Sort the columns of the eigenvector matrix V and eigenvalue matrix D in order of decreasing eigenvalue
