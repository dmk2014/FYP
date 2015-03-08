function [image distance idx] = testRecogniser(U,weights,averageFace,facePath)
  if(nargin != 4)
    usage("testRecogniser(U, weights, averageFace, facePath)");
  endif
  
  #Projecting New Face
  image = double(imread(facePath));
  #"~/Desktop/yaleB01_P00A+000E+00.pgm"
  
  image = reshape(image,rows(image) * columns(image),1);

  weightOfUnknownFace = projectFace(U,image,averageFace);

  #Euclidian Distance
  #Loop through weights and find smallest distance
  #ED = norm(W - W, 2); #use 2-norm approach
  
  #ED from between unknown face and each face
  for i=1:columns(weights)
    ED(1,i) = norm(weights(:,i) - weightOfUnknownFace, 2);
  endfor
  
  [distance idx] = min(ED);
endfunction