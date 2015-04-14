function facialImage = reconstructFace(weights, eigenfaces, averageFace, idxToReconstruct)
  % reconstructFace - reconstruct a face using its weights and the eigenfaces
  %
  % Inputs:
  %    weights - weights calculated for the original data set
  %    eigenfaces - eigenfaces calculated for the original data set
  %    averageFace - the average face calculated for the original data set
  %    idxToReconstruct - index of face in the original data set to reconstruct
  %
  % Outputs:
  %    facialImage - reconstructed facial image
  
  % Author: Darren Keane
  % Institute of Technology, Tralee
  % email: darren.m.keane@students.ittralee.ie
  
  if(nargin != 4)
    usage("reconstructFace(weights, eigenfaces, averageFace, idxToReconstruct)");
  endif
  
  % Get the weights that were calculated for the face to be reconstructed
  faceWeights = weights(:, idxToReconstruct);
  
  % Multiply the weights for this face by all eigenfaces
  % Finally, add back the average face
  facialImage = eigenfaces * faceWeights;
  facialImage = normalize(facialImage, 0, 255)
  facialImage += averageFace;
endfunction