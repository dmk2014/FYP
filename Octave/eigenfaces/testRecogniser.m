function [image idx] = testRecogniser(U,weights,averageFace,facePath)
  if(nargin != 4)
    usage("testRecogniser(U, weights, averageFace, facePath)");
  endif
  
  #Projecting New Face
  image = double(imread(facePath));
  image = reshape(image,rows(image) * columns(image),1);

  weightOfUnknownFace = projectFace(U,image,averageFace);
  idx = nearestMatchEuclideanDistance(weights, weightOfUnknownFace)
endfunction