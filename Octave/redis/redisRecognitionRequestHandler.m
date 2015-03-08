function sessionData = redisRecognitionRequestHandler(sessionData)
  if(nargin != 1)
    usage("redisRecognitionRequestHandler(sessionData)");
  endif

  faceAsString = sessionData.requestData; #format "1,2,3.......,32256"
  faceSplit = strsplit(faceAsString,",");
  face = [];

  for i=1:columns(faceSplit)
    curPixel = faceSplit{i}; #char: 123
    curPixel = str2double(curPixel);
    face = [face;curPixel]; #construct column vector of facial image
  endfor
  
  #Result is face label
  result = classifyAnUnknownFace(sessionData.U, face, sessionData.averageFace, sessionData.labels)
  
  sessionData.responseCode = 100;
  sessionData.responseData = result;
endfunction