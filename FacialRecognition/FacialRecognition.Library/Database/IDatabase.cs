using FacialRecognition.Library.Models;
using System;
using System.Collections.Generic;

namespace FacialRecognition.Library.Database
{
    public interface IDatabase
    {
        Boolean Store(Person Person);
        Boolean Update(Person Person);
        Person Retrieve(String ID);
        List<Person> RetrieveAll();
    }
}