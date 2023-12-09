using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using static CalenderForProject.Form1;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Util.Store;
using System.Threading;

namespace CalenderForProject
{
    public partial class FormCreateCodecs : Form

    {

        public FormCreateCodecs()
        {
            InitializeComponent();

        }


        private void FormCreateCodecs_Load(object sender, EventArgs e)
        {

            tBoxCode.Text = FormTitle.rastgeleKod;
        }
       

        private void btnCopy_Click(object sender, EventArgs e)
        {
            
            Clipboard.SetText(tBoxCode.Text);
            MessageBox.Show("The text has been copied to the clipboard.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnOK_Click(object sender, EventArgs e)
        {

            FormCalendar formCalendar = new FormCalendar();
            formCalendar.Show();
            this.Hide();
        }


        

    }
}
