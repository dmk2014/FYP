cd("C:/FacialRecognition/startup");

#Install and load required packages
#TODO
#Validate install and check permissions for re-install
pkg install "C:/FacialRecognition/octave-packages/instrument-control-0.2.1.tar.gz";
pkg load all;

#Ensure go-redis functions are accessible
addpath ("C:/FacialRecognition/octave-packages/go-redis/go-redis/");

#Ensure FacialRecognition functions are accessible
#This is all my Octave code
#GenPath loads every .m file in the directory
addpath(genpath("C:/FacialRecognition/FYP/Octave"));

disp("Octave startup complete");
disp("Starting Redis listener with default settings...");

#Start listening for Redis connections
redisListener(redisConnection())