function [data, labels] = loadYaleTrainingDatabase(path)
  % loadYaleTrainingDatabase - load the Yale training database
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
  
  if(persistedYaleDatabaseExists())
    % Load Yale database from persisted data
    [data, labels] = loadYaleTrainingDatabaseFromPersistedData();
  else
    if(nargin != 1)
      usage("loadYaleTrainingDatabase(path)");
    endif
    
    % Load Yale database from raw image files
    [data, labels] = loadYaleTrainingDatabaseFromImageFiles(path);
  endif
endfunction

function exists = persistedYaleDatabaseExists()
  % persistedYaleDatabaseExists - check if a persisted Yale training database is on disk
  %
  % Outputs:
  %    exists - boolean indicating if a persisted Yale training database was found

  % Author: Darren Keane
  % Institute of Technology, Tralee
  % email: darren.m.keane@students.ittralee.ie
  
  imagesExist = exist("C:/FacialRecognition/data/yale_database_images", "file");
  labelsExist = exist("C:/FacialRecognition/data/yale_database_labels", "file");
  
  % Exist function return "2" if a file exists, "0" if it doesn't
  if(imagesExist == 2 && labelsExist == 2)
    exists = true;
  else
    exists = false;
  endif
endfunction