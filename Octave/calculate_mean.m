averageValues = [];

for i=1:rows(M)
  row = M(i,:);
  rowMean = mean(row);
  averageValues = [averageValues; rowMean];
endfor

#Temporarily Persist Data
#averageFace = reshape(averageValues,192,168);
#averageFace = uint8(averageFace);
#imwrite(averageFace,"~/Desktop/FYP/Octave/averageFace.png","png");