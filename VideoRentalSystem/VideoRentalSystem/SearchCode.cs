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
        private NumericUpDown MinPriceBox, MaxPriceBox;
        private Button ClearButton;
        private DataGridView DataGridView;
        private DataTable DataTable; // temporarily video data
        private Label NoResultsLabel, MinPriceLabel, MaxPriceLabel,SearchLabel;
        //data storage
        private Dictionary <int, DataRow> VideoHashTable;

        public SearchForm()
        {
            InitialiseUI(); // setup UI components
            InitialiseDataTable(); // Load sample data
        }
        private void InitialiseUI()
        {
            //set form properties
            this.Text = "Video Search";
            this.Size = new Size(700, 500);
            this.BackColor = Color.WhiteSmoke;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new Font("Segeo UI", 10);

            // search label
            SearchLabel = new Label()
            {
                Text="Search:",
                Location = new Point(15,20),
                AutoSize = true,
                Font = new Font("Segeo UI", 10, FontStyle.Bold)
            };
            //Search Input field
            SearchTextBox = new TextBox()
            {
                Location = new Point(90,15),
                Width=250,
                Font = new Font("Segeo UI", 12),
                BorderStyle = BorderStyle.FixedSingle
            };
            //live search
            SearchTextBox.TextChanged += SearchTextBox_TextChanged;

            //minimum price label
            MinPriceLabel = new Label()
            {
                Text="Min Price:",
                Location= new Point(340,20),
                AutoSize= true,
                Font = new Font("Segeo UI", 10, FontStyle.Bold)
            };

            //minimum price input
            MinPriceBox = new NumericUpDown()
            {
                Location= new Point(440,15),
                Width=80,
                Minimum=0,
                Maximum=1000,
                DecimalPlaces=2,
                Font = new Font("Segeo UI", 10)
            };

            MinPriceBox.ValueChanged += SearchTextBox_TextChanged; // refilter upon value changes
            
            //maximum price label
            MaxPriceLabel = new Label()
            {
                Text = "Max Price:",
                Location = new Point(340, 60),
                AutoSize = true,
                Font = new Font("Segeo UI", 10, FontStyle.Bold)
            };

            //maximum price input
            MaxPriceBox = new NumericUpDown()
            {
                Location = new Point(440, 55),
                Width = 80,
                Minimum = 0,
                Maximum = 1000,
                DecimalPlaces = 2,
                Font = new Font("Segeo UI", 10)
            };

            MaxPriceBox.ValueChanged += SearchTextBox_TextChanged; // refilter upon value changes

            //search button
            ClearButton = new Button()
            {
                Text = "❌ Clear",
                Location = new System.Drawing.Point(550,  35),
                Width= 120,
                Height=40,
                BackColor = Color.Crimson,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };

            ClearButton.FlatAppearance.BorderSize = 0;
            ClearButton.Click += ClearButton_Click; // attach click event handler

            // data grid to display results
            DataGridView = new DataGridView()
            {
                Location = new System.Drawing.Point(20, 100),
                Width = 640,
                Height = 300,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                AllowUserToAddRows = false,
                ReadOnly = true,
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

            //add components to the form
            this.Controls.Add(SearchLabel);
            this.Controls.Add( SearchTextBox );
            this.Controls.Add(MinPriceLabel);
            this.Controls.Add(MaxPriceLabel);
            this.Controls.Add(MinPriceBox);
            this.Controls.Add(MaxPriceBox);
            this.Controls.Add( NoResultsLabel );
            this.Controls.Add( ClearButton );
            this.Controls.Add( DataGridView );

            //dynamically position it at the center 
            PositionNoResultsLabel();
        }

        private void PositionNoResultsLabel()
        {
            //position in the center
            NoResultsLabel.Location = new Point(
                DataGridView.Location.X + (DataGridView.Width / 2 ) -(NoResultsLabel.Width/2),
                 DataGridView.Location.Y + (DataGridView.Height / 2) - (NoResultsLabel.Height / 2)
                );

            NoResultsLabel.BringToFront();
        }
        private void InitialiseDataTable()
        {
            //temporary data
            DataTable = new DataTable();
            DataTable.Columns.Add("ID",typeof(int));
            DataTable.Columns.Add("Video", typeof(string));
            DataTable.Columns.Add("Genre", typeof(string));
            DataTable.Columns.Add("Duration (min)", typeof(int));
            DataTable.Columns.Add("Price ($)", typeof(decimal));

            //initialise hash table
            VideoHashTable = new Dictionary <int ,DataRow>();

            //add sample video
            AddVideo(1, "aaa", "Horror", 148,5.99m);
            AddVideo(2, "bbb", "Romance", 451,7.48m);
            AddVideo(3, "ccc", "Horror", 110,7.23m);
            AddVideo(4, "dd", "Horror", 98,4.56m);

            DataGridView.DataSource = DataTable;

        }

        //add video data  in hashtable
        private void AddVideo(int id, string name, string genre, int duration, decimal price)
        {
            DataRow row = DataTable.NewRow();
            row["ID"] = id;
            row["Video"] = name;
            row["Genre"] = genre;
            row["Duration (min)"] = duration;
            row["Price ($)"]= price;

            DataTable.Rows.Add(row);
            VideoHashTable[id] = row; // store in hashtable
        }
        private void SearchTextBox_TextChanged(object sender, EventArgs e)
        {

            string SearchText = SearchTextBox.Text.Trim().ToLower();
            decimal MinPrice = MinPriceBox.Value;
            decimal MaxPrice = MaxPriceBox.Value == 0 ? 1000 : MaxPriceBox.Value;



            var FilteredRows = VideoHashTable.Values
                .Where(row =>
                {
                    decimal price = row.Field<decimal>("Price ($)");
                    bool MatchSearch = row.Field<string>("Video").ToLower().Contains(SearchText) ||
                                       row.Field<string>("Genre").ToLower().Contains(SearchText) ||
                                       row.Field<int>("Duration (min)").ToString().Contains(SearchText);
                    bool MatchPrice = price>= MinPrice && price<= MaxPrice;
                    return MatchSearch && MatchPrice;

                })
                .ToList();
 


            //display
            if (FilteredRows.Any())
            {
                DataTable FilteredTable = DataTable.Clone();

                foreach ( var row in FilteredRows)
                    FilteredTable.ImportRow(row);

                DataGridView.DataSource = FilteredTable;
                NoResultsLabel.Visible = false;
            }
            else
            {
                DataGridView.DataSource = DataTable.Clone();
                PositionNoResultsLabel();
                NoResultsLabel.Visible = true;
            }
        }

        //clears the search input and reset filters
        private void ClearButton_Click( object sender, EventArgs e)
        {

            SearchTextBox.Clear();
            MinPriceBox.Value = 0;
            MaxPriceBox.Value = 0;
            DataGridView.DataSource = DataTable;
            NoResultsLabel.Visible = false;

        }
    }
}
