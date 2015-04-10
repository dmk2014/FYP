function redisListener(R)
  if(nargin != 1)
    usage("redisListener(redisConnection)");
  endif
  
  % Reference globals that will be used
  global NoData;
  global RequestReload;
  global RecogniserAvailable;
  global RecogniserBusy;
  
  % Ensure initial setup of request code & data keys
  % Infinite loop occurs if keys do not exist in Redis
  redisSet(R, "facial.request.code", RequestReload);
  redisSet(R, "facial.request.data", "NULL");
  done = false;
  
  while(!done)
    requestCode = redisGet(R, "facial.request.code");
    
    if(requestCode != NoData)
      redisSet(R, "facial.recogniser.status", RecogniserBusy);
      printf("Request Received: %d", requestCode);
      
      % Store the Redis request code and data
      sessionData.requestCode = requestCode;
      sessionData.requestData = redisGet(R, "facial.request.data");
      
      % Pass to request to the handler. It will be executed and a
      % response will be sent
      sessionData = redisRequestHandler(R, sessionData);
      
      % Prepare Redis to receive new requests
      redisSet(R, "facial.request.code", NoData);
      redisSet(R, "facial.recogniser.status", RecogniserAvailable);
      disp("Recogniser prepared to accept new request...\n");
    endif
    
    % Wait 100 milliseconds before checking for a new request
    usleep(100);
  endwhile
endfunction