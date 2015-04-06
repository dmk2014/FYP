function sessionData = redisRequestHandler(R, sessionData)
  if(nargin != 2)
    usage("redisRequestHandler(R, sessionData)");
  endif
  
  # Reference globals that will be used
  global REQUEST_REC;
  global REQUEST_RELOAD;
  global REQUEST_SAVE;
  global REQUEST_RETRAIN;
  global RESPONSE_OK;
  global RESPONSE_FAIL;
  
  # Determine the type of request received using its code
  # Attempt to execute the requested action
  # A response is sent once the action completes, whether it is successful or not
  if(sessionData.requestCode == REQUEST_REC)
  
    # Attempt to recognise an unknown face
    # Pass the sessionData to the dedicated recognition request handler
    try
      sessionData = redisRecognitionRequestHandler(sessionData);
      redisSendResponse(R, sessionData.responseCode, sessionData.responseData);
    catch
      redisSendResponse(R, RESPONSE_FAIL, "Octave: facial recognition failed with an exception");
      disp(lasterror.message);
    end_try_catch
    
  elseif (sessionData.requestCode == REQUEST_RELOAD)
  
    # Reload all data from disk
    try
      clear("sessionData");
      sessionData = loadSession();
      redisSendResponse(R, RESPONSE_OK, "Octave: reload session success");
      disp("Response Sent: 100...reload succeeded");
    catch
      redisSendResponse(R, RESPONSE_FAIL, "Octave: load session failed");
      disp(lasterror.message);
    end_try_catch
    
  elseif (sessionData.requestCode == REQUEST_SAVE)
  
    # Save all session data to disk
    try
      saveSession(sessionData);
      redisSendResponse(R, RESPONSE_OK, "Octave: save session success");
      disp("Response Sent: 100...recogniser data saved successfully");
    catch
      redisSendResponse(R, RESPONSE_FAIL, "Octave: save session failed");
      disp(lasterror.message);
    end_try_catch
  
  elseif (sessionData.requestCode == REQUEST_RETRAIN)  
  
    # Retrain the database
    # Call dedicated retrain handler that will read all data from Redis
    # and pass it to the trainRecogniser function
    try
      clear("sessionData");
      sessionData = redisRetrainRequestHandler(R);
      redisSendResponse(R, RESPONSE_OK, "Octave: retraining recogniser success");
      disp("Response Sent: 100...retrain completed successfully");
    catch
      redisSendResponse(R, RESPONSE_FAIL, "Octave: retraining recogniser failed");
      disp("Response sent: 200...retrain encountered an error and failed");
      disp(lasterror.message);
    end_try_catch
    
  else
    # An invalid request code was received
    redisSendResponse(R, RESPONSE_FAIL, "Octave: invalid request");
    disp("Response Sent: 100...the received request code was invalid");
  endif
  
  disp("Request Parsed, Handled & Response Sent");
endfunction