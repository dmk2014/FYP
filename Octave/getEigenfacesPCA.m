function U = getEigenfacesPCA(M,k)
  #code maintained for prosperity
  #eigenfaces as described by Turk & Pentland
  #values returned are unstable and do not give accurate results
  
  #Calculate covariance matrix
  #C = A * A' will fail, ridiculously inefficient
  #Trying to calculate n^2 * n^2 matrix = 32256*32256
  #Solution is to use an MxM numbers, where M is number of training images (2432), or C = A' * A
  #Octave's cov() function will handle this process for us
  C = cov(M);

  #Next step is PCA - calculates Eigenvectors and Eigenvalues
  #Achieved via decomposition of the covariance matrix C
  #where
  #V is eigenvectors
  #D is diagonal matrix of eigenvalues of C

  #D(i,i) is the eigenvalue relative to V(:,i)
  #D(1,1) for V(:,1) -> every row for 1st column, so entire first column
  [V D] = eig(C);

  #Sort the columns of the eigenvector matrix V and eigenvalue matrix D in order of decreasing eigenvalue
  [D,i] = sort(diag(D), 'descend');
  V = V(:,i);
  
  #Retrieve k higher dimensional eigenvectors
  U = getHigherDimensionalEigenvectors(V,M,k);
endfunction