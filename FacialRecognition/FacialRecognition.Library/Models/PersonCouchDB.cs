using DreamSeat.Interfaces;

namespace FacialRecognition.Library.Models
{
    public class PersonCouchDB : Person, ICouchDocument
    {
        public string Rev { get; set; }
    }
}