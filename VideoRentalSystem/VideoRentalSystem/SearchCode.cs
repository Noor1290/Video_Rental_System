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
            this.Size = new System.Drawing.Size(600, 450);
            this.BackColor = Color.LightBlue;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new Font("Segeo UI", 10);


            //Search Input field
            SearchTextBox = new TextBox()
            {
                Location = new System.Drawing.Point(20,20),
                Width=380,
                Height=30,
                Font = new Font("Segeo UI", 10),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.WhiteSmoke
            };

            //search button
            SearchButton = new Button()
            {
                Text = "🔍 Search",
                Location = new System.Drawing.Point(420,  18),
                Width= 140,
                Height=35,
                Font = new Font("Segeo UI", 10, FontStyle.Bold),
                BackColor = Color.DodgerBlue,
                ForeColor = Color.White,
                Cursor = Cursors.Hand

            };

            SearchButton.FlatAppearance.BorderSize = 0;
            SearchButton.Click += SearchButton_Click; // attach click event handler

            // data grid to display results
            DataGridView = new DataGridView()
            {
                Location = new System.Drawing.Point(20, 60),
                Width = 440,
                Height = 220,
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Top,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                AllowUserToAddRows = false,
                ReadOnly = true,
                EnableHeadersVisualStyles = false,

                DefaultCellStyle = new DataGridViewCellStyle()
                {
                    Font = new Font("Segeo UI", 10),
                    ForeColor = Color.DarkSlateGray,
                    SelectionBackColor = Color.LightSkyBlue,
                    SelectionForeColor = Color.Black,
                },

                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle() {
                    Font = new Font("Segeo UI", 10),
                    ForeColor = Color.White,
                    BackColor = Color.SteelBlue,
                    Alignment= DataGridViewContentAlignment.MiddleCenter

                }

            };

            //add components to the form
            this.Controls.Add( SearchTextBox );
            this.Controls.Add( SearchButton );
            this.Controls.Add( DataGridView );
        }
        private void InitialiseDataTable()
        {
            //temporary data
            DataTable = new DataTable();
            DataTable.Columns.Add("ID",typeof(int));
            DataTable.Columns.Add("Video Name", typeof(string));
            DataTable.Columns.Add("Genre", typeof(string));
            DataTable.Columns.Add("Year", typeof(int));
            DataTable.Columns.Add("Category", typeof(string));

            DataTable.Rows.Add(1, "aaa", "Horror", 2001, "Movie");
            DataTable.Rows.Add(2, "bbb", "Romance", 2008, "Movie");
            DataTable.Rows.Add(3, "ccc", "Horror", 2001, "TV Show");
            DataTable.Rows.Add(4, "dd", "Horror", 2002, "Movie");

            DataGridView.DataSource = DataTable;

        }

        private void SearchButton_Click( object sender, EventArgs e)
        {
            string SearchText = SearchTextBox.Text.Trim().ToLower();

            //check if any input is entered
            if (string.IsNullOrEmpty(SearchText)) { 
                MessageBox.Show("No input","Error",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
            var FilteredRows = DataTable.AsEnumerable()
               .Where(row =>
               row.Field<string>("Video Name").ToLower().Contains(SearchText) ||
               row.Field<string>("Genre").ToLower().Contains(SearchText) ||
               row.Field<int>("Year").ToString().Contains(SearchText) ||
               row.Field<string>("Category").ToLower().Contains(SearchText)
               );

            //display
            if (FilteredRows.Any()) { 
                DataGridView.DataSource = FilteredRows.CopyToDataTable();
                
            }
            else
            {
                MessageBox.Show("No results", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DataGridView.DataSource = null;
            }
        }
    }
}
