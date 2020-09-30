using IdentityServer4.Models;
using System;
using System.Windows.Forms;

namespace secureapiwrapper.sha256generator
{
    public partial class FormSha256Generator : Form
    {
        public FormSha256Generator()
        {
            InitializeComponent();
        }

        private void txtInput_TextChanged(object sender, EventArgs e)
        {
            string str = txtInput.Text;
            txtSha256.Text = str.Sha256();
        }
    }
}
