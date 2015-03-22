function face = redisUnmarshalFacialData(faceData)
  if(nargin != 1)
    usage("redisUnmarshalFacialData(faceData)");
  endif
  
  # faceData is a string of comma seperated values.
  # Each value is a pixel.
  # Convert each pixel value to a double and store the result
  # as a column vector.
  
  pixelValues = strsplit(faceData,",");
  face = [];

  for i=1:columns(pixelValues)
    curPixel = pixelValues{i};
    curPixel = str2double(curPixel);
    face = [face;curPixel];
  endfor
endfunction