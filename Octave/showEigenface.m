function showEigenface(U,k)
  #TODO
  #Error checking
  
  eigenface = U(:,k);
  eigenface = reshape(eigenface,192,168);
  imagesc(eigenface);
  colormap(gray);
endfunction