using FacialRecognition.Library.Recognition;

namespace FacialRecognition.Library.Redis
{
    public class RedisMessage
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
        /// Construct an Redis message with no data.
        /// </summary>
        public RedisMessage()
        {
            this.Code = (int)RecogniserCode.NoData;
            this.Data = ((int)RecogniserCode.NoData).ToString();
        }

        /// <summary>
        /// Construct a Redis message using the specified code.
        /// </summary>
        /// <param name="code">The message code.</param>
        public RedisMessage(int code)
        {
            this.Code = code;
            this.Data = ((int)RecogniserCode.NoData).ToString();
        }

        /// <summary>
        /// Construct a Redis message using the specified code & data.
        /// </summary>
        /// <param name="code">The message code.</param>
        /// <param name="data">The message data.</param>
        public RedisMessage(int code, string data)
        {
            this.Code = code;
            this.Data = data;
        }
    }
}