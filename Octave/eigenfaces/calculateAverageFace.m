function result = calculateAverageFace(M)
  % calculateAverageFace - finds the average face in a matrix of faces
  %
  % Inputs:
  %    M - the matrix of faces, where each face is a column vector
  %
  % Outputs:
  %    result - the average face

  % Author: Darren Keane
  % Institute of Technology, Tralee
  % email: darren.m.keane@students.ittralee.ie
  
  if(nargin != 1)
    usage("calculateAverageFace(matrix)");
  endif
  
  if(ismatrix(M))
    result = [];

    for i=1:rows(M)
      row = M(i, :);
      rowMean = mean(row);
      result = [result; rowMean];
    endfor
  else
    error("calculateAverageFace: expecting matrix argument");
  endif
endfunction