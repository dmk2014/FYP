using System;
using System.Collections.Generic;
using System.Drawing;

namespace FacialRecognition.Library.Models
{
    public class Person
    {
        /// <summary>
        /// Get or set the persons Id.
        /// </summary>
        public String Id { get; set; }

        /// <summary>
        /// Get or set the persons forename.
        /// </summary>
        public String Forename { get; set; }

        /// <summary>
        /// Get or set the persons surname.
        /// </summary>
        public String Surname { get; set; }

        /// <summary>
        /// Get or set the List of images associated with the person.
        /// </summary>
        public List<Image> Images { get; set; }

        /// <summary>
        /// Create a new person object.
        /// </summary>
        public Person()
        {
            this.Images = new List<Image>();
        }
    }
}