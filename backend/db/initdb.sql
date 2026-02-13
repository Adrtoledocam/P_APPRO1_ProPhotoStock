CREATE DATABASE IF NOT EXISTS db_prophotostock;

USE db_prophotostock;

CREATE TABLE t_user (
    userId INT AUTO_INCREMENT PRIMARY KEY,
    useName VARCHAR(50) NOT NULL,
    useEmail VARCHAR(100) NOT NULL UNIQUE,
    useRole VARCHAR(20) NOT NULL,
    usePassword CHAR(100) NOT NULL    
);

CREATE TABLE t_photo (
    photoId INT AUTO_INCREMENT PRIMARY KEY,
    phoTitle VARCHAR(255) NOT NULL,
    phoUrl Text,    
    phoDateUpload DATE,
    phoVisible BOOLEAN DEFAULT TRUE,
    userId INT NOT NULL,
    FOREIGN KEY(userId) REFERENCES t_user(userId)
);
CREATE TABLE t_tag (
    tagId INT AUTO_INCREMENT PRIMARY KEY,
    tagName VARCHAR(50) NOT NULL
);
CREATE TABLE t_tagPhoto(
    photoId INT NOT NULL,
    tagId INT NOT NULL,
    PRIMARY KEY(photoId, tagId),
    FOREIGN KEY(photoId) REFERENCES t_photo(photoId),
    FOREIGN KEY(tagId) REFERENCES t_tag(tagId)
);

CREATE TABLE t_contract (
    contractId INT AUTO_INCREMENT PRIMARY KEY,
    conType VARCHAR(20) NOT NULL,
    conUsage VARCHAR (20) NOT NULL,
    conDateStart DATE,
    conDateEnd DATE,
    conPrix DECIMAL (15,2), 
    userId INT NOT NULL,
    photoId INT NOT NULL,
    FOREIGN KEY(userId) REFERENCES t_user(userId),
    FOREIGN KEY(photoId) REFERENCES t_photo(photoId)
);

INSERT INTO t_user (useName, useEmail, useRole, usePassword)
VALUES 
('Adrian Toledo', 'aftc1999@gmail.com', 'photographe', 'password1'),
('Fede Campoverde', 'fede@example.com', 'client', 'password2'),
('Admin', 'admin@prophotostock.ch', 'admin', 'password3');

INSERT INTO t_photo (phoTitle, phoUrl, phoDateUpload, phoVisible, userId)
VALUES 
('Portrait Urbain', 'https://images.unsplash.com/photo-1504208434309-cb69f4fe52b0', '2024-01-10', TRUE, 1),
('Montagne Suisse', 'https://images.unsplash.com/photo-1501785888041-af3ef285b470', '2024-01-12', TRUE, 1),
('Chat Noir', 'https://images.unsplash.com/photo-1518791841217-8f162f1e1131', '2024-01-15', TRUE, 1);

INSERT INTO t_contract (conType, conUsage, conDateStart, conDateEnd, conPrix, userId, photoId)
VALUES ('Diffusion', 'Graphisme', '2024-02-10', '2024-04-10', 90.00, 2, 2);
