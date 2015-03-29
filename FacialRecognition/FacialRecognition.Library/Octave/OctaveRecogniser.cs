using System;
using System.Drawing;
using System.Collections.Generic;

namespace FacialRecognition.Library.Octave
{
    public class OctaveRecogniser : Core.IFacialRecogniser
    {
        private OctaveInterface Interface;

        public OctaveRecogniser(OctaveInterface Interface)
        {
            this.SetInterface(Interface);
        }

        public void SetInterface(OctaveInterface Interface)
        {
            this.Interface = Interface;
        }

        public Models.Person ClassifyFace(Image FacialImage)
        {
            // FacialImage parameter is expected to be received in normalised form
            // It is an image that must be marshalled to a string
            var imageAsString = this.MarshalFacialImage(FacialImage);

            var recogniserRequest = new OctaveMessage((int)OctaveMessageType.REQUEST_REC, imageAsString);
            Interface.SendRequest(recogniserRequest);

            var recogniserResponse = Interface.ReceiveResponse(15000);

            if (recogniserResponse.Code == (int)OctaveMessageType.RESPONSE_OK)
            {
                // The recogniser response will be the ID of the closest match found in the facial database
                var result = new Models.Person();
                result.Id = recogniserResponse.Data;
                return result;
            }
            else
            {
                throw new Exception(recogniserResponse.Data);
            }
        }

        private String MarshalFacialImage(Image FacialImage)
        {
            var facialBitmap = new Bitmap(FacialImage);
            var faceAsString = String.Empty;

            for (int column = 0; column < FacialImage.Width; column++)
            {
                for (int row = 0; row < FacialImage.Height; row++)
                {
                    // BGR all have same values - can use any one of these to produce 8 bit grayscale image
                    // Octave requires that data is passed in column major order
                    // column1
                    // column2
                    // columnN
                    var pixel = facialBitmap.GetPixel(column, row);
                    var value = pixel.B;
                    faceAsString += value + ",";
                }
            }

            //Remove trailing ','
            faceAsString = faceAsString.TrimEnd(',');

            return faceAsString;
        }

        public Boolean SaveSession()
        {
            var recogniserRequest = new OctaveMessage((int)OctaveMessageType.REQUEST_SAVE, String.Empty);
            Interface.SendRequest(recogniserRequest);

            var response = Interface.ReceiveResponse(30000);

            if (response.Code == (int)OctaveMessageType.RESPONSE_OK)
            {
                return true;
            }
            else
            {
                throw new Exception(response.Data);
            }
        }

        public Boolean ReloadSession()
        {
            var recogniserRequest = new OctaveMessage((int)OctaveMessageType.REQUEST_RELOAD, String.Empty);
            Interface.SendRequest(recogniserRequest);

            var response = Interface.ReceiveResponse(30000);

            if (response.Code == (int)OctaveMessageType.RESPONSE_OK)
            {
                return true;
            }
            else
            {
                throw new Exception(response.Data);
            }
        }

        public Boolean RetrainRecogniser(List<Models.Person> PeopleInDatabase)
        {
            // Prepare the data for retraining
            this.SendDataToCacheForRetraining(PeopleInDatabase);

            // Send a request to retrain the recogniser
            var recogniserRequest = new OctaveMessage((int)OctaveMessageType.REQUEST_RETRAIN, String.Empty);
            Interface.SendRequest(recogniserRequest);

            // Wait for a response - large timeout because retraining requires considerable time period
            int timeoutThirtyMinutes = 1800000;
            var repsonse = Interface.ReceiveResponse(timeoutThirtyMinutes);

            if (repsonse.Code == (int)OctaveMessageType.RESPONSE_OK)
            {
                return true;
            }
            else
            {
                throw new Exception(repsonse.Data);
            }
        }

        private void SendDataToCacheForRetraining(List<Models.Person> PeopleInDatabase)
        {
            // Ensure cache is cleared of all previous data
            Interface.EnsurePersonDataIsClearedFromCache();

            // Send all data to the cache
            foreach(var person in PeopleInDatabase)
            {
                foreach(var image in person.Images)
                {
                    Interface.SendPersonDataToCache(person.Id, this.MarshalFacialImage(image));
                }
            }

            // Mark end of data in the cache - required by Octave
            Interface.SendPersonDataToCache(((int)OctaveMessageType.NO_DATA).ToString(), ((int)OctaveMessageType.NO_DATA).ToString());
        }
    }
}