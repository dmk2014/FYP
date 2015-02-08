function result = normalize(M,low,high)
  minVal = min(M);
  maxVal = max(M);
  
  result = M - minVal;
  result = result / (maxVal-minVal);
  
  result = result * (high-1);
  result = result+1;
endfunction