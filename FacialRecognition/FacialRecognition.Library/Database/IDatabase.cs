using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacialRecognition.Library.Database
{
    interface IDatabase
    {
        //TODO
        //Define all DB functionality required
        //Store a person
        //Update a person
        //Retrieve a person
        //Retrieve all persons

        Boolean Store(FacialRecognition.Library.Models.Person Person);
        Boolean Update(FacialRecognition.Library.Models.Person Person);

        FacialRecognition.Library.Models.Person Retrieve();
        FacialRecognition.Library.Models.Person RetrieveAll();
    }
}