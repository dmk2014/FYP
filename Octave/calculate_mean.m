#Take copy of training dataset
localFaces = matrixOfColumnVectors;
averageValues = [];

for i=1:rows(localFaces)
  row = localFaces(i,:);
  rowMean = mean(row);
  averageValues = [averageValues; rowMean];
endfor

#Temporarily Persist Data
averageFace = reshape(averageValues,192,168);
averageFace = uint8(averageFace);
imwrite(averageFace,"~/Desktop/FYP/Octave/averageFace.png","png");