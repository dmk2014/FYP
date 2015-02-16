using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacialRecognition.Library
{
    public class OctaveRecogniser : IFacialRecogniser
    {
        public Models.Person ClassifyFace(System.Drawing.Bitmap FacialImage)
        {
            //Normalize request image -> maybe should be done earlier than here
            //Send recognition request to Octave
            //Send response request
            //Handle response received, if any
            throw new NotImplementedException();
        }
    }
}