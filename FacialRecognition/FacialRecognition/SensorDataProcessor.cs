using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FacialRecognition
{
    public class SensorDataProcessor
    {
        public Bitmap ColorToBitmap(ColorImageFrame _imageFrame)
        {
            var _byte = new byte[_imageFrame.PixelDataLength];
            _imageFrame.CopyPixelDataTo(_byte);

            var _image = new Bitmap(_imageFrame.Width,
                _imageFrame.Height,
                PixelFormat.Format32bppRgb
            );

            var _bmapdata = _image.LockBits(new Rectangle(0, 0, _imageFrame.Width, _imageFrame.Height),
                ImageLockMode.WriteOnly,
                _image.PixelFormat);

            var _addressFirstPixel = _bmapdata.Scan0;
            Marshal.Copy(_byte, 0, _addressFirstPixel, _imageFrame.PixelDataLength);

            _image.UnlockBits(_bmapdata);
            return _image;
        }

        public Bitmap DepthToBitmap(DepthImageFrame imageFrame)
        {
            short[] _pixelData = new short[imageFrame.PixelDataLength];
            imageFrame.CopyPixelDataTo(_pixelData);

            Bitmap _image = new Bitmap(imageFrame.Width,
                imageFrame.Height,
                PixelFormat.Format16bppRgb555
            );

            BitmapData bmapdata = _image.LockBits(
                new Rectangle(0, 0, imageFrame.Width, imageFrame.Height),
                ImageLockMode.WriteOnly,
                _image.PixelFormat
            );

            var _addressFirstPixel = bmapdata.Scan0;
            Marshal.Copy(_pixelData, 0, _addressFirstPixel, imageFrame.Width * imageFrame.Height);

            _image.UnlockBits(bmapdata);
            return _image;
        }
    }
}