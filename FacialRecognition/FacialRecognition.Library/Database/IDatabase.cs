using FacialRecognition.Library.Models;
using System;
using System.Collections.Generic;

namespace FacialRecognition.Library.Database
{
    public interface IDatabase
    {
        void CreateDatabase(string database);
        void DeleteDatabase(string database);
        bool Store(Person person);
        bool Update(Person person);
        Person Retrieve(String id);
        List<Person> RetrieveAll();
    }
}