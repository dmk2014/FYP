function redisRequestHandler(R,request)
  #Load globals
  global NO_REQUEST = 50;
  global REQUEST_REC = 100;
  global REQUEST_RELOAD = 200;
  global REQUEST_SAVE = 300;
  global RESPONSE_OK = 100;
  global RESPONSE_FAIL = 200;
  
  data = redisGet(R,"facial.request.data");
  #parse request
  #decide what to do
  if(request == REQUEST_REC)
    #recognise a face
    redisSendResponse(R,"200","Octave: not implemented <facial rec>");
  elseif (request == REQUEST_RELOAD)
    #reload all data
    redisSendResponse(R,"200","Octave: not implemented <reload>");
  elseif (request == REQUEST_SAVE)
    #save all data to disk
    try
      saveSession
      redisSendResponse(R,"100","Octave: save session success");
    catch
      redisSendResponse(R,"200","Octave: save session failed");
    end_try_catch
  else
    #invalid request
    redisSendResponse(R,"200","Octave: invalid request");
  endif
  
  disp("Request Parsed & Response Sent");
endfunction