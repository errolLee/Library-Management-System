-- Run this in MySQL/phpMyAdmin to set up the database

CREATE DATABASE IF NOT EXISTS library_db;
USE library_db;

-- Users table (Admin, Librarian, Borrower)
CREATE TABLE IF NOT EXISTS users (
    id INT AUTO_INCREMENT PRIMARY KEY,
    fullname VARCHAR(100) NOT NULL,
    username VARCHAR(50) NOT NULL UNIQUE,
    password VARCHAR(50) NOT NULL,
    role ENUM('Admin', 'Librarian', 'Borrower') NOT NULL
);

-- Books table
CREATE TABLE IF NOT EXISTS books (
    book_id INT AUTO_INCREMENT PRIMARY KEY,
    title VARCHAR(150) NOT NULL,
    author VARCHAR(100) NOT NULL,
    genre VARCHAR(50),
    quantity INT DEFAULT 1
);

-- Transactions table (borrow/return records)
CREATE TABLE IF NOT EXISTS transactions (
    transaction_id INT AUTO_INCREMENT PRIMARY KEY,
    user_id INT NOT NULL,
    book_id INT NOT NULL,
    borrow_date DATE NOT NULL,
    due_date DATE NOT NULL,
    return_date DATE DEFAULT NULL,
    FOREIGN KEY (user_id) REFERENCES users(id),
    FOREIGN KEY (book_id) REFERENCES books(book_id)
);

-- Sample admin account (username: admin, password: admin123)
INSERT INTO users (fullname, username, password, role) VALUES
('Administrator', 'admin', 'admin123', 'Admin');

-- Sample librarian account
INSERT INTO users (fullname, username, password, role) VALUES
('Juan Librarian', 'librarian1', 'lib123', 'Librarian');

-- Sample borrower account
INSERT INTO users (fullname, username, password, role) VALUES
('Maria Santos', 'maria', 'maria123', 'Borrower');

-- Sample books
INSERT INTO books (title, author, genre, quantity) VALUES
('The Great Gatsby', 'F. Scott Fitzgerald', 'Fiction', 3),
('To Kill a Mockingbird', 'Harper Lee', 'Fiction', 2),
('Introduction to C#', 'Andrew Troelsen', 'Technology', 5),
('Clean Code', 'Robert Martin', 'Technology', 4),
('Harry Potter', 'J.K. Rowling', 'Fantasy', 6);
