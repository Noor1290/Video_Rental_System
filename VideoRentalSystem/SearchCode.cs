//imports
using System;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Collections; // for hash table

namespace VideoRentalSystem
{
    public partial class SearchForm : Form
    {
        //UI components
        private TextBox SearchTextBox;
        private Button ClearButton;
        private DataGridView DataGridView;
        private DataTable DataTable; // temporarily video data
        private Label NoResultsLabel;
        private CustomHashTable VideoHashTable = new CustomHashTable(10000);

        public SearchForm()
        {
            InitialiseUI(); // setup UI components
            InitialiseDataTable(); // Load sample data
        }
        private void InitialiseUI()
        {
            //set form properties
<<<<<<< HEAD
            this.Text = "Video Search";
            this.Size = new System.Drawing.Size(650, 500);
            this.BackColor = Color.WhiteSmoke;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new Font("Segeo UI", 10);

=======
            this.Text = "Search Form";
            this.Size = new System.Drawing.Size(500, 350);
>>>>>>> 42f68fe98dc71d7bb7fbd4b2c0098331302de2ad

            //Search Input field
            SearchTextBox = new TextBox()
            {
                Location = new System.Drawing.Point(20,20),
<<<<<<< HEAD
                Width=450,
                Height=30,
                Font = new Font("Segeo UI", 12),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.WhiteSmoke,
                ForeColor = Color.Black,
=======
                Width=300,
                Height=50
>>>>>>> 42f68fe98dc71d7bb7fbd4b2c0098331302de2ad
            };
            //live search
            SearchTextBox.TextChanged += SearchTextBox_TextChanged;

            //search button
            ClearButton = new Button()
            {
<<<<<<< HEAD
                Text = "❌ Clear",
                Location = new System.Drawing.Point(490,  18),
                Width= 120,
                Height=35,
                Font = new Font("Segeo UI", 10, FontStyle.Bold),
                BackColor = Color.Crimson,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand

            };

            ClearButton.FlatAppearance.BorderSize = 0;
            ClearButton.Click += ClearButton_Click; // attach click event handler
=======
                Text = "Search",
                Location = new System.Drawing.Point(330,  18),
                Width= 80,
                Height=30
            };

            SearchButton.Click += SearchButton_Click; // attach click event handler
>>>>>>> 42f68fe98dc71d7bb7fbd4b2c0098331302de2ad

            // data grid to display results
            DataGridView = new DataGridView()
            {
<<<<<<< HEAD
                Location = new System.Drawing.Point(20, 70),
                Width = 600,
                Height = 350,
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
                    SelectionForeColor = Color.Black
                },

                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle() {
                    Font = new Font("Segeo UI", 11, FontStyle.Bold),
                    ForeColor = Color.White,
                    BackColor = Color.SteelBlue,
                    Alignment= DataGridViewContentAlignment.MiddleCenter

                }

=======
                Location = new System.Drawing.Point(20, 60),
                Width = 440,
                Height=220,
                AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.Fill   
>>>>>>> 42f68fe98dc71d7bb7fbd4b2c0098331302de2ad
            };

            NoResultsLabel = new Label()
            {
                Text = "No results found",
                ForeColor = Color.Red,
                Font = new Font("Segeo UI", 12, FontStyle.Bold),
                AutoSize= true,
                //hidden by default
                Visible = false
            };
            //dynamically position it at the center 
            PositionNoResultsLabel();

            //add components to the form
            this.Controls.Add( SearchTextBox );
            this.Controls.Add( NoResultsLabel );
            this.Controls.Add( ClearButton );
            this.Controls.Add( DataGridView );
        }

        private void PositionNoResultsLabel()
        {
            int gridX = DataGridView.Location.X;
            int gridY = DataGridView.Location.Y;
            int gridWidth = DataGridView.Width;
            int gridHeight = DataGridView.Height;

            //position in the center
            NoResultsLabel.Location = new Point( gridX + (gridWidth /2) - (NoResultsLabel.Width/2), gridY +(gridHeight/2 )- (NoResultsLabel.Height/2));
        }
        private void InitialiseDataTable()
        {
            //temporary data
            DataTable = new DataTable();
            DataTable.Columns.Add("ID",typeof(int));
            DataTable.Columns.Add("Genre", typeof(string));
            DataTable.Columns.Add("Video Name", typeof(int));
            DataTable.Columns.Add("Year", typeof(int));
            DataTable.Columns.Add("Category", typeof(string));

            VideoHashTable = new CustomHashTable(10000);
            AddVideo(1, "aaa", "Horror", 2001, "Movie");
            AddVideo(2, "bbb", "Romance", 2008, "Movie");
            AddVideo(3, "ccc", "Horror", 2001, "TV Show");
            AddVideo(4, "dd", "Horror", 2002, "Movie");

            DataGridView.DataSource = DataTable;

        }

        //add video data  in hashtable
        private void AddVideo(int id, string name, string genre, int year, string category)
        {
            var VideoData = new { ID = id, Name = name, Genre = genre, Year = year, Category = category };
            //store in hashtable
            VideoHashTable[id] = VideoData;
            DataTable.Rows.Add(id, name, genre, year, category);
        }
        private void SearchTextBox_TextChanged(object sender, EventArgs e)
        {

            string SearchText = SearchTextBox.Text.Trim().ToLower();

            //check if any input is entered
            if (string.IsNullOrEmpty(SearchText))
            {
                ResetSearch();
                return;
            }
            var FilteredRows = DataTable.AsEnumerable()
               .Where(row =>
               row.Field<string>("Video Name").ToLower().Contains(SearchText) ||
               row.Field<string>("Genre").ToLower().Contains(SearchText) ||
               row.Field<int>("Year").ToString().Contains(SearchText) ||
               row.Field<string>("Category").ToLower().Contains(SearchText) ||
               );

            //display
            if (FilteredRows.Any())
            {
                DataGridView.DataSource = FilteredRows.CopyToDataTable();
<<<<<<< HEAD
                NoResultsLabel.Visible = false;

            }
            else
            {
                var EmptyTable = DataTable.Clone();
                DataGridView.DataSource = EmptyTable;
                PositionNoResultsLabel();
                NoResultsLabel.Visible = true;
=======
                MessageBox.Show("No results", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DataGridView.DataSource= null
>>>>>>> 42f68fe98dc71d7bb7fbd4b2c0098331302de2ad
            }
        }
        private void ClearButton_Click( object sender, EventArgs e)
        {
            ResetSearch();
        }

        private void ResetSearch()
        {
            SearchTextBox.Clear();
            DataGridView.DataSource = DataTable;
            NoResultsLabel.Visible = false;
        }
    }
}
