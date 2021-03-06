﻿using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace FacialRecognition.Library.Hardware.KinectV1
{
    public class SensorDataProcessor
    {
        /// <summary>
        /// Convert a raw ColorImageFrame to a Bitmap image.
        /// </summary>
        /// <param name="colorFrame">The source color frame to be converted.</param>
        /// <returns>A Bitmap image.</returns>
        public Bitmap ColorToBitmap(Microsoft.Kinect.ColorImageFrame colorFrame)
        {
            var sourceImageData = new byte[colorFrame.PixelDataLength];
            colorFrame.CopyPixelDataTo(sourceImageData);

            return this.ConvertColorByteArrayToBitmap(sourceImageData,
                colorFrame.Width,
                colorFrame.Height,
                colorFrame.PixelDataLength,
                PixelFormat.Format32bppRgb);
        }

        /// <summary>
        /// Converts a raw DepthImageFrame to a Bitmap image.
        /// </summary>
        /// <param name="depthFrame">The source depth frame to be converted.</param>
        /// <returns>A Bitmap image.</returns>
        public Bitmap DepthToBitmap(Microsoft.Kinect.DepthImageFrame depthFrame)
        {
            var sourceDepthData = new short[depthFrame.PixelDataLength];
            depthFrame.CopyPixelDataTo(sourceDepthData);

            return this.ConvertDepthShortArrayToBitmap(sourceDepthData,
                depthFrame.Width,
                depthFrame.Height,
                PixelFormat.Format16bppRgb555);
        }

        /// <summary>
        /// Uses the provided depth data to reduce a color image frame. Pixels outside the max depth are removed.
        /// </summary>
        /// <param name="colorFrame">The source color image frame to be reduced.</param>
        /// <param name="depthFrame">The depth frame.</param>
        /// <param name="maxDepth">The max depth in millimetres.</param>
        /// <returns>A Bitmap image.</returns>
        public Bitmap ReduceColorImageUsingDepthData(Microsoft.Kinect.ColorImageFrame colorFrame, Microsoft.Kinect.DepthImageFrame depthFrame, int maxDepth)
        {
            // Reference used for Kinect Depth API functions: https://msdn.microsoft.com/en-us/library/jj131029.aspx
            
            // Get the depth data
            var depthData = new Microsoft.Kinect.DepthImagePixel[depthFrame.PixelDataLength];
            depthFrame.CopyDepthImagePixelDataTo(depthData);

            // Get the color data
            var colorImageData = new byte[colorFrame.PixelDataLength];
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
            var image = this.ConvertColorByteArrayToBitmap(colorImageData,
                colorFrame.Width,
                colorFrame.Height,
                colorFrame.PixelDataLength,
                PixelFormat.Format32bppRgb);

            return image;
        }

        // While these methods are quite similar, a conscious design design was made to not refactor them
        // Numerous errors occurred following a refactoring attempt
        // There are minor but important differences between the methods
        // First note that ColorImageFrame and DepthImageFrame have different data structures
        // -> ColorToBitmap requires byte[]
        // -> DepthToBitmap requires short[]
        // Parameters required for Marshal.copy() method are different
        private Bitmap ConvertColorByteArrayToBitmap(byte[] colorData, int imageWidth, int imageHeight, int pixelDataLength, PixelFormat pixelFormat)
        {
            var image = new Bitmap(imageWidth, imageHeight, pixelFormat);
            var imageRectangle = new Rectangle(0, 0, imageWidth, imageHeight);
            var bitmapData = image.LockBits(imageRectangle, ImageLockMode.WriteOnly, image.PixelFormat);
            var addressFirstPixel = bitmapData.Scan0;

            Marshal.Copy(colorData, 0, addressFirstPixel, pixelDataLength);
            image.UnlockBits(bitmapData);

            return image;
        }

        private Bitmap ConvertDepthShortArrayToBitmap(short[] depthData, int imageWidth, int imageHeight, PixelFormat pixelFormat)
        {
            var image = new Bitmap(imageWidth, imageHeight, pixelFormat);
            var imageRectangle = new Rectangle(0, 0, imageWidth, imageHeight);
            var bitmapData = image.LockBits(imageRectangle, ImageLockMode.WriteOnly, image.PixelFormat);
            var addressFirstPixel = bitmapData.Scan0;

            Marshal.Copy(depthData, 0, addressFirstPixel, imageWidth * imageHeight);
            image.UnlockBits(bitmapData);

            return image;
        }
    }
}