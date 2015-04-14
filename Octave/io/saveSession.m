function saveSession(recogniserData)
  % saveSession - save a recogniser session to disk
  %
  % Inputs:
  %    recogniserData - struct containing the recognisers required data

  % Author: Darren Keane
  % Institute of Technology, Tralee
  % email: darren.m.keane@students.ittralee.ie
  
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