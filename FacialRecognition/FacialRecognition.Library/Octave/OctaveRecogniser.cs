using FacialRecognition.Library.Models;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace FacialRecognition.Library.Octave
{
    public class OctaveRecogniser : Core.IFacialRecogniser
    {
        private RedisConnection Interface;

        /// <summary>
        /// Connects to an Octave recogniser using the Redis server at the specified host and port.
        /// </summary>
        /// <param name="redisHost">Host where Redis server is running.</param>
        /// <param name="redisPort">Port where Redis server is running.</param>
        public OctaveRecogniser(string redisHost, int redisPort)
        {
            this.SetInterface(redisHost, redisPort);
        }

        /// <summary>
        /// Sets the Redis server utilised to that at the specified host and port.
        /// </summary>
        /// <param name="redisHost">Host where Redis server is running.</param>
        /// <param name="redisPort">Port where Redis server is running.</param>
        public void SetInterface(string redisHost, int redisPort)
        {
            this.Interface = new RedisConnection(redisHost, redisPort);
        }

        /// <summary>
        /// Classify an unknown facial image.
        /// </summary>
        /// <param name="facialImage">A normalised facial image.</param>
        /// <returns>A person whose ID field contains the closest database match found.</returns>
        public Person ClassifyFace(Image facialImage)
        {
            // Normalised image that must be marshalled to a string
            var imageAsString = this.MarshalFacialImage(facialImage);

            var recogniserRequest = new OctaveMessage((int)OctaveMessageType.RequestRecognition, imageAsString);
            this.Interface.SendRequest(recogniserRequest);

            var timeoutThirtySeconds = 30000;
            var response = this.Interface.ReceiveResponse(timeoutThirtySeconds);

            if (response.Code == (int)OctaveMessageType.ResponseOk)
            {
                // The recogniser response will be the ID of the closest match found in the facial database
                var result = new Person();
                result.Id = response.Data;
                return result;
            }
            else
            {
                throw new Exception(response.Data);
            }
        }

        /// <summary>
        /// Save all data of the active recogniser to disk.
        /// </summary>
        /// <returns>A boolean indictaing the success of the operation.</returns>
        public bool SaveSession()
        {
            var recogniserRequest = new OctaveMessage((int)OctaveMessageType.RequestSave);
            this.Interface.SendRequest(recogniserRequest);

            int timeoutTenMinutes = 600000;
            var response = this.Interface.ReceiveResponse(timeoutTenMinutes);

            if (response.Code == (int)OctaveMessageType.ResponseOk)
            {
                return true;
            }
            else
            {
                throw new Exception(response.Data);
            }
        }

        /// <summary>
        /// Reload all data of the recogniser from the most recent persisted data.
        /// </summary>
        /// <returns>A boolean indictaing the success of the operation.</returns>
        public bool ReloadSession()
        {
            var recogniserRequest = new OctaveMessage((int)OctaveMessageType.RequestReload);
            this.Interface.SendRequest(recogniserRequest);

            int timeoutFiveMinutes = 300000;
            var response = this.Interface.ReceiveResponse(timeoutFiveMinutes);

            if (response.Code == (int)OctaveMessageType.ResponseOk)
            {
                return true;
            }
            else
            {
                throw new Exception(response.Data);
            }
        }

        /// <summary>
        /// Retrain the recogniser using the training set and a supplied list of people.
        /// </summary>
        /// <param name="people">People to be included in the retrained recogniser.</param>
        /// <returns>A boolean indictaing the success of the operation.</returns>
        public bool RetrainRecogniser(List<Person> people)
        {
            // Prepare the data for retraining
            this.SendDataToCacheForRetraining(people);

            // Send a request to retrain the recogniser
            var recogniserRequest = new OctaveMessage((int)OctaveMessageType.RequestRetrain);
            this.Interface.SendRequest(recogniserRequest);

            // Wait for a response - large timeout because retraining requires considerable time period
            int timeoutThirtyMinutes = 1800000;
            var response = this.Interface.ReceiveResponse(timeoutThirtyMinutes);
            
            if (response.Code == (int)OctaveMessageType.ResponseOk)
            {
                return true;
            }
            else
            {
                throw new Exception(response.Data);
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