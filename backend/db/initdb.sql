DROP DATABASE IF EXISTS prophotostock;
CREATE DATABASE prophotostock CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE prophotostock;

-- ============================
-- USERS
-- ============================
CREATE TABLE t_users (
    userId INT AUTO_INCREMENT PRIMARY KEY,
    useName VARCHAR(50) NOT NULL,
    useEmail VARCHAR(100) NOT NULL UNIQUE,
    useRole VARCHAR(20) NOT NULL,
    usePassword VARCHAR(255) NOT NULL,
    useSalt VARCHAR(255)
) ENGINE=InnoDB;

CREATE TABLE t_photographers (
    photographerId INT AUTO_INCREMENT PRIMARY KEY,
    fkUser INT NOT NULL UNIQUE,
    FOREIGN KEY (fkUser) REFERENCES t_users(userId)
) ENGINE=InnoDB;

CREATE TABLE t_tags (
    tagId INT AUTO_INCREMENT PRIMARY KEY,
    tagName VARCHAR(50) NOT NULL
) ENGINE=InnoDB;

CREATE TABLE t_contract_types (
    typeId INT AUTO_INCREMENT PRIMARY KEY,
    typeName VARCHAR(50) NOT NULL,
    isExclusive BOOLEAN NOT NULL
) ENGINE=InnoDB;

CREATE TABLE t_usage (
    usageId INT AUTO_INCREMENT PRIMARY KEY,
    usageName VARCHAR(50) NOT NULL,
    priceExclusive DECIMAL(15,2) NOT NULL,
    priceDiffusion DECIMAL(15,2) NOT NULL
) ENGINE=InnoDB;

CREATE TABLE t_photos (
    photoId INT AUTO_INCREMENT PRIMARY KEY,
    photoTitle VARCHAR(50) NOT NULL,
    photoUrl TEXT NOT NULL,
    uploadDate DATE NOT NULL,
    isVisible BOOLEAN DEFAULT TRUE,
    status VARCHAR(50) NOT NULL,
    fkPhotographer INT NOT NULL,
    FOREIGN KEY (fkPhotographer) REFERENCES t_photographers(photographerId)
) ENGINE=InnoDB;

CREATE TABLE t_contracts (
    contractId INT AUTO_INCREMENT PRIMARY KEY,
    startDate DATE NOT NULL,
    endDate DATE NOT NULL,
    price DECIMAL(15,2) NOT NULL,
    status VARCHAR(50) NOT NULL,
    photographerCommission DECIMAL(15,2) NOT NULL,
    fkUsage INT NOT NULL,
    fkType INT NOT NULL,
    fkPhoto INT NOT NULL,
    fkUser INT NOT NULL,
    FOREIGN KEY (fkUsage) REFERENCES t_usage(usageId),
    FOREIGN KEY (fkType) REFERENCES t_contract_types(typeId),
    FOREIGN KEY (fkPhoto) REFERENCES t_photos(photoId),
    FOREIGN KEY (fkUser) REFERENCES t_users(userId)
) ENGINE=InnoDB;

CREATE TABLE t_photo_tags (
    fkPhoto INT NOT NULL,
    fkTag INT NOT NULL,
    PRIMARY KEY (fkPhoto, fkTag),
    FOREIGN KEY (fkPhoto) REFERENCES t_photos(photoId),
    FOREIGN KEY (fkTag) REFERENCES t_tags(tagId)
) ENGINE=InnoDB;

-- ============================
-- INSERTS
-- ============================

INSERT INTO t_users (useName, useEmail, useRole, usePassword)
VALUES 
('Adrian Toledo', 'adri@gmail.com', 'photographer', 'password1'),
('Fede Campoverde', 'fede@gmail.com', 'client', 'password2'),
('Admin', 'admin@prophotostock.ch', 'admin', 'password3');

INSERT INTO t_photographers (fkUser)
VALUES (1);

INSERT INTO t_tags (tagName)
VALUES 
('Sport'),
('Landscape'),
('Portrait');

INSERT INTO t_contract_types (typeName, isExclusive)
VALUES
('Exclusive', TRUE),
('Diffusion', FALSE);

INSERT INTO t_usage (usageName, priceExclusive, priceDiffusion)
VALUES
('Advertising', 740.00, 370.00),
('Graphic Design', 1280.00, 640.00),
('Media', 1400.00, 700.00);

INSERT INTO t_photos (photoTitle, photoUrl, uploadDate, isVisible, status, fkPhotographer)
VALUES
('Urban Portrait', 'https://images.unsplash.com/photo-1504208434309-cb69f4fe52b0', '2024-01-10', TRUE, 'available', 1),
('Swiss Mountain', 'https://images.unsplash.com/photo-1501785888041-af3ef285b470', '2024-01-12', TRUE, 'available', 1),
('Black Cat', 'https://images.unsplash.com/photo-1518791841217-8f162f1e1131', '2024-01-15', TRUE, 'available', 1);

INSERT INTO t_photo_tags (fkPhoto, fkTag)
VALUES
(1, 3),
(2, 2),
(3, 1);

INSERT INTO t_contracts (startDate, endDate, price, status, photographerCommission, fkUsage, fkType, fkPhoto, fkUser)
VALUES
('2024-02-10', '2024-04-10', 90.00, 'active', 18.00, 2, 2, 2, 2);

