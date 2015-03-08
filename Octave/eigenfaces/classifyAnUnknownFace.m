function result = classifyAnUnknownFace(U,face,averageFace,faceLabels)
  if(nargin != 3)
    usage("classifyAnUnknownFace(U, unknownFace, averageFace, faceLabels");
  endif
  
  weightOfUnknownFace = projectFace(sessionData.U,face,sessionData.averageFace);  
  idxOfClosestMatch = nearestMatchEuclideanDistance(sessionData.weights, weightOfUnknownFace)
  
  result = faceLabels(idxOfClosestMatch);
endfunction