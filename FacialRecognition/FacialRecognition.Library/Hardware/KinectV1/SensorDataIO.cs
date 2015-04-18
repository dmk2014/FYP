using System;
using System.IO;

namespace FacialRecognition.Library.Hardware.KinectV1
{
    public class SensorDataIO
    {
        private string DesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        public void SaveRawPixelDataDepth(Microsoft.Kinect.DepthImageFrame frame)
        {
            var pixelData = new short[frame.PixelDataLength];
            frame.CopyPixelDataTo(pixelData);

            using (var fileStream = new StreamWriter(DesktopPath + "/depth.csv"))
            {
                for (int i = 1; i <= pixelData.Length; i++)
                {
                    fileStream.Write(pixelData[i - 1] + ",");

                    if (i % 640 == 0 && i > 1)
                    {
                        fileStream.Write("\n");
                    }
                }
            }
        }

        public void SaveRawPixelDataColour(Microsoft.Kinect.ColorImageFrame frame)
        {
            var pixelData = new byte[frame.PixelDataLength];
            frame.CopyPixelDataTo(pixelData);

            using (var fileStream = new StreamWriter(DesktopPath + "/colour.csv"))
            {
                for (int i = 1; i <= pixelData.Length; i++)
                {
                    fileStream.Write(pixelData[i - 1] + ",");

                    if (i % 2560 == 0 && i > 1)
                    {
                        fileStream.Write("\n");
                    }
                }
            }
        }
    }
}