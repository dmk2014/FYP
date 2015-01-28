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

#Below code reads training set to memory
#Takes ~30 seconds using an i5 @ 3.8Ghz, and ~150MB RAM
trainingDatabase = readdir("~/Desktop/FYP/YaleTrainingDatabase");
dirCount = 0;
folderCount = numel(trainingDatabase);

matrixOfColumnVectors = [];

for i=1:folderCount
  #Skip special files . and ..
  if(regexp(trainingDatabase{i}, "^\\.\\.?$"))
    continue;
  endif
  
  #disp(trainingDatabase(i))
  currentFolder = ["~/Desktop/FYP/YaleTrainingDatabase/" trainingDatabase{i}]
  
  if(isdir(currentFolder))
    #Read all .pgm images in that dir
    imgDir = readdir(currentFolder);
    
    for j=1:numel(imgDir)
      if(regexp(imgDir{j}, "^\\.\\.?$") || regexp(imgDir{j}, "_Ambient.pgm"))
        continue;
      endif
      
      if(regexp(imgDir{j}, ".pgm"))
        currentImagePath = [currentFolder "/" imgDir{j}]
        img = imread(currentImagePath);
        
        #Process the image
        img = img';
        img = reshape(img,rows(img) * columns(img),1);
        matrixOfColumnVectors = [matrixOfColumnVectors, img];
      endif
    endfor
    
    dirCount = dirCount + 1;
  endif
endfor

#PseudoCode to calculate the mean
#for each row in matix of training set images
#   row = matrix[i,:]
#   rowMean = mean(row)
#   store mean as column vector -> [nx1], or [2432x1]
#endfor
#subtract mean from training set -> is time an issue? >700 million values to compute