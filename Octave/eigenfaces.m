tic;

#M = loadYaleTrainingDatabase("C:/Users/t00157675/Desktop/fyp/FYP/YaleTrainingDatabase/");
M = loadYaleTrainingDatabase("~/Desktop/FYP/YaleTrainingDatabase/");

#Reduce matrix using mean
averageFace = calculateMean(M);
reducedFaces = reduceFaces(M,averageFace);

#Get eigenfaces using SVD method
U = getEigenfacesSVD(reducedFaces,250); #returns top k eigenvectors

#Represent each image in terms of the k eignenfaces
#Find a weight vector for each training set image
#Wj = Uj' * ReducedFacesi
weights = findWeights(reducedFaces, U);

timeElapsed = toc;