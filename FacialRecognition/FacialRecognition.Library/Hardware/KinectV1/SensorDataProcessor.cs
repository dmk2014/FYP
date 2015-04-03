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
    }
}