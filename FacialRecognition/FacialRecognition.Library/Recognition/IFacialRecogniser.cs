﻿using FacialRecognition.Library.Models;
using System.Drawing;
using System.Collections.Generic;

namespace FacialRecognition.Library.Recognition
{
    public interface IFacialRecogniser
    {
        Person ClassifyFace(Image facialImage);
        bool SaveSession();
        bool ReloadSession();
        bool RetrainRecogniser(List<Person> people);
    }
}