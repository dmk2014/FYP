##Read image from file
#image = imread("~/Desktop/FYP/YaleTrainingDatabase/yaleB01/yaleB01_P00A-005E-10.pgm");

#Write image to disk
#imwrite(image, "~/Desktop/FYP/Octave/testimage.png", "png");

#Above is for one image
#We need to read each image from the training set and find the column vectors in turn
#Transpose step is to ensure correct ordering
#e.g [1,2,3
#     4,5,6]
#becomes [1,2,3,4,5,6] column vector, as oppsed to Octace reshape() default of [1,4,2,5,3,6]

#Below code reads training set to memory
#Takes ~30 seconds using an i5 @ 3.8Ghz, and ~150MB RAM
trainingDatabase = readdir("~/Desktop/FYP/YaleTrainingDatabase");
folderCount = numel(trainingDatabase);

M = [];

for i=1:folderCount
  #Skip special files . and ..
  if(regexp(trainingDatabase{i}, "^\\.\\.?$"))
    continue;
  endif
  
  currentFolder = ["~/Desktop/FYP/YaleTrainingDatabase/" trainingDatabase{i}];
  
  if(isdir(currentFolder))
    #Read all .pgm images in that dir
    imgDir = readdir(currentFolder);
    
    for j=1:numel(imgDir)
      if(regexp(imgDir{j}, "^\\.\\.?$") || regexp(imgDir{j}, "_Ambient.pgm"))
        continue;
      endif
      
      if(regexp(imgDir{j}, ".pgm"))
        currentImagePath = [currentFolder "/" imgDir{j}];
        img = imread(currentImagePath);
        
        #Process the image
        #img = img';
        img = reshape(img,rows(img) * columns(img),1);
        M = [M, img];
      endif
    endfor
  endif
endfor