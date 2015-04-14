function sessionData = redisRecognitionRequestHandler(sessionData)
  % redisRecognitionRequestHandler - handles a Redis request to recognise an unknown face
  %
  % Inputs:
  %    sessionData - struct containing the recognisers required data
  %
  % Outputs:
  %    sessionData - struct containing the recognisers required data, updated with recogniton results

  % Author: Darren Keane
  % Institute of Technology, Tralee
  % email: darren.m.keane@students.ittralee.ie
  
  if(nargin != 1)
    usage("redisRecognitionRequestHandler(sessionData)");
  endif
  
  % Reference globals that will be used
  global ResponseOK;
  global ResponseFail;
  
  % First, unmarshal the facial data.
  % Then invoke the recognisers classification function which returns
  % the label of the closest match.
  
  try
    disp("Beginning facial recognition...");
  
    face = redisUnmarshalFacialData(sessionData.requestData);
    labelOfClosestMatch = classifyAnUnknownFace(sessionData.U, sessionData.weights, face, sessionData.averageFace, sessionData.labels);
  
    sessionData.responseCode = ResponseOK;
    sessionData.responseData = labelOfClosestMatch;
    
    disp("Facial recognition completed...");
  catch
    sessionData.responseCode = ResponseFail;
    sessionData.responseData = "An error occurred while handling the recognition request";
  end_try_catch
endfunction