function plotReconstruction(originalFace, reconstructedFace, nEigenfacesUsed)
  % plotReconstruction - display an unmodified face alongside that face as reconstructed from eigenfaces
  %
  % Inputs:
  %    originalFace - the original, source facial image
  %    reconstructedFace - the reconstruction of the original face from eigenfaces
  %    nEigenfacesUsed - number of eignfaces used in the reconstruction

  % Author: Darren Keane
  % Institute of Technology, Tralee
  % email: darren.m.keane@students.ittralee.ie
  
  if(nargin != 3)
    usage("plotReconstruction(originalFace, reconsructedFace, nEigenfacesUsed)");
  endif
  
  % Plot the original face
  subplot(1, 2, 1); % Rows, Columns, 1
  originalFace = reshape(originalFace, 192, 168);
  imshow(originalFace, []);
  title("Original Face");
  
  % Plot the reconstructed face
  subplot(1, 2, 2); % Rows, Columns, 2
  reconstructedFace = reshape(reconstructedFace, 192, 168);
  imshow(reconstructedFace, []);
  title(sprintf("Reconstruction (Using %i eigenfaces)", nEigenfacesUsed));
endfunction