function result = normalize(M, low, high)
  if(nargin != 3)
    usage("normalise(M, low, high)");
  endif
  
  % Normalise all values of M to within the range [low - high]
  
  minimumValue = min(M);
  maximumValue = max(M);
  
  % Normalise the data using its current minimum and maximum value
  result = M - minimumValue;
  result = result / (maximumValue - minimumValue);
  
  % Normalise to the data to within the specified low/high range
  result = result * (high - low);
  result = result + low;
endfunction