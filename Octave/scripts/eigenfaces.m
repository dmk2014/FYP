tic;

sessionData.M = loadYaleTrainingDatabase("C:/FacialRecognition/FYP/YaleTrainingDatabase/");

#Reduce matrix using mean
sessionData.averageFace = calculateMean(sessionData.M.data);
sessionData.reducedFaces = reduceFaces(sessionData.M.data,sessionData.averageFace);

#Get eigenfaces using SVD method
sessionData.U = getEigenfacesSVD(sessionData.reducedFaces,250); #returns top k eigenvectors

#Represent each image in terms of the k eignenfaces
#Find a weight vector for each training set image
#Wj = Uj' * ReducedFacesi
sessionData.weights = findWeights(sessionData.reducedFaces, sessionData.U);

timeElapsed = toc;