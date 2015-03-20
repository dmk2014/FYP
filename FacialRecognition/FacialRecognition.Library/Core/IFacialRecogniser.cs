using System.Drawing;

namespace FacialRecognition.Library.Core
{
    public interface IFacialRecogniser
    {
        Models.Person ClassifyFace(Image FacialImage);
    }
}