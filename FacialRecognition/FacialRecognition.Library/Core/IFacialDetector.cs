using System;

namespace FacialRecognition.Library.Core
{
    interface IFacialDetector
    {
        System.Drawing.Rectangle[] DetectFaces(System.Drawing.Bitmap image);
    }
}