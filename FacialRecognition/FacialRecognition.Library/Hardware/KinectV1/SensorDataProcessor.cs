﻿using Microsoft.Kinect;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace FacialRecognition.Library.Hardware.KinectV1
{
    public class SensorDataProcessor
    {
        // While these methods are quite similar, a conscious design design was made to not refactor them
        // Numerous errors occurred following a refactoring attempt
        // There are minor but important differences between the methods
        // First note that ColorImageFrame and DepthImageFrame have different data structures
        // -> ColorToBitmap requires byte[]
        // -> DepthToBitmap requires short[]
        // Parameters required for Marshal.copy() method are different
        public Bitmap ColorToBitmap(ColorImageFrame imageFrame)
        {
            var sourceImageData = new byte[imageFrame.PixelDataLength];
            imageFrame.CopyPixelDataTo(sourceImageData);

            var image = new Bitmap(imageFrame.Width, imageFrame.Height, PixelFormat.Format32bppRgb);
            var imageRectangle = new Rectangle(0, 0, imageFrame.Width, imageFrame.Height);
            var bitmapData = image.LockBits(imageRectangle, ImageLockMode.WriteOnly, image.PixelFormat);
            var addressFirstPixel = bitmapData.Scan0;

            Marshal.Copy(sourceImageData, 0, addressFirstPixel, imageFrame.PixelDataLength);
            image.UnlockBits(bitmapData);

            return image;
        }

        public Bitmap DepthToBitmap(DepthImageFrame imageFrame)
        {
            var sourceDepthData = new short[imageFrame.PixelDataLength];
            imageFrame.CopyPixelDataTo(sourceDepthData);
            
            var image = new Bitmap(imageFrame.Width, imageFrame.Height, PixelFormat.Format16bppRgb555);
            var imageRectangle = new Rectangle(0, 0, imageFrame.Width, imageFrame.Height);
            var bitmapData = image.LockBits(imageRectangle, ImageLockMode.WriteOnly, image.PixelFormat);
            var addressFirstPixel = bitmapData.Scan0;

            Marshal.Copy(sourceDepthData, 0, addressFirstPixel, imageFrame.Width * imageFrame.Height);
            image.UnlockBits(bitmapData);

            return image;
        }

        public Bitmap ReduceColorImageUsingDepthData(ColorImageFrame colorFrame, DepthImageFrame depthFrame, int maxDepth)
        {
            // Reference used for Kinect Depth API functions: https://msdn.microsoft.com/en-us/library/jj131029.aspx

            // Get the depth data
            DepthImagePixel[] depthData = new DepthImagePixel[depthFrame.PixelDataLength];
            depthFrame.CopyDepthImagePixelDataTo(depthData);

            // Get the color data
            byte[] colorImageData = new byte[colorFrame.PixelDataLength];
            colorFrame.CopyPixelDataTo(colorImageData);

            // The color data is four times the size of the depth data
            // Color data has four values - R, G, B, A

            // Loop through the depth values
            for (var i = 0; i < depthData.Length; i++)
            {
                // Check if the depth value is outside the specified range
                if (depthData[i].Depth > maxDepth)
                {
                    // Modify the color frame - set the color to black
                    // Multiply the index by 4 to get the correct start point in the color data array
                    // Remember that color data has 4 times as many values as depth data

                    int startIndexRGBA = i * 4;
                    int rIndex = startIndexRGBA;
                    int gIndex = startIndexRGBA + 1;
                    int bIndex = startIndexRGBA + 2;
                    int aIndex = startIndexRGBA + 3;

                    colorImageData[rIndex] = 0;
                    colorImageData[gIndex] = 0;
                    colorImageData[bIndex] = 0;
                    colorImageData[aIndex] = 0;
                }
            }

            // Convert the processed color byte array to a Bitmap and return it
            var image = new Bitmap(colorFrame.Width, colorFrame.Height, PixelFormat.Format32bppRgb);
            var imageRectangle = new Rectangle(0, 0, colorFrame.Width, colorFrame.Height);
            var bitmapData = image.LockBits(imageRectangle, ImageLockMode.WriteOnly, image.PixelFormat);
            var addressFirstPixel = bitmapData.Scan0;

            Marshal.Copy(colorImageData, 0, addressFirstPixel, colorFrame.PixelDataLength);
            image.UnlockBits(bitmapData);

            return image;
        }
    }
}