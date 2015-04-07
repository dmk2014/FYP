function result = reconstructFace(weights,U,averageFace,i)
  if(nargin != 4)
    usage("reconstructFace(weights, U, averageFace, i)");
  endif
  % reconstructs face i in the training set
  
  % select the weights that were calculated for that face
  faceWeights = weights(:,i); #column vector of dim cols(U) x 1
  
  % multiply the weights for face i by all eigenfaces
  % and then add back the average face
  result = U * faceWeights;
  result = normalize(result,0,255)
  result += averageFace;
endfunction