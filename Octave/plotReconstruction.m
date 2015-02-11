function plotReconstruction(originalFace, reconstructedFace, k)
  subplot(1,2,1);
  originalFace = reshape(originalFace,192,168); #TODO: remove hard-coded values
  imshow(originalFace, []);
  title("Original Face");
  
  subplot(1,2,2);
  reconstructedFace = reshape(reconstructedFace,192,168);
  imshow(reconstructedFace, []);
  title(sprintf("Reconstruction (Using %i eigenfaces)",k));
endfunction