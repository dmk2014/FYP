function reconstructFace(weights,U)
  faceWeights = weights(:,10); #300x1, or EigCountx1
  result = zeros(32256,1);

  for i=1:300
    curWeight = faceWeights(i,:);
    curEig = U(:,i);
    result += curWeight .* curEig;
  endfor

  #result += averageFace;
  result = reshape(result,192,168);
  imagesc(result);
  colormap(gray);
endfunction