function [data,labels] = loadYaleTrainingDatabase(path)
  # Load the Yale database using:
  # Persisted Data -> much quicker, or
  # Raw Image Files -> slower, but required if data files don't exist
  
  if(persistedYaleDatabaseExists())
    [data, labels] = loadYaleTrainingDatabaseFromPersistedData();
  else
    if(nargin != 1)
      usage("loadYaleTrainingDatabase(path)");
    endif
    
    [data, labels] = loadYaleTrainingDatabaseFromImageFiles(path);
  endif
endfunction

function exists = persistedYaleDatabaseExists() 
  imagesExist = exist("C:/FacialRecognition/data/yale_database_images", "file");
  labelsExist = exist("C:/FacialRecognition/data/yale_database_labels", "file");
  
  # Exist function return "2" if a file exists, "0" if it doesn't
  if(imagesExist == 2 && labelsExist == 2)
    exists = true;
  else
    exists = false;
  endif
endfunction