function result = classifyAnUnknownFace(eigenfaces, weights, unknownFace, averageFace, faceLabels)
  % classifyAnUnknownFace - find the label of the closest match to an unknown face
  %
  % Inputs:
  %    eigenfaces - the eigenfaces
  %    weights - the weights for all known faces
  %    unknownFace - the unknown face
  %    averageFace - the average face
  %    faceLabels - the labels for all known faces
  %
  % Outputs:
  %    result - the label of the closest match to the unknown face

  % Author: Darren Keane
  % Institute of Technology, Tralee
  % email: darren.m.keane@students.ittralee.ie
  
  if(nargin != 5)
    usage("classifyAnUnknownFace(eigenfaces, weights, unknownFace, averageFace, faceLabels)");
  endif
  
  weightOfUnknownFace = projectFace(eigenfaces, unknownFace, averageFace);  
  idxOfClosestMatch = nearestMatchEuclideanDistance(weights, weightOfUnknownFace);
  
  result = faceLabels(idxOfClosestMatch, :);
endfunction