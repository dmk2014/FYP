function result = loadMatrixData(fileName)
  #load the requested file from disk
  dir = pwd();
  cd("data");
  result = load(fileName);
  result = result.M; #as files is loaded as struct data type
  cd(dir);
endfunction