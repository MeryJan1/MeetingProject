using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CalenderForProject
{
    public partial class Form1 : Form //server form
    {
        

        public Form1()
        {
            InitializeComponent();
        }


        private void btnJoin_Click(object sender, EventArgs e)
        {
            FormJoinwithCode joinwithCode = new FormJoinwithCode();
            joinwithCode.Show();
            this.Hide();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            FormLogin formLogin = new FormLogin();
            formLogin.Show();
            this.Hide();
        }

        
    }
}
