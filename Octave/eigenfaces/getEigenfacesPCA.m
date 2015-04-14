function U = getEigenfacesPCA(M, k)
  % getEigenfacesPCA - calculates eigenfaces using the PCA method described by Turk & Pentland
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
    usage("getEigenfacesPCA(M, k)");
  endif
  
  % Code maintained for posterity
  % Eigenfaces as described by Turk & Pentland
  % Values returned did not give accurate results - see SVD method which was used instead
  
  % Calculate covariance matrix
  % C = A * A' will fail, ridiculously inefficient
  % Trying to calculate n^2 * n^2 matrix = 32256*32256
  % Solution is to use an MxM numbers, where M is number of training images (2432), or C = A' * A
  % Octave's cov() function will handle this process for us
  C = cov(M);

  % Calculate Eigenvectors and Eigenvalues
  % Achieved via decomposition of the covariance matrix C
  % V is eigenvectors
  % D is diagonal matrix of eigenvalues of C

  % D(i, i) is the eigenvalue relative to V(:, i)
  [V D] = eig(C);

  % Sort the columns of the eigenvector matrix V and eigenvalue matrix D in order of decreasing eigenvalue
  [D, i] = sort(diag(D), 'descend');
  V = V(:, i);
  
  % Retrieve k higher dimensional eigenvectors
  U = getHigherDimensionalEigenvectors(V, M, k);
endfunction