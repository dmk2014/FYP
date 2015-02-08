function reconstructFace(weights,U,averageFace,faceIndex)
  faceWeights = weights(:,faceIndex); #300x1, or EigCountx1
  result = zeros(32256,1);

  for i=1:columns(U)
    curWeight = faceWeights(i,1);
    curEig = U(:,i);
    result += curWeight * curEig;
  endfor
  
  result = normalize(result,0,255);

  result += averageFace;
  result = reshape(result,192,168);
  imagesc(result);
  colormap(gray);
endfunction