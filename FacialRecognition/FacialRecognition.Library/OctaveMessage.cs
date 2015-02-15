using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacialRecognition.Library
{
    public enum OctaveMessage
    {
        NO_DATA = 0,
        REQUEST_REC = 100,
        REQUEST_RELOAD = 200,
        REQUEST_SAVE = 300,
        RESPONSE_OK = 100,
        RESPONSE_FAIL = 200
    }
}