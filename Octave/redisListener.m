function redisListener(R)
  #R = redisConnection
  #Load globals 
  #redisGlobals
  global NO_REQUEST = 50;
  global REQUEST_REC = 100;
  global REQUEST_RELOAD = 200;
  global REQUEST_SAVE = 300;
  global RESPONSE_OK = 100;
  global RESPONSE_FAIL = 200;

  sentinel = 0;
  
  while (sentinel == 0)
    request = redisGet(R,"facial.request.code");
    
    if(request != NO_REQUEST)
      out = strcat("Request Received: ", num2str(request));
      disp(out);
      
      #Do work
      #Send response -> facial.response
      
      redisRequestHandler(R,request);
      
      #Work is done, so clear request for use before next loop
      redisSet(R,"facial.request.code","50");
      disp("Request Code Reset to 0");
    endif
    
    usleep(100); #sleep 100 millisecs
    
  endwhile
endfunction