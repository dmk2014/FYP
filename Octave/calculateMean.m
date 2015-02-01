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

#Temporarily Persist Data
#averageFace = reshape(averageValues,192,168);
#averageFace = uint8(averageFace);
#imwrite(averageFace,"~/Desktop/FYP/Octave/averageFace.png","png");