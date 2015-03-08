function plotEigenfaces(U)
  if(nargin != 1)
    usage("plotEigenfaces(U)");
  endif
  
  for i=1:16
    subplot(4,4,i); #rows,columns,i
    eigen = reshape(U(:,i),192,168);
    imshow(eigen, []); #[] will automatically normalize the data
    colormap(gray);
    title(sprintf("Eigenface #%i",i));
  endfor
endfunction