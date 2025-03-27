# Video Renting System

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
  ### Creation of the tables in SQL
  Please find the folder named ***Database*** in the remote repository. This folder contains the SQL statements for the craetion of the 3 tables named:

1. ***Users***: This is the table for registering new users and logging in existing users.

  2.***VideoDatabase***: This contains the information about the videos that will be displayed on the main page.
  
3.***VideoRentals***: This table is used to store all the videos rented by specific users.



