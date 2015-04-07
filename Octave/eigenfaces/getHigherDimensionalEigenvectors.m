% Select K eigenvectors
% They must be in the original dimensionality, i.e 32256 x 2432
% Ui = MVi
% M "into" Vi will give corresponding eigenvector in higher dimensional space
% where: Ui is eigenvector in higher dimensional space
%        Vi is eigenvector in lower dimensional space
%        M is face set

function U = getHigherDimensionalEigenvectors(V, M, k)
  % args: V = lower dimensional eigenvectors
  %       M = original set of faces OR reduced faces //TODO: Verify this step
  %       k = number of eigenvectors to retrieve
  if(nargin != 3)
    usage("calculateMean(lower dimensional eigenvectors, original face set, number of eigenvectors to retrieve)");
  endif
  
  if(k > columns(V))
    error("getHigherDimensionalEigenvectors: k must be less than the number of faces in the set");
  else
    for i=1:k
      U(:,i) = M * V(:,i); % Debug and verify
    endfor
  endif 
endfunction