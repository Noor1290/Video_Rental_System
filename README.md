﻿# Video Renting System

This is a **Video Renting System** that allows users to browse, search, and rent videos. The system uses an **SQL database** to store and manage video data and user rental information. Upon login, the SQL database is transferred to a **hash table**, and all modifications are made to the hash table. When the user logs out, the updated hash table is then synced back to the SQL database.

---

## Features

- **Login System**: Users can log in and retrieve their video and rental data from the SQL database into a hash table.
- **Search Functionality**: Users can search for videos by title.
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
DO NOT forget to change the **server name** and **database name** in the C# program (Refer to the **Location of Server** and **database name** section in the C# code).

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

### Location of Sever Name in the Project
1. In Register, the server name and database name should be changed unless the same database name is used which is recommended.
The database name is ***VideoRentalSystem***
2. In Login, they are located on ***Line 53***
3. In Main Page, they are located on ***Line 429***

### Additional change needed to make (if database name is different)
In Main Page:

1.The Unlink Query on ***Line 700*** and ***Line 701*** needs to be changed.

2.The Clear Query on ***Line 712*** needs to be changed.

##  Navigation

### **1. Welcome Section**
- The application opens to a **welcome page** with 2 options:
  - **Users**
  - **Adminr**
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
- ***Add***: Allowing the admin to add videoes that will be saved in hashtable with validations checking whether the video name is already present to prevent duplications.
- ***Delete***: Allowing the admin to delete videos from the hashtable with validations checking whether the video title exists.
- ***Logout***: Upon clicking on this button, the program will uery SQL to store all additions and deletions made in the hashtable.

## 2. Users Section

### **1. Welcome Section for Users**
- The application opens to a **welcome page** for Users allowing them to:
  - Login
  - Register for new users

### **1. Register Section**
This section will prompt new users to enter their persoonal information:
- Username: It should be at least 5 characters long amd unique (validations)
- Email: It should contain symbols which will show on the screen when the user did not input the correct format.
- Among others

### **4. Login Section**
This page is for previously logged in user

### **5. Main Page**
This section allows the user to search for specific video through videoTitle or through Categories.
- The user will then click on their desired video which will open a pop up window displaying all the video information and a ***rent button***
- The user will then press on the rent button which will save it in hashtable which can be viewed in ***profile page***
- ***Logout Button*** : This button will save everything in all 3 hashtable to SQL permanently.
- ***Profile Page*** : This button will display all the videos rented by the logged in user
	 - All rented videos will be dispalyed in orange if timelimit has not been up yet.
	 -  All expired videos will be displayed in LightBlue.
---





