function U = getHigherDimensionalEigenvectors(V, M, k)
  #args: V = lower dimensional eigenvectors
  #      M = original set of faces
  #      k = number of eigenvectors to retrieve
  if(nargin != 3)
    usage("calculateMean(lower dimensional eigenvectors, original face set, number of eigenvectors to retrieve)");
  endif
  
  if(k > columns(V))
    error("getHigherDimensionalEigenvectors: k must be less than the number of faces in the set");
  else
    for i=1:k
      Ui = M * V(:i);
      U = U(i,Ui);
    endfor
  endif 
  
  clear("i","Ui");
endfunction