function reducedFaces = reductionFaces(M,averageFace)
  for i=1:columns(M)
    reducedFaces(:,i) = M(:,i) - averageFace;
  endfor
endfunction