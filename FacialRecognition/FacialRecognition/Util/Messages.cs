using System.Windows.Forms;

namespace FacialRecognition.Util
{
    public class Messages
    {
        public static void DisplayInformationMessage(IWin32Window owner, string message)
        {
            MessageBox.Show(owner, message, "Facial Recognition - Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void DisplayErrorMessage(IWin32Window owner, string message)
        {
            MessageBox.Show(owner, message, "Facial Recognition - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}