% runAllTests - This script will execute all implemented tests

% Author: Darren Keane
% Institute of Technology, Tralee
% email: darren.m.keane@students.ittralee.ie

% ___Prepare For Testing___

disp("Initialising application for testing...");

% Ensure all testable functions are added to the load path
addpath(genpath("C:/FacialRecognition/FYP/Octave"));
disp("Octave code added to load path");

% Ensure go-redis functions are accessible
addpath ("C:/FacialRecognition/octave-packages/go-redis/go-redis/");
disp("Go-Redis package loaded");

% Ensure all packages are loaded
pkg load all;
disp("All installed packages loaded");
disp("...COMPLETE");

% ___Execute Test Scripts___

disp("\nRunning IO Tests...");
test testIO;

disp("\nRunning Eigenfaces Tests...");
test testEigenfaces;

disp("\nRunning Redis Tests...");
test testRedis;

disp("\nRunning Util Tests...");
test testUtil;