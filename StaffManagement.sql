
/* Create Database and Basic Structure Required for the application */

CREATE DATABASE StaffManagement; /* This would be on master */

/* Create Users Table */
CREATE TABLE Users (
	UserId int IDENTITY(1,1) PRIMARY KEY,
	UserName nvarchar(255) NOT NULL,
	Password nvarchar(255) NOT NULL,
	RoleId smallint NOT NULL, 
	DailyCheckInTime time NOT NULL,
	DailyCheckOutTime time NOT NULL
)

/* Create Roles Table */
CREATE TABLE Roles(
	RoleId smallint PRIMARY KEY,
	RoleName nvarchar(255) NOT NULL
)

/* Create Default Values for Roles */
INSERT INTO Roles Values (1, 'Admin');
INSERT INTO Roles Values (2, 'Staff');

/* Maintain Relation between User and their Role */
ALTER TABLE Users
ADD FOREIGN KEY (RoleId) REFERENCES Roles(RoleId);

/* Creating Tasks Table to Maintain Tasks */
CREATE TABLE Tasks(
	Taskid bigint IDENTITY(1,1) PRIMARY KEY,
	UserId int NOT NULL,
	TaskName nvarchar(255) NOT NULL,
	TaskDate date NOT NULL,
	NumberOfHours smallInt NOT NULL,
	StartTime time NOT NULL,
	EndTime time NOT NULL,
	Comments nvarchar(1000)
	FOREIGN KEY (UserId) REFERENCES Users(UserId) 
)

/* Default Admin User */
INSERT INTO Users (UserName, Password, RoleId, DailyCheckInTime, DailyCheckOutTime) VALUES ('Admin', 'Admin', 1, '9:00', '17:00');