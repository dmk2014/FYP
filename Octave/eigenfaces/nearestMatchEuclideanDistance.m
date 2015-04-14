function idxNearestMatch = nearestMatchEuclideanDistance(weights, weightOfUnknownFace)
  % nearestMatchEuclideanDistance - find the closest match to an unknown facial image
  %
  % Inputs:
  %    weights - weight matrix for all known faces
  %    weightOfUnknownFace - weight vector of unknown face
  %
  % Outputs:
  %    idxNearestMatch - index of nearest facial match
  
  % Author: Darren Keane
  % Institute of Technology, Tralee
  % email: darren.m.keane@students.ittralee.ie
  
  if(nargin != 2)
    usage("nearestMatchEuclideanDistance(weights, weightOfUnknownFace");
  endif
  
  % TODO
  % Update this function with threshold value
  
  % Euclidean Distance
  % Loop through weights and find shortest distance from the unknown weight
  % Use 2-norm approach
  for i=1:columns(weights)
    ED(1, i) = norm(weights(:, i) - weightOfUnknownFace, 2);
  endfor
  
  [distance idx] = min(ED);
  
  % Return the index of the closest match
  idxNearestMatch = idx;
endfunction