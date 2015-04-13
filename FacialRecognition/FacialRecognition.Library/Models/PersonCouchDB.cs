using DreamSeat.Interfaces;

namespace FacialRecognition.Library.Models
{
    public class PersonCouchDB : Person, ICouchDocument
    {
        /// <summary>
        /// Get or set the persons revision number.
        /// </summary>
        public string Rev { get; set; }
    }
}