function plotEigenfaces(U)
  for i=1:16
    subplot(4,4,i);
    eigen = reshape(U(:,i),192,168);
    imshow(eigen, [0,255]);
    colormap(gray);
    title(sprintf("Eigenface #%i",i));
  endfor
endfunction