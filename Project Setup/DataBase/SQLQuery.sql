--SQL Query of teknikservisotomasyonuproje

-- Tables

DROP TABLE TestUser;
CREATE TABLE TestUser (
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