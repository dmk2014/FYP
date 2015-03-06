using System;
using System.Collections.Generic;
using System.Drawing;

namespace FacialRecognition.Library.Models
{
    public class Person
    {
        public String Id { get; set; }
        public String Forename { get; set; }
        public String Surname { get; set; }
        public List<Image> Images { get; set; }

        public Person()
        {
            this.Images = new List<Image>();
        }
    }
}