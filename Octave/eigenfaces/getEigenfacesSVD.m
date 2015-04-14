function U = getEigenfacesSVD(M, k)
  % getEigenfacesSVD - calculates eigenfaces via a Singular Value Decomposition
  %
  % Inputs:
  %    M - the matrix of facial images from which to calculate eigenfaces
  %    k - the number of eigenfaces to retain
  %
  % Outputs:
  %    U - a matrix of eigenfaces

  % Author: Darren Keane
  % Institute of Technology, Tralee
  % email: darren.m.keane@students.ittralee.ie
  
  if(nargin != 2)
    usage("getEigenfacesSVD(M, k)");
  endif
  
  % SVD is an alternate method of performing PCA
  % Observed results were preferred to those of getEigenfacesPCA()

  % Higher dimensional eigenvectors returned in U
  % These eigenvectors are in an ordered state
  [U S V] = svd(M, "econ");
  
  % Select top k eigenfaces to retain
  U = U(:, 1:k);
endfunction