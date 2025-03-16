using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoRentalSystem
{
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]

    public partial class Main : Form
    {
        private CustomHashTable userInfo;
        private CustomHashTable videoData;
        private CustomHashTable videoRentals;
        private CustomHashTable uploads;
        public Main(CustomHashTable userInfo, CustomHashTable videoData, CustomHashTable videoRentals, CustomHashTable uploads)
        {
            this.userInfo = userInfo;
            this.videoData = videoData;
            this.videoRentals = videoRentals;
            this.uploads = uploads;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void Logout_CLick(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Order_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            ProfilePage profile = new ProfilePage();
            profile.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }
    }
}
