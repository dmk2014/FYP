#M = loadYaleTrainingDatabase("C:/Users/t00157675/Desktop/fyp/FYP/YaleTrainingDatabase/");
M = loadYaleTrainingDatabase("~/Desktop/FYP/YaleTrainingDatabase/");

#Reduce matrix using mean
averageFace = calculateMean(M);
reducedFaces = reduceFaces(M,averageFace);

#SVD is an alternate method of performing PCA
#Results much more accurate than original algorithm
[U S V] = svd(reducedFaces,"econ");

U = U(:,1:150); #Select first 50 eigenfaces

#Represent each image in terms of the k eignenfaces
#Find a weight vector for each training set image
#Wj = Uj' * ReducedFacesi
weights = findWeights(reducedFaces, U);