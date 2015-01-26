info = imfinfo("~/Desktop/FYP/YaleTrainingDatabase/yaleB01/yaleB01_P00A-005E-10.pgm");

##Read image from file
image = imread("~/Desktop/FYP/YaleTrainingDatabase/yaleB01/yaleB01_P00A-005E-10.pgm");

#Write image to disk
imwrite(image, "~/Desktop/FYP/Octave/testimage.png", "png");

#A = A'
imageTranspose  = image';

#Converting image to column vector
colVector = reshape(imageTranspose,32256,1);

#Above is for one image
#We need to read each image from the training set and find the column vectors in turn
#Transpose step is to ensure correct ordering
#e.g [1,2,3
#     4,5,6]
#becomes [1,2,3,4,5,6] column vector, as oppsed to Octace reshape() default of [1,4,2,5,3,6]

trainingDatabase = readdir("~/Desktop/FYP/YaleTrainingDatabase");

for i=1:length(trainingDatabase)
  if(isdir(i))
    i
  endif
  
  #dir = trainingDatabase(i)
  #dir
endfor