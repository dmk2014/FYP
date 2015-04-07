function weightOfUnknownFace = projectFace(U,face,averageFace)
  if(nargin != 3)
    usage("projectFace(U,face,averageFace)");
  endif
  
  % Get the reduced face
  face = face - averageFace;
  
  % Calculate the weight of the reduced face
  for i=1:columns(U)
    weightOfUnknownFace(i,1) = U(:,i)' * face;  
  endfor
endfunction