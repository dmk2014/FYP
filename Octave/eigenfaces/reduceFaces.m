function reducedFaces = reduceFaces(M,averageFace)
  if(nargin != 2)
    usage("reduceFaces(M, averageFace)");
  endif
  
  for i=1:columns(M)
    reducedFaces(:,i) = M(:,i) .- averageFace;
  endfor
endfunction