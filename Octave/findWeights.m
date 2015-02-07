#weights = [weighti, weighti,........] where i <= M

#face1 = (W1 * U1) + (W1 * U2) .... + (W1 * Uj)
#Wj = Uj' * facei

function weights = findWeights(reducedFaces, U)
  weights = [];
  weightsForFaceI = [];

  for i=1:columns(reducedFaces)
    currentFace = reducedFaces(:,i);
  
    for j=1:columns(U)
      #dot product between the image and each eigenface  
      currentEigenvector = U(:,j);
      
      weight = currentEigenvector' * currentFace;
      weightsForFaceI = [weightsForFaceI,weight];
    endfor
    
    weightsForFaceI = reshape(weightsForFaceI,
                              rows(weightsForFaceI)*columns(weightsForFaceI),
                              1);
    
    weights = [weights,weightsForFaceI];
    weightsForFaceI = [];
  endfor
endfunction