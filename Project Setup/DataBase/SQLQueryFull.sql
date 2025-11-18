--SQL Query of teknikservisotomasyonuproje

-- Database
-- if database exists drop it
USE master;
GO
IF DB_ID('teknikServisOtomasyonDB') IS NOT NULL
BEGIN
    ALTER DATABASE teknikServisOtomasyonDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE teknikServisOtomasyonDB;
END
GO

-- create the database
CREATE DATABASE teknikServisOtomasyonDB;
GO

-- to query the database we created
USE teknikServisOtomasyonDB;
GO

-- Tables

CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    Role NVARCHAR(20) NOT NULL
        CHECK (Role IN ('Customer', 'Staff', 'Admin')),
    Email NVARCHAR(100) UNIQUE NOT NULL,
    PasswordHash NVARCHAR(255) NOT NULL,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    Phone NVARCHAR(20) UNIQUE NOT NULL,
    CreatedAt DATETIME DEFAULT(GETDATE())
);

CREATE TABLE ServiceRecords (
    ServiceID INT PRIMARY KEY IDENTITY(1,1),
    CustomerID INT NOT NULL,
    AssignedStaffID INT NULL,
    DeviceType NVARCHAR(50),
    Brand NVARCHAR(50),
    Model NVARCHAR(50),
    SerialNumber NVARCHAR(50),
    ProblemDescription NVARCHAR(MAX),
    Status NVARCHAR(50) NOT NULL CHECK (Status IN (
        'Talep Alýndý',
        'Personele Atandý',
        'Ýþlemde',
        'Tamamlandý',
        'Teslime Hazýr',
        'Teslim Edildi',
        'Ýptal Edildi'
    )),
    CreatedAt DATETIME DEFAULT(GETDATE()),
    ClosedAt DATETIME NULL,
    FOREIGN KEY (CustomerID) REFERENCES Users(UserID),
    FOREIGN KEY (AssignedStaffID) REFERENCES Users(UserID)
);

CREATE TABLE ServiceOperations (
    OperationID INT PRIMARY KEY IDENTITY(1,1),
    ServiceID INT NOT NULL,
    Description NVARCHAR(MAX),
    Cost DECIMAL(10,2),
    PerformedAt DATETIME DEFAULT(GETDATE()),
    FOREIGN KEY (ServiceID) REFERENCES ServiceRecords(ServiceID)
);

CREATE TABLE ServiceComments (
    CommentID INT PRIMARY KEY IDENTITY(1,1),
    ServiceID INT NOT NULL,
    CustomerID INT NOT NULL,
    Rating INT CHECK (Rating BETWEEN 1 AND 5),
    CommentText NVARCHAR(MAX),
    CreatedAt DATETIME DEFAULT(GETDATE()),
    FOREIGN KEY (ServiceID) REFERENCES ServiceRecords(ServiceID),
    FOREIGN KEY (CustomerID) REFERENCES Users(UserID)
);


-- Inserts

-- Users
INSERT INTO Users (Role, Email, PasswordHash, FirstName, LastName, Phone)
VALUES 
 ('Customer', 'ilk@test.com', '123456789', 'Ahmet', 'Yýlmaz', '5551234567');


GO

-- end of file

select * from Users;