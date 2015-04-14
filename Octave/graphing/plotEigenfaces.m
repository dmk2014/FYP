function plotEigenfaces(eigenfaces)
  % plotEigenfaces - display an Octave plot of the first 16 eigenfaces
  %
  % Inputs:
  %    eigenfaces - the matrix of eigenfaces from which to plot

  % Author: Darren Keane
  % Institute of Technology, Tralee
  % email: darren.m.keane@students.ittralee.ie
  
  if(nargin != 1)
    usage("plotEigenfaces(eigenfaces)");
  endif
  
  for i=1:16
    subplot(4, 4, i); % Rows, Columns, i
    eigen = reshape(eigenfaces(:, i), 192, 168);
    imshow(eigen, []); % [] will automatically normalize the data
    colormap(gray);
    title(sprintf("Eigenface #%i", i));
  endfor
endfunction