# Video Renting System

This is a **Video Renting System** that allows users to browse, search, and rent videos. The system uses an **SQL database** to store and manage video data and user rental information. Upon login, the SQL database is transferred to a **hash table**, and all modifications are made to the hash table. When the user logs out, the updated hash table is then synced back to the SQL database.

---

## Features

- **Login System**: Users can log in and retrieve their video and rental data from the SQL database into a hash table.
- **Search Functionality**: Users can search for videos by title, price, genre and duration.
- **Rent Videos**: Users can rent videos, and the system will update rental information.
- **Database Integration**: The system integrates an SQL database.
- **Modifications via Hash Table**: All data manipulations (like renting videos) are done in memory via hash tables. The changes are later pushed back to the SQL database when the user logs out.

---

## Prerequisites

Before getting started, ensure you have the following:

- **SQL Database**: You must have an **SQL database** for storing user and video data. (We have used **SSMS** – SQL Server Management Studio).
- **.NET Framework**: The project uses one of the latest **.NET Framework** versions.
- **Git**: Version control for collaboration.

---

## SQL Connection Setup

Follow these steps to connect the project to your SQL server:

1. **Select Tools** > **Connect to Database**.
2. Choose **Microsoft SQL Server**.
3. Enter the **name of the server**.
4. Select your **database name**.
5. Check **Trust Server Certificate**.
6. Click **Test Connection** to ensure it's working.

**Important**:  
DO NOT forget to change the **server name** and **database name** in the C# program.

---

## Installation

### 1. Install Required Packages

To interact with the SQL database, you'll need to install the `Microsoft.Data.SqlClient` package:

1. **Right-click** on the solution project.
2. Choose **Manage NuGet Packages**.
3. Browse for **Microsoft.Data.SqlClient** and click **Install**.

---

### 2. Pull the Remote Repository to Your Local Machine

To open this repository:

1. Create a **new folder** on your desktop.
2. **Right-click** the folder and select **Open Command Prompt** or **PowerShell**.

```
git init
```

3. Connect the remote repository to the local one.
 ```
 git remote add origin git remote add origin https://github.com/Noor1290/Video_Rental_System.git

 ```
 4. Check whether the remote repository has been correctly added to the local repository
	```
	git remote -v
	 ```
	Note: The result should be as follows:
	```
	origin  https://github.com/Noor1290/Video_Rental_System.git (fetch)
	origin  https://github.com/Noor1290/Video_Rental_System.git (push)
	```
5. Pull it into the local repository
 ```
 git pull origin main

 ```
 ##  Running the Application

### **1. Open Project in Visual Studio**
- Navigate to the project folder and open **`VideoRentalSystem.sln`** in **Visual Studio**.

### **2. Build & Compile**
- In **Visual Studio**, go to:
  - `Build` → `Build Solution` (`Ctrl + Shift + B`).

### **3. Run the Application**
- Press on the run button or press F5

  ### Creation of the tables in SQL
  Please find the folder named ***Database*** in the remote repository. This folder contains the SQL statements for the craetion of the 3 tables named:

1. ***Users***: This is the table for registering new users and logging in existing users.

  2.***VideoDatabase***: This contains the information about the videos that will be displayed on the main page.
  
3.***VideoRentals***: This table is used to store all the videos rented by specific users.

### Server Name and Database Name
- Please make sure to change the server name in Login, Register, Main page for users 
- As well as for Admin Page in LoginAdmin and AdminMain
- Please Make Sure to use the same database name: <span style="color:lightgreen;">***VideoRentalSystem***</span> since the name VideoRentalSystem is also used in the logout function

### How to locate the database name in the above files
Please press
-  `Ctrl + F`
- Then search for the server name

##  Navigation

### **1. Welcome Section**
- The application opens to a **welcome page** with 2 options:
  - **Users**
  - **Admin**
## 1. Admin Section
### **1. Welcome Section for Admin**
 The application opens to a **welcome page** for Admin user allowing him to:
  - Add video
  - Delete video

### **2. Login Section for Admin**
 ***Note***: The Admin user credentials are as follows:
- **Username**: <span style="color:lightgreen;">admin</span>
- **Password**: <span style="color:lightgreen;">admin123</span>
- ### **3. Main Page for Admin**
There are 3 buttons on the Admin's main page:
- ***Add***: Allowing the admin to add videos that will be saved in hashtable with validations checking whether the video name is already present to prevent duplications.
- ***Delete***: Allowing the admin to delete videos from the hashtable with validations checking whether the video title exists.
- ***Logout***: Upon clicking on this button, the program will query SQL to store all additions and deletions made in the hashtable.

## 2. Users Section

### **1. Welcome Section for Users**
- The application opens to a **welcome page** for Users allowing them to:
  - Login
  - Register for new users

### **1. Register Section**
This section will prompt new users to enter their personal information:
- Username: It should be at least 5 characters long and unique (validations)
- Email: It should contain symbols which will show on the screen when the user did not input the correct format.
- Among others

### **4. Login Section**
- This page is for previously logged in user
- Please note that the text file input box will allow the user to upload a text file containing his database for the main table.
- Validations have been used to 
	- Verify whether the database is empty and to check for duplicates.
	- The user will have to copy the full path of the text file called ***Database.txt*** using
	- `Ctrl + Shift + C` or by right clicking on the text file and looking for copy as path.
	 - <span style="color:red;">**ERROR**</span> may arise when copying the path with its double quotes, which needs to be removed.

### **5. Main Page**
This section allows the user to search for specific video through videoTitle or through Categories.
- The user will then click on their desired video which will open a pop up window displaying all the video information and a ***rent button***
- The user will then press on the rent button which will save it in hashtable which can be viewed in ***profile page***
- ***Logout Button*** : This button will save everything in all 3 hashtable to SQL permanently.
- ***Profile Page*** : This button will display all the videos rented by the logged in user
	### Video Status:

- **Rented videos** will be displayed in <span style="color:orange;">orange</span> if the time limit has not been reached yet.
- **Expired videos** will be displayed in <span style="color:cyan;">light blue</span>.

### **6.Search Page**
This section allows the user to search specific video through video title, price, genre and duration.
- It is found on top right corner of the main page.
- It initially displays a table of available videos along with their respective data.
- It will performs a live search.
- The user can input the video title, genre or the duration in the search box.
- The user can also input the minimum price and maximum price that is of his interest.
- The table of videos will be adjusted according to what the user types in the search box
- If the video is not found, an error message is displayed in red.
- The user can click the clear button to erase content in the search box and the table of videos will appear.
- The user can also return by clicking on the go back button.
### Instructions:

- When a video is rented, it will show in **orange** until the time limit is up.
- Once the time limit has passed, the video will be marked as **expired** and displayed in **light blue**.
---





