#weights = [wieghti, weighti,........] where i <= M

#for j=1:30
#  weight = U(:,j)' * reducedFaces(:,i);
#  weights = [weights,weight];
#endfor

#face1 = (W1 * U1) + (W1 * U2) .... + (W1 * Uj)
#Wj = Uj' * facei

function weights = findWeights(reducedFaces, U)
  weights = [];
  weightsForFaceI = [];
  reducedFaces = double(reducedFaces);

  for i=1:columns(reducedFaces)
    #i = currentFace;
    #j = currentEigenvector;
    currentFace = reducedFaces(:,i);
  
    for j=1:columns(U)
      #dot product between the image and each eigenface
      #weight = U(:,j)' * reducedFaces(:,i);
      #face = [face; (weight * U(:,j))];
      #face = [face,weight];
      
      currentEigenvector = U(:,j);
      
      #weight = reducedFaces(:,i) * U(:,j)';
      weight = currentEigenvector' * currentFace;
      weightsForFaceI = [weightsForFaceI,weight];
    endfor
    
    weightsForFaceI = reshape(weightsForFaceI,rows(weightsForFaceI) * columns(weightsForFaceI),1);
    weights = [weights,weightsForFaceI];
    weightsForFaceI = [];
  endfor
endfunction