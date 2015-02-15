function redisRequestHandler(R,request)
  #Load globals
  global NO_REQUEST;
  global REQUEST_REC;
  global REQUEST_RELOAD;
  global REQUEST_SAVE;
  global RESPONSE_OK;
  global REPONSE_FAIL;
  
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
  endif
  
  disp("Request Parsed & Response Sent");
endfunction