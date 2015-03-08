##Read image from file
#image = imread("~/Desktop/FYP/YaleTrainingDatabase/yaleB01/yaleB01_P00A-005E-10.pgm");

#Write image to disk
#imwrite(image, "~/Desktop/FYP/Octave/testimage.png", "png");

function [data,labels] = loadYaleTrainingDatabase(path)
  if(nargin != 1)
    usage("loadYaleTrainingDatabase(path)");
  endif
  
  trainingDatabase = readdir(path);
  folderCount = numel(trainingDatabase);
  
  data = [];
  labels = {};

  for i=1:folderCount
    #Skip special files . and ..
    if(regexp(trainingDatabase{i}, "^\\.\\.?$"))
      continue;
    endif
  
    currentFolder = [path trainingDatabase{i}];
    test = trainingDatabase(i,:);
    test1 = trainingDatabase{i};
    
    if(isdir(currentFolder))
      #Read all .pgm images in that dir
      imgDir = readdir(currentFolder);
    
      for j=1:numel(imgDir)
        if(regexp(imgDir{j}, "^\\.\\.?$") || regexp(imgDir{j}, "_Ambient.pgm"))
          continue;
        endif
      
        if(regexp(imgDir{j}, ".pgm"))
          currentImagePath = [currentFolder "/" imgDir{j}];
          img = double(imread(currentImagePath));
        
          #Process the image
          img = reshape(img,rows(img) * columns(img),1);
          data = [data, img];          
          #label = [label; test1]; #label will be folder name/subject name
          labels = [labels; test];
        endif
      endfor
    endif
  endfor
endfunction