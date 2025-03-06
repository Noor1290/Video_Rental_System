using System;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;

namespace VideoRentalSystem
{
    public partial class SearchForm : Form
    {
        //UI components
        private TextBox SearchTextBox;
        private Button SearchButton;
        private DataGridView DataGridView;
        private DataTable DataTable; // temporarily video data

        public SearchForm()
        {
            InitialiseUI(); // setup UI components
            InitialiseDataTable(); // Load sample data
        }
        private void InitialiseUI()
        {
            //set form properties
            this.Text = "Search Form";
            this.Size = new System.Drawing.Size(500, 350);

            //Search Input field
            SearchTextBox = new TextBox()
            {
                Location = new System.Drawing.Point(20,20),
                Width=300,
                Height=50
            };

            //search button
            SearchButton = new Button()
            {
                Text = "Search",
                Location = new System.Drawing.Point(330,  18),
                Width= 80,
                Height=30
            };

            //SearchButton.Click += SearchButton_Click; // attach click event handler

            // data grid to display results
            DataGridView = new DataGridView()
            {
                Location = new System.Drawing.Point(20, 60),
                Width = 440,
                Height=220,
                AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.Fill   
            };

            //add components to the form
            this.Controls.Add( SearchTextBox );
            this.Controls.Add( SearchButton );
            this.Controls.Add( DataGridView );
        }
        private void InitialiseDataTable()
        {
            //temporary data

        }
    }
}
