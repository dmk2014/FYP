using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacialRecognition.Library.Hardware
{
    interface IImagingHardware
    {
        //TODO
        //Defines the functions of connected hardware
        //Allows system to easily use diff cameras such as a webcam

        //e.g capture frame for Kinect should also handle background removal

        void CaptureImage();
    }
}