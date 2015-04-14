function plotRecognition(M, unknownFace, idxClosestMatch)
  % plotRecognition: display an unknown face alongside its closest match in the database
  %
  % Inputs:
  %    M - the set of faces available to the recogniser
  %    unknownFace - the unknown face as a column vector
  %    idxClosestMatch - index of the closest match in M to the unknown face

  % Author: Darren Keane
  % Institute of Technology, Tralee
  % email: darren.m.keane@students.ittralee.ie
  
  if(nargin != 3)
    usage("plotRecognition(M, unknownFace, idxClosestMatch)");
  endif
  
  % Plot unknown face
  subplot(1, 2, 1); % Row, Column, 1
  unknownFace = reshape(unknownFace, 192, 168);
  imshow(unknownFace, []);
  title("Unknown Face");
  
  % Find and convert closest face match from column vector to matrix
  closestMatch = M(:, idxClosestMatch);
  closestMatch = reshape(closestMatch, 192, 168);
  
  % Plot closest match
  subplot(1, 2, 2); % Row, Column, 2
  imshow(closestMatch, []);
  title("Closest Match");
endfunction