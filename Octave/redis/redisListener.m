function redisListener(R)
  if(nargin != 1)
    usage("redisListener(redisConnection)");
  endif
  
  # Reference globals that will be used
  global NO_REQUEST;
  global REQUEST_REC;
  global REQUEST_RELOAD;
  global REQUEST_SAVE;
  global REQUEST_RETRAIN;
  global RESPONSE_OK;
  global RESPONSE_FAIL;
  
  # Ensure initial setup of request code & data keys
  # Infinite loop occurs if keys do not exist in Redis
  redisSet(R, "facial.request.code", REQUEST_RELOAD);
  redisSet(R, "facial.request.data", "NULL");
  done = false;
  
  while(!done)
    request = redisGet(R, "facial.request.code");
    
    if(request != NO_REQUEST)
      out = strcat("Request Received: ", num2str(request));
      disp(out);
      
      # Store the Redis request code and data
      sessionData.requestCode = request;
      sessionData.requestData = redisGet(R, "facial.request.data");
      
      # Pass to request to the handler. It will be executed and a
      # response will be sent
      sessionData = redisRequestHandler(R, sessionData);
      
      # Reset request code Redis key
      redisSet(R, "facial.request.code", NO_REQUEST);
      disp("Request Code Reset to 50");
    endif
    
    # Wait 100 milliseconds before checking for a new request
    usleep(100);
    
  endwhile
endfunction