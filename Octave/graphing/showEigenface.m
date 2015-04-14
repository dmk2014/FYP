function showEigenface(eigenfaces, idx)
  % showEigenface - display a single, specified eigenface
  %
  % Inputs:
  %    eigenfaces - matrix of eigenfaces
  %    idx - index of eigenface to display

  % Author: Darren Keane
  % Institute of Technology, Tralee
  % email: darren.m.keane@students.ittralee.ie
  
  if(nargin != 2)
    usage("showEigenface(eigenfaces, idx)");
  endif
  
  % Retrieve the eigenface and resize from column vector to image
  eigenface = eigenfaces(:, idx);
  eigenface = reshape(eigenface, 192, 168);
  
  % Display the eigenface
  imagesc(eigenface);
  colormap(gray);
endfunction