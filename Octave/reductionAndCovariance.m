#Reduce matrix using mean
#faceDatabase - average
reducedFaces = matrixOfColumnVectors - rowMeans;

#Calculate covariance matrix
#faces = faces'
facesCovariance = reducedFaces';

#Singular Value Decomposition