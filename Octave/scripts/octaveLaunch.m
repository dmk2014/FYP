cd("C:/FacialRecognition/startup");

# Ensure FacialRecognition functions are accessible
# This is all my Octave code
# GenPath loads every .m file in the directory
addpath(genpath("C:/FacialRecognition/FYP/Octave"));
disp("Octave code loaded");

# Ensure go-redis functions are accessible
addpath ("C:/FacialRecognition/octave-packages/go-redis/go-redis/");
disp("Go-Redis package loaded");

# Install and load required packages
# Check if instrument-control is installed
isInstalled = checkPackageInstalled("instrument-control");

if (!isInstalled)
  pkg install "C:/FacialRecognition/octave-packages/instrument-control-0.2.1.tar.gz";
endif

pkg load all;

disp("Instrument control package loaded");

# Initialise required global variables
redisGlobals;

disp("Redis globals initialised");
disp("Octave startup complete");
disp("Starting Redis listener with default settings...");

# Start listening for Redis connections
redisListener(redisConnection())