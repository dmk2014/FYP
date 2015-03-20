Updated: 23rd Febraury 2015

There are two main applications: FacialRecognition and Octave

#FacialRecognition
This contains the .NET solution for my FYP.

You will need to install the Microsoft Kinect SDK to run this project
Version 1.8 of the SDK is being used for development, and can be downloaded from the following link:
http://www.microsoft.com/en-us/download/details.aspx?id=40278

The EmguCV wrapper library for OpenCV is also required.
Copy the x86 amd x64 .dlls to the prepared folder strucure within the solution.


##Octave
This contains the implementation of the eigenface algorithm
You can demonstate this functionality by running the "eigenfaces.m" script
A 64-bit build of Octave is required, version 3.8.2 or above.

The IntrumentControl and go-redis packages are required. These allow communication with the Redis
server.


##Execution Instructions
1. Start Redis server, currently only accepting defaults on localhost

2. Start the RedisListener in Octave
   Execute "loadPackages.m" if required
   redisListener(redisConnection())

3. Start the FacialRecognition application
