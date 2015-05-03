using System.Windows.Forms;

namespace FacialRecognition.Util
{
    public class Messages
    {
        /// <summary>
        /// Displays an information message.
        /// </summary>
        /// <param name="owner">The Window which will own the message.</param>
        /// <param name="message">The message text.</param>
        public static void DisplayInformationMessage(IWin32Window owner, string message)
        {
            MessageBox.Show(owner, message, "Facial Recognition - Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Displays an error message.
        /// </summary>
        /// <param name="owner">The Window which will own the message.</param>
        /// <param name="message">The message text.</param>
        public static void DisplayErrorMessage(IWin32Window owner, string message)
        {
            MessageBox.Show(owner, message, "Facial Recognition - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}