function saveSession(recogniserData)
  if(nargin != 1)
    usage("saveSession(recogniserData)");
  endif
  
  % Save training set
  saveMatrixData(recogniserData.M, "training_set");
  
  % Save face labels
  saveMatrixData(recogniserData.labels, "face_labels");

  % Save average face
  saveMatrixData(recogniserData.averageFace, "average_face");

  % Save reduced faces
  saveMatrixData(recogniserData.reducedFaces, "reduced_faces");

  % Save eigenfaces
  saveMatrixData(recogniserData.U, "eigenfaces");

  % Save weights
  saveMatrixData(recogniserData.weights, "weights");
endfunction