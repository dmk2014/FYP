function plotReconstruction(originalFace, reconstructedFace, k)
  subplot(1,2,1);
  imshow(originalFace, []);
  title("Original Face");
  
  sunplot(1,2,2);
  imshow(reconstructedFace, []);
  title("Reconstruction (Using %k eigenfaces)",k);
endfunction