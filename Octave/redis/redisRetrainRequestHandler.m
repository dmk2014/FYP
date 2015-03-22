function sessionData = redisRetrainRequestHandler(redisConnection)
  if(nargin != 1)
    usage("redisRetrainRequestHandler(redisConnection)");
  endif
  
  # Constants
  redisListLabels = "facial.database.labels";
  redisListData = "facial.database.data";
  noRemainingData = "50";
  
  # Read all data from the cache
  faceLabels = {};
  faces = [];
  done = false;
  
  disp("Reading database contents from the cache...");
  
  while(!done)
    label = redisListLPOP(redisConnection, redisListLabels);
    
    # strcmp returns 1 (or true) if strings are equals
    dataRemaining = strcmp(label, noRemainingData);
    
    if (!dataRemaining)
      faceLabels = [faceLabels; label];
      
      # Pop the facial data from Redis, unmarshal it, and add it to the face array
      faceData = redisListLPOP(redisConnection, redisListData);
      face = redisUnmarshalFacialData(faceData);
      faces = [faces, face];
    else
      done = true;
    endif
  endwhile
  
  # Convert labels cell array to a matrix - required for data persistence
  faceLabels = cell2mat(faceLabels);
  disp("Reading database contents...DONE");
  
  # Pass data and labels acquired from Redis to the trainRecogniser function
  sessionData = trainRecogniser(faces, faceLabels);
endfunction