function eigenvectors = getHigherDimensionalEigenvectors(lowerEigenvectors, originalData, nEigenvectors)
  % getHigherDimensionalEigenvectors - return lower dimensional eigenvectors to the dimensionality 
  %                                    of the original data
  %
  % Inputs:
  %    lowerEigenvectors - lower dimensional eigenvectors
  %    originalData - original data from which lower dimensional eigenvectors were calculated
  %    nEigenvectors - the number of eigenvectors to retain
  %
  % Outputs:
  %    eigenvectors - a matrix of eigenvectors
  
  % Author: Darren Keane
  % Institute of Technology, Tralee
  % email: darren.m.keane@students.ittralee.ie
  
  if(nargin != 3)
    usage("getHigherDimensionalEigenvectors(lowerEigenvectors, originalData, nEigenvectors)");
  endif
  
  if(nEigenvectors > columns(lowerEigenvectors))
    error("getHigherDimensionalEigenvectors: nEigenvectors must be less than the number of faces in the set");
  endif
  
  % Get higher dimensional eigenvectors
  for i=1:nEigenvectors
    eigenvectors(:, i) = originalData * lowerEigenvectors(:, i);
  endfor
endfunction