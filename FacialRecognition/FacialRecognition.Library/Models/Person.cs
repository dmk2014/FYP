using System;

namespace FacialRecognition.Library.Models
{
    public class Person
    {
        public int _id;
        public String Forename { get; set; }
        public String Surname { get; set; }

        //TODO
        //Handle images and other required fields

        public void SetID(int _id)
        {
            this._id = _id;
        }
    }
}