function redisListener(R)
  #R = redisConnection
  global NO_REQUEST = 0;
  global REQUEST_REC = 100;
  global REQUEST_RELOAD = 200;
  global REQUEST_SAVE = 300;
  global RESPONSE_OK = 100;
  global REPONSE_FAIL = 200;
  
  while (true)
    request = redisGet(R,"facial.request.code");
    
    if(!request == NO_REQUEST)
      parseRequest(R,request);
      
      ###
      #Work is done, so clear request for use before next loop
      redisSet(R,"facial.request.code","0");
    endif
    
    pause = "p";
    #Do work
    #Send response -> facial.response
  endwhile
endfunction


function parseRequest(R,request)
  data = redisGet(R,"facial.request.data");
  #parse request
  #decide what to do
  if(request == REQUEST_REC)
    #recognise a face
  elseif (request == REQUEST_RELOAD)
    #reload all data
  elseif (request == REQUEST_SAVE)
    #save all data to disk
    try
      saveSession
      sendResponse(R,"100","Octave: save session success");
    catch
      sendResponse(R,"200","Octave: save session failed");
  endif
endfunction


function sendResponse(R,code,data)
  redisSet("facial.response.code",code);
  redisSet("facial.response.data",data);
endfunction