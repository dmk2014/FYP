using System.Drawing;

namespace FacialRecognition.Library.Hardware
{
    interface IImagingHardware
    {
        // TODO
        // Defines the generic functions of connected hardware
        // Allows system to easily use diff cameras such as a webcam

        Bitmap CaptureImage();
    }
}