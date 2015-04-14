function recogniserData = trainRecogniser(databaseFaces, databaseLabels)
  % trainRecogniser - train the facial recognition system
  %
  % Inputs:
  %    databaseFaces - faces retrieved from the facial database
  %    databaseLabels - associated labels for the database faces
  %
  % Outputs:
  %    recogniserData - struct containing all data required for the recogniser to function
  
  % Author: Darren Keane
  % Institute of Technology, Tralee
  % email: darren.m.keane@students.ittralee.ie
  
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
  recogniserData.averageFace = calculateAverageFace(recogniserData.M);
  recogniserData.reducedFaces = reduceFaces(recogniserData.M, recogniserData.averageFace);

  % Get eigenfaces using SVD method
  disp("Calculating top 250 eigenfaces...");
  recogniserData.U = getEigenfacesSVD(recogniserData.reducedFaces, 250); % Returns top k eigenfaces

  % Calculate weights for each image in the training set
  disp("Finding weights...");
  sessionData.weights = findWeights(recogniserData.reducedFaces, recogniserData.U);
  
  printf("Train Recogniser COMPLETED. Time Taken: %.2f seconds\n", toc)
endfunction