#C = A * A' will fail, ridiculously inefficient
#Trying to calculate n^2 * n^2 matrix = 32256*32256
#Solution is to use an MxM numbers, where M is number of training images (2432), or C = A' * A
#Octave's cov() function will handle this process for us

function [reducedFaces, C] = reductionAndCovariance(M,averageFace)
  reducedFaces = M - averageFace;
  C = cov(reducedFaces);
endfunction