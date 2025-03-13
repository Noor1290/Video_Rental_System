using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VideoRentalSystem;


namespace Last
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Register_Click(object sender, EventArgs e)
        {
            Register register = new Register();
            register.Show();
            this.Hide();

        }

        private void Close_CLick(object sender, EventArgs e)
        {
            Close();
        }

        private void Reset_Click(object sender, EventArgs e)
        {
            Reset reset = new Reset();
            reset.Show();
            this.Hide();

        }

        private void Login_Click(object sender, EventArgs e)
        {
            Main main = new Main();
            main.Show();
            this.Hide();

        }
    }
}
