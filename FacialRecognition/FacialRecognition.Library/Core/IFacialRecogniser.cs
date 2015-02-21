using System.Drawing;

namespace FacialRecognition.Library.Core
{
    interface IFacialRecogniser
    {
        Models.Person ClassifyFace(Image FacialImage);
    }
}