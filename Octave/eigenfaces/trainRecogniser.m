function sessionData = trainRecogniser(databaseFaces, databaseLabels)
  if(nargin != 0 && nargin != 2)
    usage("trainRecogniser() OR trainRecogniser(databaseFaces, databaseLabels)");
  endif

  # Labels will be a cell array, converted to a matrix here - must be a matrix for peristence
  # TODO: Extract this to the loadYale function
  [sessionData.M, labels] = loadYaleTrainingDatabase("C:/FacialRecognition/data/YaleTrainingDatabase/");
  sessionData.labels = cell2mat(labels);
  
  # If database parameters were supplied
  if(nargin == 2)
    sessionData.M = [sessionData.M, databaseFaces];
    sessionData.labels = [sessionData.labels; databaseLabels];
  endif
  
  # Reduce matrix using mean
  sessionData.averageFace = calculateMean(sessionData.M);
  sessionData.reducedFaces = reduceFaces(sessionData.M,sessionData.averageFace);

  # Get eigenfaces using SVD method
  sessionData.U = getEigenfacesSVD(sessionData.reducedFaces,250); #returns top k eigenvectors

  # Represent each image in terms of the k eignenfaces
  # Find a weight vector for each training set image
  # Wj = Uj' * ReducedFacesi
  sessionData.weights = findWeights(sessionData.reducedFaces, sessionData.U);
endfunction