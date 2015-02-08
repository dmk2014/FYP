#imagesc(image) -> scales image and displays

function showEigenface(U,k)
  #TODO
  #Error checking
  
  eigenface = U(:,k);
  normalize(eigenface,0,255);
  eigenface = reshape(eigenface,192,168);
  imagesc(eigenface);
  colormap(gray);
endfunction