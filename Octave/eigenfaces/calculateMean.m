function result = calculateMean(M)
  % calculateMean - find a column vector representing the average face in M
  %
  % Inputs:
  %    M - the matrix
  %
  % Outputs:
  %    result - the average face

  % Author: Darren Keane
  % Institute of Technology, Tralee
  % email: darren.m.keane@students.ittralee.ie
  
  if(nargin != 1)
    usage("calculateMean(matrix)");
  endif
  
  if(ismatrix(M))
    result = [];

    for i=1:rows(M)
      row = M(i, :);
      rowMean = mean(row);
      result = [result; rowMean];
    endfor
  else
    error("calculateMean: expecting matrix argument");
  endif
endfunction