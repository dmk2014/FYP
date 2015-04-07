function plotRecognition(M,unknownFace,idxFound)
  if(nargin != 3)
    usage("plotRecognition(M, unknownFace, idxFound)");
  endif
  
  % M = face set
  % unknownFace = face to be recognised
  % idxFound = index of closest match in database
  subplot(1,2,1);
  unknownFace = reshape(unknownFace,192,168); % TODO: remove hard-coded values
  imshow(unknownFace, []);
  title("Unknown Face");
  
  closestMatch = M(:,idxFound);
  closestMatch = reshape(closestMatch,192,168);
  
  subplot(1,2,2);
  imshow(closestMatch, []);
  title("Closest Match in Database");
endfunction