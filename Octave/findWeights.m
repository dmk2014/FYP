function weights = findWeights(reducedFaces, U)
  for i=1:columns(reducedFaces)
    currentFace = reducedFaces(:,i);
  
    for j=1:columns(U)
      #dot product between the image and each eigenface  
      currentEigenvector = U(:,j);
      
      #weight = dot(currentEigenvector',currentFace);
      weight = dot(currentFace,currentEigenvector);
      weights(j,i) = weight;
    endfor
  endfor
endfunction