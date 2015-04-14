function recogniserData = redisRecognitionRequestHandler(recogniserData)
  % redisRecognitionRequestHandler - handles a Redis request to recognise an unknown face
  %
  % Inputs:
  %    recogniserData - struct containing the recognisers required data
  %
  % Outputs:
  %    recogniserData - struct containing the recognisers required data, updated with recogniton results

  % Author: Darren Keane
  % Institute of Technology, Tralee
  % email: darren.m.keane@students.ittralee.ie
  
  if(nargin != 1)
    usage("redisRecognitionRequestHandler(recogniserData)");
  endif
  
  % Reference globals that will be used
  global ResponseOK;
  global ResponseFail;
  
  % First, unmarshal the facial data.
  % Then invoke the recognisers classification function which returns
  % the label of the closest match.
  
  try
    disp("Beginning facial recognition...");
  
    face = redisUnmarshalFacialData(recogniserData.requestData);
    
    labelOfClosestMatch = classifyAnUnknownFace(recogniserData.U,
      recogniserData.weights,
      face,
      recogniserData.averageFace,
      recogniserData.labels);
  
    recogniserData.responseCode = ResponseOK;
    recogniserData.responseData = labelOfClosestMatch;
    
    disp("Facial recognition completed...");
  catch
    recogniserData.responseCode = ResponseFail;
    recogniserData.responseData = "An error occurred while handling the recognition request";
  end_try_catch
endfunction