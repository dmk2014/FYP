using System.Drawing;

namespace FacialRecognition.Library.Hardware
{
    public interface IImagingHardware
    {
        // Defines the minimum required, generic functionality of a connecting camera
        Bitmap CaptureImage();
    }
}