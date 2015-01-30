#Reduce matrix using mean
reducedFaces = matrixOfColumnVectors - averageValues;

#Calculate covariance matrix
c = double(reducedFaces')*double(reducedFaces);

#This will fail, ridiculously inefficient
#Trying to calculate n^2 * n^2 matrix = 32256*32256

#Solution is to use an M numbers, where M is number of training images (2432)