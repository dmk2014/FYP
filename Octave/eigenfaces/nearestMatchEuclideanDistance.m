function result = nearestMatchEuclideanDistance(weights, weightOfUnknownFace)
  if(nargin != 2)
    usage("nearestMatchEuclideanDistance(weights, weightOfUnknownFace");
  endif
  
  % TODO
  % Update this function with threshold value
  
  % Euclidian Distance
  % Loop through weights and find smallest distance
  % ED = norm(W - W, 2); use 2-norm approach
  % Find ED between unknown face and each face in dataset
  for i=1:columns(weights)
    ED(1,i) = norm(weights(:,i) - weightOfUnknownFace, 2);
  endfor
  
  [distance idx] = min(ED);
  
  % Return the index of the closest match
  result = idx;
endfunction