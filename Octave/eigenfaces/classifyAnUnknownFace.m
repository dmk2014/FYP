function result = classifyAnUnknownFace(U,weights,face,averageFace,faceLabels)
  if(nargin != 5)
    usage("classifyAnUnknownFace(U, weights, unknownFace, averageFace, faceLabels)");
  endif
  
  weightOfUnknownFace = projectFace(U,face,averageFace);  
  idxOfClosestMatch = nearestMatchEuclideanDistance(weights, weightOfUnknownFace)
  
  result = faceLabels(idxOfClosestMatch,:);
endfunction