function result = loadMatrixData(fileName)
  #load the requested file from disk
  #error check: if in data then an error is thrown
  dir = pwd();
  cd("C:/FacialRecognition/data");
  result = load(fileName);
  result = result.M; #as files is loaded as struct data type
  cd(dir);
endfunction