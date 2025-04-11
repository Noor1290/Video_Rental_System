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
    
    public partial class DeleteVideo : Form
    {
        private CustomHashTable videoData;
        public DeleteVideo(CustomHashTable videoData)
        {
            InitializeComponent();
            this.videoData = videoData;
        }


        private void txtDeleteVideo_TextChanged(object sender, EventArgs e)
        {
            string videoTitleToDelete = txtDeleteVideo.Text.Trim(); // Get the text entered by the user
            bool videoFound = false;
            bool isFullTitleEntered = false; // Flag to check if the full video title is entered

            // If the textbox is empty, clear any previous messages
            if (string.IsNullOrEmpty(videoTitleToDelete))
            {
                lblMessage.Text = "";  // Clear the message when the textbox is empty
                return;
            }

            // Check if the entered title matches any video in the hashtable
            foreach (KeyValuePair<string, object> entry in videoData)
            {
                CustomHashTable videoDetails = (CustomHashTable)entry.Value;
                string videoTitle = videoDetails["VideoTitle"].ToString();

                if (videoTitle.Equals(videoTitleToDelete, StringComparison.OrdinalIgnoreCase))  // Full match check
                {
                    videoFound = true;
                    isFullTitleEntered = true; // Full match is found
                    break;  // Exit the loop once a full match is found
                }
                else if (videoTitle.Contains(videoTitleToDelete, StringComparison.OrdinalIgnoreCase)) // Partial match check
                {
                    videoFound = true;
                }
            }

            // Provide feedback based on whether the video is found
            if (videoFound)
            {
                if (isFullTitleEntered)
                {
                    lblMessage.Text = "Video found! You can delete it now.";
                    lblMessage.ForeColor = Color.Green;  // Green message for a full match
                }
                else
                {
                    lblMessage.Text = "Please enter the full video title.";
                    lblMessage.ForeColor = Color.Red;  // Red message for partial match
                }
            }
            else
            {
                lblMessage.Text = "No matching video found.";
                lblMessage.ForeColor = Color.Red;  // Red message for no match
            }
        }




        private void Delete_Click(object sender, EventArgs e)
        {
            string videoTitleToDelete = txtDeleteVideo.Text.Trim(); // Get the video title entered by the user

            if (string.IsNullOrEmpty(videoTitleToDelete))
            {
                MessageBox.Show("Please enter a Video Title to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check if the video title exists in the hashtable
            bool videoFound = false;

            foreach (KeyValuePair<string, object> entry in videoData)
            {
                CustomHashTable videoDetails = (CustomHashTable)entry.Value;
                string videoTitle = videoDetails["VideoTitle"].ToString();

                if (videoTitle.Equals(videoTitleToDelete, StringComparison.OrdinalIgnoreCase))
                {
                    // Video found, delete the video
                    videoData.Remove(entry.Key);
                    videoFound = true;
                    MessageBox.Show($"Video '{videoTitleToDelete}' deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                }
            }

            // If the video is not found, display an error message
            if (!videoFound)
            {
                MessageBox.Show("No video found with the given title.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            AdminMain main = new AdminMain(videoData);
            main.Show();
            this.Close();

        }
        private void GoBackbutton_Click(object sender, EventArgs e)
        {
            AdminMain main = new AdminMain(videoData);
            main.Show();
            this.Close();
        }
    }
}
