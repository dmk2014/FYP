using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacialRecognition.Library
{
    interface IFacialRecogniser
    {
        //TODO
        //Define generic functionality to recognise a facial image

        Models.Person ClassifyFace(System.Drawing.Bitmap FacialImage);
    }
}