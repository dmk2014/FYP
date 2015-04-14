function weights = findWeights(reducedFaces, eigenfaces)
  % findWeights - calculates the weight values for each face in the face set
  %
  % Inputs:
  %    reducedFaces - the reduced set of facial images
  %    eigenfaces - the eigenfaces calculated from the reduced faces
  %
  % Outputs:
  %    weights - the weights for each facial image in the set

  % Author: Darren Keane
  % Institute of Technology, Tralee
  % email: darren.m.keane@students.ittralee.ie
  
  if(nargin != 2)
    usage("findWeights(reducedFaces, eigenfaces");
  endif
  
  % For-each facial image...
  for i=1:columns(reducedFaces)
    currentFace = reducedFaces(:, i);
    
    % Calculate the weight each eigenface face has on the image
    for j=1:columns(eigenfaces)
      currentEigenvector = eigenfaces(:, j);
      
      % Weight value is the dot product between the facial image and the eigenface
      weight = dot(currentFace, currentEigenvector);
      
      % Store the calculated weight value
      weights(j, i) = weight;
    endfor
  endfor
endfunction