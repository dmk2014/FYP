info = imfinfo("~/Desktop/FYP/YaleTrainingDatabase/yaleB01/yaleB01_P00A-005E-10.pgm");

##Read image from file
image = imread("~/Desktop/FYP/YaleTrainingDatabase/yaleB01/yaleB01_P00A-005E-10.pgm");

#Write image to disk
imwrite(image, "~/Desktop/FYP/Octave/testimage.png", "png");

#Converting image to column vector
colVector = reshape(image,32256,1);