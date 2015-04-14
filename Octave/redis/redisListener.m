function redisListener(R)
  % redisListener - Listens for, and controls execution of, recogniser requests send via Redis
  %
  % Inputs:
  %    R - the redisConnection on which to listen for requests

  % Author: Darren Keane
  % Institute of Technology, Tralee
  % email: darren.m.keane@students.ittralee.ie

  if(nargin != 1)
    usage("redisListener(redisConnection)");
  endif
  
  % Reference globals that will be used
  global RequestCodeKey;
  global RequestDataKey;
  global NoData;
  global RequestReload;
  global RecogniserStatusKey;
  global RecogniserAvailable;
  global RecogniserBusy;
  
  % Ensure initial setup of request code & data keys
  % Infinite loop occurs if keys do not exist in Redis
  redisSet(R, RequestCodeKey, RequestReload);
  redisSet(R, RequestDataKey, NoData);
  done = false;
  
  while(!done)
    requestCode = redisGet(R, RequestCodeKey);
    
    if(requestCode != NoData)
      redisSet(R, RecogniserStatusKey, RecogniserBusy);
      printf("Request Received: %d", requestCode);
      
      % Store the Redis request code and data
      recogniserData.requestCode = requestCode;
      recogniserData.requestData = redisGet(R, RequestDataKey);
      
      % Pass the request to the handler for execution and response
      recogniserData = redisRequestHandler(R, recogniserData);
      
      % Prepare Redis to receive new requests
      redisSet(R, RequestCodeKey, NoData);
      redisSet(R, RecogniserStatusKey, RecogniserAvailable);
      disp("Recogniser prepared to accept new request...\n");
    endif
    
    % Wait 100 milliseconds before checking for a new request
    usleep(100);
  endwhile
endfunction