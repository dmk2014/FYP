function result = calculateMean(M)
  if(nargin != 1)
    usage("calculateMean(matrix)");
  endif
  
  if(ismatrix(M))
    result = [];

    for i=1:rows(M)
      row = M(i,:);
      rowMean = mean(row);
      result = [result; rowMean];
    endfor
  else
    error("calculateMean: expecting matrix argument");
  endif
  
  clear("row","rowMean");
endfunction