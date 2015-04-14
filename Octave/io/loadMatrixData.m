function result = loadMatrixData(fileName)
  % loadMatrixData - load a matrix from disk
  %
  % Inputs:
  %    fileName - the name, on disk, of the matrix to be loaded
  %
  % Outputs:
  %    result - loaded matrix data

  % Author: Darren Keane
  % Institute of Technology, Tralee
  % email: darren.m.keane@students.ittralee.ie
  
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