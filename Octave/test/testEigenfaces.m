% testEigenfaces - This script will execute tests on eigenface functions

% Author: Darren Keane
% Institute of Technology, Tralee
% email: darren.m.keane@students.ittralee.ie

% ___Test Calculate Average Face___

%!test
%! yaleFaces = loadYaleTrainingDatabase();
%! 
%! averageFace = calculateAverageFace(yaleFaces);
%! expectedRows = 32256;
%! expectedColumns = 1;
%!
%! assert(rows(averageFace), expectedRows);
%! assert(columns(averageFace), expectedColumns);


% ___Test Reduce Faces Using Average Face___

%!test
%! % Use a subset of the Yale images to improve testing efficiency
%! yaleFaces = loadYaleTrainingDatabase();
%! yaleFaces = yaleFaces(:, 1:500);
%!
%! averageFace = calculateAverageFace(yaleFaces);
%! reducedFaces = reduceFaces(yaleFaces, averageFace);
%!
%! expectedRows = 32256;
%! expectedColumns = 500;
%! 
%! assert(rows(reducedFaces), expectedRows);
%! assert(columns(reducedFaces), expectedColumns);


% ___Test Calculate Eigenfaces___

%!test
%! % Use a subset of the Yale images to improve testing efficiency
%! yaleFaces = loadYaleTrainingDatabase();
%! yaleFaces = yaleFaces(:, 1:500);
%!
%! averageFace = calculateAverageFace(yaleFaces);
%! reducedFaces = reduceFaces(yaleFaces, averageFace);
%!
%! numEigenfacesToCalculate = 50;
%! eigenfaces = getEigenfacesSVD(reducedFaces, numEigenfacesToCalculate);
%!
%! expectedRows = 32256;
%! expectedColumns = 50;
%!
%! assert(rows(eigenfaces), expectedRows);
%! assert(columns(eigenfaces), expectedColumns);


% ___Test Calculate Weights___

%!test
%! % Use a subset of the Yale images to improve testing efficiency
%! yaleFaces = loadYaleTrainingDatabase();
%! yaleFaces = yaleFaces(:, 1:500);
%!
%! averageFace = calculateAverageFace(yaleFaces);
%! reducedFaces = reduceFaces(yaleFaces, averageFace);
%!
%! numEigenfacesToCalculate = 50;
%! eigenfaces = getEigenfacesSVD(reducedFaces, numEigenfacesToCalculate);
%!
%! weights = findWeights(reducedFaces, eigenfaces);
%!
%! expectedRows = 50;
%! expectedColumns = 500;
%!
%! assert(rows(weights), expectedRows);
%! assert(columns(weights), expectedColumns);