function sessionData = redisRetrainRequestHandler(redisConnection)
  if(nargin != 1)
    usage("redisRetrainRequestHandler(redisConnection)");
  endif
  
  % Reference globals that will be used
  global DatabaseLabelsKey;
  global DatabaseDataKey;
  global NoData;
  
  % Read all data from the cache
  faceLabels = {};
  faces = [];
  done = false;
  
  disp("Retrieving database contents from Redis...");
  
  while(!done)
    label = redisListLPOP(redisConnection, DatabaseLabelsKey);
    
    % strcmp returns 1 (or true) if strings are equals
    dataRemaining = strcmp(label, NoData);
    
    if (!dataRemaining)
      faceLabels = [faceLabels; label];
      
      % Pop the facial data from Redis, unmarshal it, and add it to the face array
      faceData = redisListLPOP(redisConnection, DatabaseDataKey);
      face = redisUnmarshalFacialData(faceData);
      faces = [faces, face];
    else
      done = true;
    endif
  endwhile
  
  % Convert labels cell array to a matrix - required for data persistence
  faceLabels = cell2mat(faceLabels);
  disp("Retrieving database contents from Redis...DONE");
  
  % Pass data and labels acquired from Redis to the trainRecogniser function
  sessionData = trainRecogniser(faces, faceLabels);
endfunction