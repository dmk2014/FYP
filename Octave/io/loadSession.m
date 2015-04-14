function recogniserData = loadSession()
  % loadSession - loads a previously saved recogniser session from disk
  %
  % Outputs:
  %    recogniserData - struct containing the recognisers required data

  % Author: Darren Keane
  % Institute of Technology, Tralee
  % email: darren.m.keane@students.ittralee.ie
  
  % Load training set
  recogniserData.M = loadMatrixData("training_set");
  
  % Load face labels
  recogniserData.labels = loadMatrixData("face_labels");

  % Load average face
  recogniserData.averageFace = loadMatrixData("average_face");

  % Load reduced faces
  recogniserData.reducedFaces = loadMatrixData("reduced_faces");

  % Load eigenfaces
  recogniserData.U = loadMatrixData("eigenfaces");

  % Load weights
  recogniserData.weights = loadMatrixData("weights");
endfunction