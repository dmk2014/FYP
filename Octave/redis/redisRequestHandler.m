function recogniserData = redisRequestHandler(R, recogniserData)
  % redisRequestHandler - handles all Redis requests
  %
  % Inputs:
  %    R - the redisConnection on which the request was received
  %    recogniserData - struct containing the recognisers required data
  %
  % Outputs:
  %    recogniserData - struct containing the recognisers required data, updated with request handling results

  % Author: Darren Keane
  % Institute of Technology, Tralee
  % email: darren.m.keane@students.ittralee.ie
  
  if(nargin != 2)
    usage("redisRequestHandler(R, recogniserData)");
  endif
  
  % Reference globals that will be used
  global RequestRecognition;
  global RequestReload;
  global RequestSave;
  global RequestRetrain;
  global ResponseOK;
  global ResponseFail;
  
  % Determine the type of request received using its code
  % Attempt to execute the requested action
  % A response is sent once the action completes, whether it is successful or not
  
  disp("Begin handling of recogniser request...");
  
  if(recogniserData.requestCode == RequestRecognition)
  
    % Attempt to recognise an unknown face
    % Pass the sessionData to the dedicated recognition request handler
    try
      recogniserData = redisRecognitionRequestHandler(recogniserData);
      redisSendResponse(R, recogniserData.responseCode, recogniserData.responseData);
      disp("Response Sent: Recognition completed without error");
    catch
      redisSendResponse(R, ResponseFail, "Octave: facial recognition failed with an exception");
      disp(lasterror.message);
    end_try_catch
    
  elseif (recogniserData.requestCode == RequestReload)
  
    % Reload all data from disk
    try
      clear("sessionData");
      recogniserData = loadSession();
      redisSendResponse(R, ResponseOK, "Octave: reload session success");
      disp("Response Sent: 100...reload succeeded");
    catch
      redisSendResponse(R, ResponseFail, "Octave: load session failed");
      disp("Response sent: 200...load session encountered an error and failed");
      disp(lasterror.message);
    end_try_catch
    
  elseif (recogniserData.requestCode == RequestSave)
  
    % Save all session data to disk
    try
      saveSession(recogniserData);
      redisSendResponse(R, ResponseOK, "Octave: save session success");
      disp("Response Sent: 100...recogniser data saved successfully");
    catch
      redisSendResponse(R, ResponseFail, "Octave: save session failed");
      disp("Response sent: 200...save session encountered an error and failed");
      disp(lasterror.message);
    end_try_catch
  
  elseif (recogniserData.requestCode == RequestRetrain)  
  
    % Retrain the database
    % Call dedicated retrain handler that will read all data from Redis
    % and pass it to the trainRecogniser function
    try
      clear("sessionData");
      recogniserData = redisRetrainRequestHandler(R);
      redisSendResponse(R, ResponseOK, "Octave: retraining recogniser success");
      disp("Response Sent: 100...retrain completed successfully");
    catch
      redisSendResponse(R, ResponseFail, "Octave: retraining recogniser failed");
      disp("Response sent: 200...retrain encountered an error and failed");
      disp(lasterror.message);
    end_try_catch
    
  else
    % An invalid request code was received
    redisSendResponse(R, ResponseFail, "Octave: invalid request");
    disp("Response Sent: 100...the received request code was invalid");
  endif
  
  disp("Request handling completed...");
endfunction