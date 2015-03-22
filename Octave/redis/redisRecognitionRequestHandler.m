function sessionData = redisRecognitionRequestHandler(sessionData)
  if(nargin != 1)
    usage("redisRecognitionRequestHandler(sessionData)");
  endif
  
  # Constants
  RESPONSE_OK = 100;
  RESPONSE_FAIL = 200;
  
  # First, unmarshal the facial data.
  # Then invoke the recognisers classification function which returns
  # the label of the closest match.
  
  try
    face = redisUnmarshalFacialData(sessionData.requestData)
    labelOfClosestMatch = classifyAnUnknownFace(sessionData.U, sessionData.weights, face, sessionData.averageFace, sessionData.labels)
  
    sessionData.responseCode = RESPONSE_OK;
    sessionData.responseData = labelOfClosestMatch;
  catch
    sessionData.responseCode = RESPONSE_FAIL;
    sessionData.responseData = "An error occurred while handling the recognition request";
  end_try_catch
endfunction