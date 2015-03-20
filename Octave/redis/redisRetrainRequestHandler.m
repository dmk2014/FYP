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
  
  while(!done)
    label = redisListLPOP(redisConnection, redisListLabels);
    
    # strcmp returns 1 (or true) if strings are equals
    dataRemaining = strcmp(label, noRemainingData);
    
    if (!dataRemaining)
      faceLabels = [faceLabels; label];
      
      # TODO
      # Unmarshal the facial data
      faceData = redisListLPOP(redisConnection, redisListData);
      faces = [faces, faceData];
    else
      done = true;
    endif
  endwhile 
  
  sessionData.M = faces;
  sessionData.labels = faceLabels;
  
  # TODO
  # Pass data and labels acquired from Redis to the trainRecogniser function
  # sessionData = trainRecogniser();
endfunction