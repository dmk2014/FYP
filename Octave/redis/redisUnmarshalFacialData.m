function face = redisUnmarshalFacialData(faceData)
  % redisUnmarshalFacialData - unmarshal comma-seperated facial data to a column vector
  %
  % Inputs:
  %    faceData - the face to be unmarshalled as a comma-seperated string
  %
  % Outputs:
  %    face - the face as a column vector

  % Author: Darren Keane
  % Institute of Technology, Tralee
  % email: darren.m.keane@students.ittralee.ie
  
  if(nargin != 1)
    usage("redisUnmarshalFacialData(faceData)");
  endif
  
  % faceData is a string of comma seperated values
  % Each value is a pixel
  % Convert each pixel value to a double and store the result in a column vector
  
  pixelValues = strsplit(faceData, ",");
  face = [];

  for i=1:columns(pixelValues)
    curPixel = pixelValues{i};
    curPixel = str2double(curPixel);
    face = [face; curPixel];
  endfor
  
  % Ensure that the unmarshalled facial data is in the correct range
  % Each face used by the recogniser must have pixel values in the range [0-255]
  face = normalize(face, 0, 255);
endfunction