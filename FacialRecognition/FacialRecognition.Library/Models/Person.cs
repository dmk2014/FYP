using System;

namespace FacialRecognition.Library.Models
{
    public class Person
    {
        public String _id;
        public String Forename { get; set; }
        public String Surname { get; set; }

        //TODO
        //Handle images and other required fields

        public void SetID(String _id)
        {
            this._id = _id;
        }
    }
}