#weights = [wieghti, weighti,........] where i <= M

#for j=1:30
#  weight = U(:,j)' * reducedFaces(:,i);
#  weights = [weights,weight];
#endfor

#face1 = (W1 * U1) + (W1 * U2) .... + (W1 * Uj)
#Wj = Uj' * facei

function weights = findWeights(reducedFaces, U)
  weights = [];
  face = [];
  reducedFaces = double(reducedFaces);

  for i=1:columns(reducedFaces)
    #i = currentFace;
    #j = currentEigenvector;
  
    for j=1:columns(U)
      weight = U(:,j)' * reducedFaces(:,i);
      #face = [face; (weight * U(:,j))];
      face = [face,weight];
    endfor
    
    x = columns(U);
    face = reshape(face,columns(U),1);
    weights = [weights,face];
  endfor
endfunction