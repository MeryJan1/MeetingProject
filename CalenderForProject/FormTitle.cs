using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalenderForProject
{
    public partial class FormTitle : Form
    {
        public FormTitle()
        {
            InitializeComponent();
        }

        private void buttonSaveMeet_Click(object sender, EventArgs e)
        {
            string TitleMeet = txtBoxTitleMeet.Text;
            string DescribtionMeet = richBoxDescribtion.Text;
           FormTitle formTitle = new FormTitle();
           // open messagebox for sharing link
           // closed this Form

            
        }
    }
}
