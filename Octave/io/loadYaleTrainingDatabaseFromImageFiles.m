function [data, labels] = loadYaleTrainingDatabaseFromImageFiles(path)
  % loadYaleTrainingDatabaseFromImageFiles - load the database from its raw data
  %
  % Inputs:
  %    path - the location of the training database raw image files on disk
  %
  % Outputs:
  %    data - the training database faces
  %    labels - the training database labels

  % Author: Darren Keane
  % Institute of Technology, Tralee
  % email: darren.m.keane@students.ittralee.ie
  
  if(nargin != 1)
    usage("loadYaleTrainingDatabaseFromImageFiles(path)");
  endif
  
  trainingDatabase = readdir(path);
  folderCount = numel(trainingDatabase);
  
  data = [];
  labels = {};

  for i=1:folderCount
    % Skip special files named "." and ".."
    if(regexp(trainingDatabase{i}, "^\\.\\.?$"))
      continue;
    endif
    
    % Set current folder location in form /path/folderLabel/
    currentFolder = [path trainingDatabase{i}];
    
    % Set label for the person whose images we are about to load
    % Label is the folder name, e.g. yaleB01
    currentPersonLabel = trainingDatabase(i,:);
    
    if(isdir(currentFolder))
      % Read all items in the folder
      imgDir = readdir(currentFolder);
      
      for j=1:numel(imgDir)
        % Skip special files named "." and "..", and "_Ambient.pgm"
        if(regexp(imgDir{j}, "^\\.\\.?$") || regexp(imgDir{j}, "_Ambient.pgm"))
          continue;
        endif
        
        % If the current files end with .pgm, it is an image and we will load it
        if(regexp(imgDir{j}, ".pgm"))
          currentImagePath = [currentFolder "/" imgDir{j}];
          img = double(imread(currentImagePath));
        
          % Process the image
          img = reshape(img,rows(img) * columns(img),1);
          data = [data, img];
          labels = [labels; currentPersonLabel];
        endif
      endfor
    endif
  endfor
  
  % Convert the cell array of labels to a matrix - required for persistence
  labels = cell2mat(labels);
  
  % Persist the data for future sessions
  persistData(data, labels);
endfunction

function persistData(images, labels)
  % persistData - persist the Yale database for future recogniser sessions
  %
  % Inputs:
  %    images - the training database faces
  %    labels - the training database labels

  % Author: Darren Keane
  % Institute of Technology, Tralee
  % email: darren.m.keane@students.ittralee.ie
  
  if(nargin != 2)
    usage("persistData(images, labels");
  endif
  
  % Persist the data for future sessions
  imagesFileName = "yale_database_images";
  labelsFileName = "yale_database_labels";
  
  saveMatrixData(images, imagesFileName);
  saveMatrixData(labels, labelsFileName);
endfunction