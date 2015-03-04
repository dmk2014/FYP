function saveMatrixData(M,fileName)
  #save the matrix to disk using the HDF5 format
  dir = pwd();
  
  if !isdir("C:/FacialRecognition/data")
    mkdir("C:/FacialRecognition/data");
  endif
  
  cd("C:/FacialRecognition/data");
  save("-hdf5",fileName,"M");
  cd(dir);
endfunction