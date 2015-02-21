function sessionData = trainRecogniser()
  #M = loadYaleTrainingDatabase("C:/Users/t00157675/Desktop/fyp/FYP/YaleTrainingDatabase/");
  [sessionData.M, labels] = loadYaleTrainingDatabase("~/Desktop/FYP/YaleTrainingDatabase/");

  #Reduce matrix using mean
  sessionData.averageFace = calculateMean(sessionData.M);
  sessionData.reducedFaces = reduceFaces(sessionData.M,sessionData.averageFace);

  #Get eigenfaces using SVD method
  sessionData.U = getEigenfacesSVD(sessionData.reducedFaces,250); #returns top k eigenvectors

  #Represent each image in terms of the k eignenfaces
  #Find a weight vector for each training set image
  #Wj = Uj' * ReducedFacesi
  sessionData.weights = findWeights(sessionData.reducedFaces, sessionData.U);
endfunction