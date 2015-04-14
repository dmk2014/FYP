function recogniserData = trainRecogniser(databaseFaces, databaseLabels)
  if(nargin != 0 && nargin != 2)
    usage("trainRecogniser() OR trainRecogniser(databaseFaces, databaseLabels)");
  endif
  
  disp("Train Recogniser beginning - this will take some time");
  disp("Loading Yale Database...");
  tic;
  
  [recogniserData.M, recogniserData.labels] = loadYaleTrainingDatabase("C:/FacialRecognition/data/YaleTrainingDatabase/");
  
  % If database parameters were supplied
  if(nargin == 2)
    disp("Merging Yale and database contents...");
    recogniserData.M = [recogniserData.M, databaseFaces];
    recogniserData.labels = [recogniserData.labels; databaseLabels];
  endif
  
  % Reduce matrix using mean
  disp("Calculating mean and reducing data...");
  recogniserData.averageFace = calculateMean(recogniserData.M);
  recogniserData.reducedFaces = reduceFaces(recogniserData.M, recogniserData.averageFace);

  % Get eigenfaces using SVD method
  disp("Calculating top 250 eigenfaces...");
  recogniserData.U = getEigenfacesSVD(recogniserData.reducedFaces, 250); % returns top k eigenvectors

  % Represent each image in terms of the k eignenfaces
  % Find a weight vector for each training set image
  % Wj = Uj' * ReducedFacesi
  disp("Finding weights...");
  sessionData.weights = findWeights(recogniserData.reducedFaces, recogniserData.U);
  
  printf("Train Recogniser COMPLETED. Time Taken: %.2f seconds\n", toc)
endfunction