function weightOfUnknownFace = projectFace(eigenfaces, unknownFace, averageFace)
  % projectFace - project an unknown face into "face space" by calculating its weight
  %
  % Inputs:
  %    eigenfaces - eigenfaces calculated from known facial data
  %    unknownFace - the unknown face to be projected
  %    averageFace - the average face calculated from known facial data
  %
  % Outputs:
  %    weightOfUnknownFace - weight value of the unknown face
  
  % Author: Darren Keane
  % Institute of Technology, Tralee
  % email: darren.m.keane@students.ittralee.ie
  
  if(nargin != 3)
    usage("projectFace(eigenfaces, unknownFace, averageFace)");
  endif
  
  % Reduce the unknown face by subtracting the average face
  unknownFace = unknownFace - averageFace;
  
  % Calculate the weight of the reduced unknown face
  for i=1:columns(eigenfaces)
    weightOfUnknownFace(i, 1) = eigenfaces(:, i)' * unknownFace;  
  endfor
endfunction