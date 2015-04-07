function U = getEigenfacesSVD(M,k)
  if(nargin != 2)
    usage("getEigenfacesSVD(M, k)");
  endif
  
  % SVD is an alternate method of performing PCA
  % Results much more accurate than original algorithm

  % Higher dimensional eigenvectors returned in U
  % The eigenvectors are in an ordered state by default
  [U S V] = svd(M,"econ");
  
  % Select top k eigenfaces to keep
  U = U(:,1:k);
endfunction