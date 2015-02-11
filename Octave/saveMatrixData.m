function saveMatrixData(M,fileName)
  #save the matrix to disk using the HDF5 format
  dir = pwd();
  
  if !isdir("data")
    mkdir("data");
  endif
  
  cd("data");
  save("-hdf5",fileName,"M");
  cd(dir);
endfunction