using System.Windows.Forms;

namespace CalenderForProject
{
    public partial class ucBlank : UserControl
    {
        public ucBlank()
        {
            InitializeComponent();
        }

        private void UserControlBlank_Click(object sender, System.EventArgs e)
        {
            MessageBox.Show("This Field Cannot Be Selected.");
        }
    }
}
