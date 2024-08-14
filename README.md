ToDoDB Overview:
ToDoDB is a simple database designed for managing a to-do list. You will need a database to work from. This README provides instructions on how to create the database and its table, as well as details about the table's schema.

Database Setup
Follow these instructions to create the ToDoDB database and the ToDo table in MySQL.

1. Create the Database
To create the ToDoDB database, use the following SQL command:

sql
Copy code
CREATE DATABASE ToDoDB;

2. Use the Database
Select the ToDoDB database to use for subsequent operations:

sql
Copy code
USE ToDoDB;

3. Create the Table
Create the ToDo table with the following schema:

sql
Copy code
CREATE TABLE ToDo (
    id INT AUTO_INCREMENT PRIMARY KEY,
    task VARCHAR(255),
    status VARCHAR(50),
    schedule VARCHAR(100)
);

Table Schema
The ToDo table has the following columns:

id: An INT column that auto-increments and serves as the primary key for the table.

task: A VARCHAR(255) column to store the task description.

status: A VARCHAR(50) column to store the status of the task (e.g., "Pending", "Completed").

schedule: A VARCHAR(100) column to store scheduling information for the task (e.g., date or time).

Usage
Once the database and table are created, you can start inserting, updating, and querying tasks using SQL commands. For example, to insert a new task, you might use:

sql
Copy code
INSERT INTO ToDo (task, status, schedule) VALUES ('Finish report', 'Pending', 'Saturday');
