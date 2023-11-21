using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CalenderForProject
{
    public partial class FormCreateCodecs : Form
    {
        public FormCreateCodecs()
        {
            InitializeComponent();
        }
        //Üretilen koda göre ana dosya oluşacak bu yüzden bu kısımda yer alması geç olur dosyalama kısmında bunu hallet!!!!
        private void FormCreateCodecs_Load(object sender, EventArgs e)
        {

            tBoxCode.Text = FormTitle.rastgeleKod;
        }
       

        private void btnCopy_Click(object sender, EventArgs e)
        {
            
            Clipboard.SetText(tBoxCode.Text);
            MessageBox.Show("Metin panoya kopyalandı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        
    }
}
