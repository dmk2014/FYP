function sessionData = trainRecogniser(databaseFaces, databaseLabels)
  if(nargin != 0 && nargin != 2)
    usage("trainRecogniser() OR trainRecogniser(databaseFaces, databaseLabels)");
  endif
  
  disp("Train Recogniser beginning - this will take some time");
  
  # Labels will be a cell array, converted to a matrix here - must be a matrix for peristence
  # TODO: Extract this to the loadYale function
  disp("Loading Yale Database...");
  tic;
  
  [sessionData.M, labels] = loadYaleTrainingDatabase("C:/FacialRecognition/data/YaleTrainingDatabase/");
  sessionData.labels = cell2mat(labels);
  
  # If database parameters were supplied
  if(nargin == 2)
    disp("Merging Yale and database contents...");
    sessionData.M = [sessionData.M, databaseFaces];
    sessionData.labels = [sessionData.labels; databaseLabels];
  endif
  
  # Reduce matrix using mean
  disp("Calculating mean and reducing data...");
  sessionData.averageFace = calculateMean(sessionData.M);
  sessionData.reducedFaces = reduceFaces(sessionData.M,sessionData.averageFace);

  # Get eigenfaces using SVD method
  disp("Calculating top 250 eigenfaces...");
  sessionData.U = getEigenfacesSVD(sessionData.reducedFaces,250); #returns top k eigenvectors

  # Represent each image in terms of the k eignenfaces
  # Find a weight vector for each training set image
  # Wj = Uj' * ReducedFacesi
  disp("Finding weights...");
  sessionData.weights = findWeights(sessionData.reducedFaces, sessionData.U);
  
  printf("Train Recogniser COMPLETED. Time Taken: %.2f seconds\n", toc)
endfunction