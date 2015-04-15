﻿using System;

namespace FacialRecognition.Library.Octave
{
    public class OctaveMessage
    {
        /// <summary>
        /// Gets or sets the message code.
        /// </summary>
        public int Code { set; get; }

        /// <summary>
        /// Gets or sets the message data.
        /// </summary>
        public string Data { set; get; }

        /// <summary>
        /// Construct an Octave message with no data.
        /// </summary>
        public OctaveMessage()
        {
            this.Code = (int)OctaveMessageType.NoData;
            this.Data = ((int)OctaveMessageType.NoData).ToString();
        }

        /// <summary>
        /// Construct an Octave message using the specified code.
        /// </summary>
        /// <param name="code">The message code.</param>
        public OctaveMessage(int code)
        {
            this.Code = code;
            this.Data = ((int)OctaveMessageType.NoData).ToString();
        }

        /// <summary>
        /// Construct an Octave message using the specified code & data.
        /// </summary>
        /// <param name="code">The message code.</param>
        /// <param name="data">The message data.</param>
        public OctaveMessage(int code, string data)
        {
            this.Code = code;
            this.Data = data;
        }
    }
}