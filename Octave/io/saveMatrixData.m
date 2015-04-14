function saveMatrixData(M, fileName)
  % saveMatrixData - save a matrix to disk
  %
  % Inputs:
  %    M - the matrix to be saved
  %    fileName - the filename to be used

  % Author: Darren Keane
  % Institute of Technology, Tralee
  % email: darren.m.keane@students.ittralee.ie
  
  if(nargin != 2)
    usage("saveMatrixData(M, fileName)");
  endif
  
  % Save the matrix to disk using the HDF5 format
  % Reset directory when complete
  dir = pwd();
  
  % Create output directory if it doesn't exist
  if !isdir("C:/FacialRecognition/data")
    mkdir("C:/FacialRecognition/data");
  endif
  
  cd("C:/FacialRecognition/data");
  save("-hdf5", fileName, "M");
  
  cd(dir);
endfunction