function reducedFaces = reduceFaces(M, averageFace)
  % reduceFaces - reduce a matrix of faces by subtracting the average face
  %
  % Inputs:
  %    M - matrix of faces to be reduced
  %    averageFace - the average face as calculated for M
  %
  % Outputs:
  %    reducedFaces - matrix of reduced faces
  
  % Author: Darren Keane
  % Institute of Technology, Tralee
  % email: darren.m.keane@students.ittralee.ie
  
  if(nargin != 2)
    usage("reduceFaces(M, averageFace)");
  endif
  
  for i=1:columns(M)
    reducedFaces(:, i) = M(:, i) .- averageFace;
  endfor
endfunction