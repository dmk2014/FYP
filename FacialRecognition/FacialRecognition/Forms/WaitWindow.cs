using System.Drawing;
using System.Windows.Forms;

namespace FacialRecognition.Forms
{
    public partial class frmWaitWindow : Form
    {
        private bool Moving = false;
        private int MouseStartX;
        private int MouseStartY;

        public frmWaitWindow()
        {
            InitializeComponent();
        }

        private void frmWaitWindow_MouseDown(object sender, MouseEventArgs e)
        {
            this.Moving = true;
            this.MouseStartX = e.X;
            this.MouseStartY = e.Y;
        }

        private void frmWaitWindow_MouseUp(object sender, MouseEventArgs e)
        {
            this.Moving = false;
        }

        private void frmWaitWindow_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.Moving)
            {
                this.SetDesktopLocation(MousePosition.X - MouseStartX, MousePosition.Y - MouseStartY);
            }
        }
    }
}