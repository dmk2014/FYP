using Microsoft.Kinect;
using System;
using System.IO;

namespace FacialRecognition.Library.Hardware.KinectV1
{
    public class SensorDataIO
    {
        public void SaveRawPixelDataDepth(DepthImageFrame frame)
        {
            var _data = frame.GetRawPixelData();

            short[] pixelData = new short[frame.PixelDataLength];
            frame.CopyPixelDataTo(pixelData);

            using (StreamWriter _fileStream = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/depth.csv"))
            {
                for (int i = 1; i <= pixelData.Length; i++)
                {
                    _fileStream.Write(pixelData[i - 1] + ",");

                    if (i % 640 == 0 && i > 1)
                    {
                        _fileStream.Write("\n");
                    }
                }
            }
        }

        public void SaveRawPixelDataColour(ColorImageFrame frame)
        {
            var _data = frame.GetRawPixelData();

            var pixelData = new byte[frame.PixelDataLength];
            frame.CopyPixelDataTo(pixelData);

            using (StreamWriter _fileStream = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/colour.csv"))
            {
                for (int i = 1; i <= pixelData.Length; i++)
                {
                    _fileStream.Write(pixelData[i - 1] + ",");

                    if (i % 2560 == 0 && i > 1)
                    {
                        _fileStream.Write("\n");
                    }
                }
            }
        }
    }
}