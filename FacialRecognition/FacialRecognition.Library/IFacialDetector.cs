using System;
namespace FacialRecognition.Library
{
    interface IFacialDetector
    {
        System.Drawing.Rectangle[] DetectFaces(System.Drawing.Bitmap image);
    }
}