#Proejecting New Face
image = double(imread("~/Desktop/FYP/YaleTrainingDatabase/yaleB01/yaleB01_P00_Ambient.pgm"));

image = reshape(image,rows(image) * columns(image),1);

weightOfUnknownFace = projectFace(U,image,averageFace);

#Euclidian Distance
#Loop through weights and find smallest distance
#ED = norm(W - W, 2); #use 2-norm approach

for i=1:columns(weights)
  ED(1,i) = norm(weights(:,i) - weightOfUnknownFace, 2);
endfor