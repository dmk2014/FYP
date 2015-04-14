function result = normalize(M, low, high)
  % normalise - ensure all values of the matrix M are within the range [low - high]
  %
  % Inputs:
  %    M - the matrix to normalise
  %    low - the lower bound to normalise to
  %    high - the upper bound to normalise to
  %
  % Outputs:
  %    result - a normalised matrix

  % Author: Darren Keane
  % Institute of Technology, Tralee
  % email: darren.m.keane@students.ittralee.ie
  
  if(nargin != 3)
    usage("normalise(M, low, high)");
  endif
  
  minimumValue = min(M);
  maximumValue = max(M);
  
  % Normalise the data using its current minimum and maximum value
  result = M - minimumValue;
  result = result / (maximumValue - minimumValue);
  
  % Normalise to the data to within the specified low/high range
  result = result * (high - low);
  result = result + low;
endfunction