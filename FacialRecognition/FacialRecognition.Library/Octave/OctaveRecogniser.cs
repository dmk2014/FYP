using FacialRecognition.Library.Models;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace FacialRecognition.Library.Octave
{
    public class OctaveRecogniser : Core.IFacialRecogniser
    {
        private OctaveInterface Interface;

        public OctaveRecogniser(string redisHost, int redisPort)
        {
            this.SetInterface(redisHost, redisPort);
        }

        public void SetInterface(string redisHost, int redisPort)
        {
            this.Interface = new OctaveInterface(redisHost, redisPort);
        }

        public Person ClassifyFace(Image facialImage)
        {
            // FacialImage parameter is expected to be received in normalised form
            // It is an image that must be marshalled to a string
            var imageAsString = this.MarshalFacialImage(facialImage);

            var recogniserRequest = new OctaveMessage((int)OctaveMessageType.RequestRecognition, imageAsString);
            this.Interface.SendRequest(recogniserRequest);

            var recogniserResponse = this.Interface.ReceiveResponse(15000);

            if (recogniserResponse.Code == (int)OctaveMessageType.ResponseOk)
            {
                // The recogniser response will be the ID of the closest match found in the facial database
                var result = new Person();
                result.Id = recogniserResponse.Data;
                return result;
            }
            else
            {
                throw new Exception(recogniserResponse.Data);
            }
        }

        private string MarshalFacialImage(Image facialImage)
        {
            var facialBitmap = new Bitmap(facialImage);
            var faceAsString = String.Empty;
            var seperator = ',';

            for (int column = 0; column < facialImage.Width; column++)
            {
                for (int row = 0; row < facialImage.Height; row++)
                {
                    // BGR all have same values - can use any one of these to produce 8 bit grayscale image
                    // Octave requires that data is passed in column major order
                    // column1
                    // column2
                    // columnN
                    var pixel = facialBitmap.GetPixel(column, row);
                    var value = pixel.B;
                    faceAsString += value + seperator.ToString();
                }
            }

            //Remove trailing ','
            faceAsString = faceAsString.TrimEnd(seperator);

            return faceAsString;
        }

        public bool SaveSession()
        {
            var recogniserRequest = new OctaveMessage((int)OctaveMessageType.RequestSave);
            this.Interface.SendRequest(recogniserRequest);

            var response = this.Interface.ReceiveResponse(30000);

            if (response.Code == (int)OctaveMessageType.ResponseOk)
            {
                return true;
            }
            else
            {
                throw new Exception(response.Data);
            }
        }

        public bool ReloadSession()
        {
            var recogniserRequest = new OctaveMessage((int)OctaveMessageType.RequestReload);
            this.Interface.SendRequest(recogniserRequest);

            var response = this.Interface.ReceiveResponse(30000);

            if (response.Code == (int)OctaveMessageType.ResponseOk)
            {
                return true;
            }
            else
            {
                throw new Exception(response.Data);
            }
        }

        public bool RetrainRecogniser(List<Person> people)
        {
            // Prepare the data for retraining
            this.SendDataToCacheForRetraining(people);

            // Send a request to retrain the recogniser
            var recogniserRequest = new OctaveMessage((int)OctaveMessageType.RequestRetrain);
            this.Interface.SendRequest(recogniserRequest);

            // Wait for a response - large timeout because retraining requires considerable time period
            int timeoutThirtyMinutes = 1800000;
            var repsonse = this.Interface.ReceiveResponse(timeoutThirtyMinutes);
            
            if (repsonse.Code == (int)OctaveMessageType.ResponseOk)
            {
                return true;
            }
            else
            {
                throw new Exception(repsonse.Data);
            }
        }

        private void SendDataToCacheForRetraining(List<Person> peopleInDatabase)
        {
            // Ensure cache is cleared of all previous data
            this.Interface.EnsurePersonDataIsClearedFromCache();

            // Send all data to the cache
            foreach(var person in peopleInDatabase)
            {
                foreach(var image in person.Images)
                {
                    this.Interface.SendPersonDataToCache(person.Id, this.MarshalFacialImage(image));
                }
            }

            // Mark end of data in the cache - required by Octave
            var endOfData = ((int)OctaveMessageType.NoData).ToString();
            this.Interface.SendPersonDataToCache(endOfData, endOfData);
        }
    }
}