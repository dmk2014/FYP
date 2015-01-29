#PseudoCode to calculate the mean
#for each row in matix of training set images
#   row = matrix[i,:]
#   rowMean = mean(row)
#   store mean as column vector -> [nx1], or [2432x1]
#endfor
#subtract mean from training set -> is time an issue? >700 million values to compute


localFaces = matrixOfColumnVectors;
rowMeans = [];

for i=1:rows(localFaces)
  row = localFaces(i,:);
  rowMean = mean(row);
  rowMeans = [rowMeans; rowMean];
endfor

#averageFace = matrixOfColumnVectors - rowMeans;
#averageFace = reshape(averageFace,192,168);

#Temporarily Persist Data
averageFace = reshape(rowMeans,192,168);
averageFace = uint8(averageFace);
imwrite(averageFace,"~/Desktop/FYP/Octave/averageFace.png","png");