using FacialRecognition.Library.Models;
using System.Drawing;

namespace FacialRecognition.Library.Core
{
    public interface IFacialRecogniser
    {
        Person ClassifyFace(Image facialImage);
    }
}