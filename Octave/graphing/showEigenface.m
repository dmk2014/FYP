function showEigenface(U,k)
  if(nargin != 2)
    usage("showEigenface(U, k)");
  endif
  #TODO
  #Error checking
  
  eigenface = U(:,k);
  #normalize(eigenface,0,255);
  eigenface = reshape(eigenface,192,168);
  #imagesc(image) -> scales image and displays
  imagesc(eigenface);
  colormap(gray);
endfunction