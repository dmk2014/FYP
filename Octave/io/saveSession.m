function saveSession(sessionData)
  #Save training set
  saveMatrixData(sessionData.M,"training_set");

  #Save average face
  saveMatrixData(sessionData.averageFace,"average_face");

  #Save reduced faces
  saveMatrixData(sessionData.reducedFaces,"reduced_faces");

  #Save eigenfaces
  saveMatrixData(sessionData.U,"eigenfaces");

  #Save weights
  saveMatrixData(sessionData.weights,"weights");
endfunction