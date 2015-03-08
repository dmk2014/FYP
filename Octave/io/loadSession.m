function sessionData = loadSession()
  #clear("M","averageFace","reducedFaces","U","weights");
   
  #Load training set
  sessionData.M = loadMatrixData("training_set");
  
  #Load face labels
  sessionData.labels = loadMatrixData("face_labels");

  #Load average face
  sessionData.averageFace = loadMatrixData("average_face");

  #Load reduced faces
  sessionData.reducedFaces = loadMatrixData("reduced_faces");

  #Load eigenfaces
  sessionData.U = loadMatrixData("eigenfaces");

  #Load weights
  sessionData.weights = loadMatrixData("weights");
endfunction