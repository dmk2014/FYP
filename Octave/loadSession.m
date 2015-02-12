#Script to load session data

#Load training set
M = loadMatrixData("training_set");

#Load average face
averageFace = loadMatrixData("average_face");

#Load reduced faces
reducedFaces = loadMatrixData("reduced_faces");

#Load eigenfaces
U = loadMatrixData("eigenfaces");

#Load weights
weights = loadMatrixData("weights");