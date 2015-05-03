using FacialRecognition.Library.Models;

namespace FacialRecognition.Globals
{
    public class DisplayedPerson
    {
        /// <summary>
        /// The Person that is currently displayed on the user interface.
        /// </summary>
        public static Person Person { get; set; }

        /// <summary>
        /// The index of which image of the Person is currently displayed on the user interface.
        /// </summary>
        public static int ImageIndex { get; set; }
    }
}