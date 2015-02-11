function result = getEigenfacesSVD(M,k)
  #SVD is an alternate method of performing PCA
  #Results much more accurate than original algorithm

  #Higher dimensional eigenvectors returned in U
  #Eigenvectors, in this case, are ordered by default
  [U S V] = svd(M,"econ");
  
  #Select top k eigenfaces to keep
  U(:,k);
endfunction