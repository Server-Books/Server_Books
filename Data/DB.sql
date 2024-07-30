-- Active: 1722288378885@@bptfnq9kqbcvadwuhthr-mysql.services.clever-cloud.com@3306@bptfnq9kqbcvadwuhthr
CREATE TABLE Roles (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Role VARCHAR(255) NOT NULL
);

CREATE TABLE Users (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Names VARCHAR(255) NOT NULL,
    Password VARCHAR(255) NOT NULL,
    Email VARCHAR(255) UNIQUE NOT NULL,
    IdRole INT,
    FOREIGN KEY (IdRole) REFERENCES Roles(Id)
);

CREATE TABLE Books (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Title VARCHAR(255) NOT NULL,
    Author VARCHAR(255) NOT NULL,
    PublicationDate DATE NOT NULL,
    Status ENUM('Loaned', 'Available', 'NotAvailable') NOT NULL,
    CopiesAvailable INT NOT NULL
);

CREATE TABLE BookLending (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    DateOfLoan DATE NOT NULL,
    DateOfReturn DATE NOT NULL,
    Status ENUM('Approved', 'Denied') NOT NULL,
    UserId INT,
    BookId INT,
    FOREIGN KEY (UserId) REFERENCES Users(Id),
    FOREIGN KEY (BookId) REFERENCES Books(Id)
);

CREATE TABLE Gender (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Gender VARCHAR(255) NOT NULL
);

CREATE TABLE GenderBooks (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    GenderId INT,
    BookId INT,
    FOREIGN KEY (GenderId) REFERENCES Gender(Id),
    FOREIGN KEY (BookId) REFERENCES Books(Id)
);

-- Insertar Roles
INSERT INTO Roles (Role) VALUES 
('Client'), 
('Admin');

-- Insertar Usuarios
INSERT INTO Users (Names, Password, Email, IdRole) VALUES 
('Alice Johnson', 'password123', 'alice.johnson@example.com', 1), -- Client
('Bob Smith', 'admin456', 'bob.smith@example.com', 2); -- Admin

-- Insertar Libros
INSERT INTO Books (Title, Author, PublicationDate, Status, CopiesAvailable) VALUES 
('The Great Gatsby', 'F. Scott Fitzgerald', '1925-04-10', 'Available', 3),
('To Kill a Mockingbird', 'Harper Lee', '1960-07-11', 'Available', 2),
('1984', 'George Orwell', '1949-06-08', 'Loaned', 1); -- Loaned to Alice

-- Insertar Préstamos de Libros
INSERT INTO BooksLending (DateOfLoan, DateOfReturn, Status, UserId, BookId) VALUES 
('2024-07-01', '2024-07-15', 'Approved', 1, 3); -- Alice ha prestado '1984'

-- Insertar Géneros
INSERT INTO Gender (Gender) VALUES 
('Fiction'), 
('Non-Fiction');

-- Insertar Géneros para Libros
INSERT INTO GenderBooks (GenderId, BookId) VALUES 
(1, 1), -- 'The Great Gatsby' es de 'Fiction'
(1, 2), -- 'To Kill a Mockingbird' es de 'Fiction'
(1, 3); -- '1984' es de 'Fiction'


SELECT * FROM Users;