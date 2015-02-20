function sessionData = redisRequestHandler(R, sessionData)
  #Load globals
  global NO_REQUEST = 50;
  global REQUEST_REC = 100;
  global REQUEST_RELOAD = 200;
  global REQUEST_SAVE = 300;
  global REQUEST_RETRAIN = 400;
  global RESPONSE_OK = 100;
  global RESPONSE_FAIL = 200;
  
  request = sessionData.requestCode;
  data = sessionData.requestData;
  
  #parse request
  #decide what to do
  if(request == REQUEST_REC)
  
    #recognise a face
    redisSendResponse(R,"200","Octave: not implemented <facial rec>");
    
  elseif (request == REQUEST_RELOAD)
  
    #reload all data
    try
      clear("sessionData");
      sessionData = loadSession();
      redisSendResponse(R,"100","Octave: reload session success");
      disp("Response Sent: 100...reload succeeded");
    catch
      redisSendResponse(R,"200","Octave: load session failed");
      disp(lasterror.message);
    end_try_catch
    
  elseif (request == REQUEST_SAVE)
  
    #save all data to disk
    try
      saveSession(sessionData);
      redisSendResponse(R,"100","Octave: save session success");
    catch
      redisSendResponse(R,"200","Octave: save session failed");
      disp(lasterror.message);
    end_try_catch
  
  elseif (request == REQUEST_RETRAIN)  
    
    #reload entire sessionData
    try
      clear("sessionData");
      sessionData = trainRecogniser();
      redisSendResponse(R,"100","Octave: retraining recogniser success");
    catch
      redisSendResponse(R,"200","Octave: retraining recogniser failed");
      disp(lasterror.message);
    end_try_catch
    
  else
    #invalid request
    redisSendResponse(R,"200","Octave: invalid request");
  endif
  
  disp("Request Parsed & Response Sent");
endfunction