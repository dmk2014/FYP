using FacialRecognition.Globals;
using FacialRecognition.Library.Database;
using FacialRecognition.Library.Detection;
using FacialRecognition.Library.Hardware.KinectV1;
using FacialRecognition.Library.Recognition;
using System;
using System.Collections.Generic;

namespace FacialRecognition.Controllers
{
    public class StartupController
    {
        private string CouchDBHost;
        private int CouchDBPort;
        private string CouchDatabaseName;
        private string RedisHost;
        private int RedisPort;

        public void SystemStartup()
        {
            var errorsOccured = false;
            var errorMessages = new List<String>();
            this.ReadApplicationSettings();

            // Construct Facial Detector
            try
            {
                ApplicationGlobals.Detector = new FacialDetector();
            }
            catch (Exception ex)
            {
                errorsOccured = true;
                errorMessages.Add(ex.Message);
            }

            // Initialise Database
            try
            {
                ApplicationGlobals.Database = new CouchDatabase(this.CouchDBHost, this.CouchDBPort, this.CouchDatabaseName);
            }
            catch (Exception ex)
            {
                errorsOccured = true;
                errorMessages.Add(ex.Message);
            }

            // Initialise Kinect sensor
            try
            {
                this.PrepareKinectSensor();
            }
            catch (Exception ex)
            {
                errorsOccured = true;
                errorMessages.Add(ex.Message);
            }

            // Initialise PhotometricRecogniser which utilises Redis
            try
            {
                ApplicationGlobals.Recogniser = new PhotometricFacialRecogniser(this.RedisHost, this.RedisPort);
            }
            catch (Exception ex)
            {
                errorsOccured = true;
                errorMessages.Add(ex.Message);
            }

            if (errorsOccured)
            {
                string error = this.ConstructErrorMessage(errorMessages);
                throw new Exception(error);
            }
        }

        private string ConstructErrorMessage(List<string> errorMessages)
        {
            var errorOutput = String.Empty;

            foreach (var message in errorMessages)
            {
                errorOutput += message + "\n\n";
            }

            errorOutput.TrimEnd('\n');

            string error = "There were errors launching the application - this may prevent it from functioning correctly.\n\nError(s):\n" + errorOutput;
            return error;
        }

        private void ReadApplicationSettings()
        {
            this.CouchDBHost = Properties.Settings.Default.CouchDBHost;
            this.CouchDBPort = Properties.Settings.Default.CouchDBPort;
            this.CouchDatabaseName = Properties.Settings.Default.CouchDatabaseName;
            this.RedisHost = Properties.Settings.Default.RedisHost;
            this.RedisPort = Properties.Settings.Default.RedisPort;
        }

        private void PrepareKinectSensor()
        {
            if (Microsoft.Kinect.KinectSensor.KinectSensors.Count > 0)
            {
                ApplicationGlobals.Kinect = new KinectV1Sensor(Microsoft.Kinect.KinectSensor.KinectSensors[0]);
            }
            else
            {
                throw new Exception("Could find a Kinect sensor attached to this system");
            }
        }
    }
}