#Load database
M = loadYaleTrainingDatabase("path");

#Reduce matrix using mean
averageFace = calculateMean(M);
reducedFaces = M - averageFace;

#Calculate covariance matrix
C = cov(reducedFaces);

#Next step is PCA - calculates Eigenvectors and Eigenvalues
[V,D] = eig(C);

#Sort the columns of the eigenvector matrix V and eigenvalue matrix D in order of decreasing eigenvalue
[D,i] = sort(diag(D), "descend");
V = V(:,i);

#Retrieve k higher dimensional eigenvectors
U = getHigherDimensionalEigenvectors(V,M,30);