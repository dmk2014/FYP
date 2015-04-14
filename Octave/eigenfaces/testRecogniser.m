function [image idxClosestMatch] = testRecogniser(eigenfaces, weights, averageFace, facePath)
  % testRecogniser - invoke the recogniser using an image on disk
  %
  % Inputs:
  %    eigenfaces - the calculated eigenfaces
  %    weights - the calculated weights
  %    averageFace - the calculated average face
  %    facePath - path to the file from which to load a facial image
  %
  % Outputs:
  %    image - facial image that was loaded
  %    idxClosestMatch - index of closest match to the loaded facial image
  
  % Author: Darren Keane
  % Institute of Technology, Tralee
  % email: darren.m.keane@students.ittralee.ie
  
  if(nargin != 4)
    usage("testRecogniser(eigenfaces, weights, averageFace, facePath)");
  endif
  
  % NOTE
  % This function was used during implementation to manually test the recogniser
  
  % Load image & convert to column vector
  image = double(imread(facePath));
  image = reshape(image, rows(image) * columns(image), 1);
  
  % Project image & find its closest match
  weightOfUnknownFace = projectFace(eigenfaces, image, averageFace);
  idxClosestMatch = nearestMatchEuclideanDistance(weights, weightOfUnknownFace)
endfunction