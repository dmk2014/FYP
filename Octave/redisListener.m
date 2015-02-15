function redisListener(R)
  #R = redisConnection
  #Load globals
  global NO_REQUEST;
  global REQUEST_REC;
  global REQUEST_RELOAD;
  global REQUEST_SAVE;
  global RESPONSE_OK;
  global REPONSE_FAIL;

  sentinel = 0;
  
  while (sentinel == 0)
    request = redisGet(R,"facial.request.code");
    
    if(!request == NO_REQUEST)
      disp("Request Received: "), disp(request);
      #pause(1);
      
      #Do work
      #Send response -> facial.response
      
      redisRequestHandler(R,request);
      
      #Work is done, so clear request for use before next loop
      redisSet(R,"facial.request.code","0");
      disp("Request Code Reset to 0");
    endif
    
    usleep(100); #sleep 100 millisecs
    
  endwhile
endfunction