function result = loadMatrixData(fileName)
  if(nargin != 1)
    usage("loadMatrixData(fileName)");
  endif
  
  % Load the requested matrix from disk
  % Reset directory when complete
  dir = pwd();
  cd("C:/FacialRecognition/data");
  result = load(fileName);
  
  % corresponding save method stores data in M field of struct, so retrieve that field
  result = result.M; 
  cd(dir);
endfunction