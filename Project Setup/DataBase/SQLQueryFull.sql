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

CREATE TABLE TestUser ( -- this is just a test table this wont be in the final product
    id INT IDENTITY(1,1) PRIMARY KEY,
    name VARCHAR(50) NOT NULL,
    password VARCHAR(255) NOT NULL,
    email VARCHAR(100) UNIQUE,
    phoneNum VARCHAR(20) UNIQUE
);

-- Inserts

-- Test User
INSERT INTO TestUser(name,password,email,phoneNum) VALUES ('First User', '12345', 'firstuser01@test.com', '05516667711');
INSERT INTO TestUser(name,password,email,phoneNum) VALUES ('Second User', '54321', 'seconduser02@test.com', '05526667722');
INSERT INTO TestUser(name,password,email,phoneNum) VALUES ('Third User', '67890', 'thirduser03@test.com', '05536667733');

GO

-- end of file

select * from TestUser;