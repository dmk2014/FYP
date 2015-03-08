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
  
  #Extract to recognition methods
  #This code is duplicated in multiple functions
  weightOfUnknownFace = projectFace(sessionData.U,face,sessionData.averageFace);
  
  for i=1:columns(sessionData.weights)
    ED(1,i) = norm(sessionData.weights(:,i) - weightOfUnknownFace, 2);
  endfor
  
  [distance idx] = min(ED);
  
  sessionData.responseCode = 100;
  sessionData.responseData = idx; #change to face label
  
endfunction