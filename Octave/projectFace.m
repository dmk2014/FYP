function weightOfUnknownFace = projectFace(U,face,averageFace)
  #Face - average face
  #Then calculate its weights
  
  face = face - averageFace;
  
  for i=1:columns(U)
    weightOfUnknownFace(i,1) = U(:,i)' * face;  
  endfor
endfunction