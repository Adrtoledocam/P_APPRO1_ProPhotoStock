DROP DATABASE IF EXISTS prophotostock;
CREATE DATABASE prophotostock CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE prophotostock;


CREATE TABLE Utilisateur (
    Id_Utilisateur INT AUTO_INCREMENT PRIMARY KEY,
    nom VARCHAR(50) NOT NULL,
    email VARCHAR(100) NOT NULL UNIQUE,
    role VARCHAR(20) NOT NULL,
    motDePasse VARCHAR(255) NOT NULL
) ENGINE=InnoDB;

CREATE TABLE Photographers (
    Id_Photographers INT AUTO_INCREMENT PRIMARY KEY,
    Id_Utilisateur INT NOT NULL UNIQUE,
    FOREIGN KEY (Id_Utilisateur) REFERENCES Utilisateur(Id_Utilisateur)
) ENGINE=InnoDB;

CREATE TABLE Tag (
    Id_Tag INT AUTO_INCREMENT PRIMARY KEY,
    typeTag VARCHAR(50) NOT NULL
) ENGINE=InnoDB;

CREATE TABLE TypeContrat (
    Id_TypeContrat INT AUTO_INCREMENT PRIMARY KEY,
    nomType VARCHAR(50) NOT NULL,
    exclusif BOOLEAN NOT NULL
) ENGINE=InnoDB;

CREATE TABLE UsageContrat (
    Id_UsageContrat INT AUTO_INCREMENT PRIMARY KEY,
    usageNom VARCHAR(50) NOT NULL,
    prixExclusif DECIMAL(15,2) NOT NULL,
    prixDiffusion DECIMAL(15,2) NOT NULL
) ENGINE=InnoDB;

CREATE TABLE Photo (
    Id_Photo INT AUTO_INCREMENT PRIMARY KEY,
    titre VARCHAR(50) NOT NULL,
    url TEXT NOT NULL,
    dateUpload DATE NOT NULL,
    estVisible BOOLEAN DEFAULT TRUE,
    status VARCHAR(50) NOT NULL,
    Id_Photographers INT NOT NULL,
    FOREIGN KEY (Id_Photographers) REFERENCES Photographers(Id_Photographers)
) ENGINE=InnoDB;


CREATE TABLE Contrat (
    Id_Contrat INT AUTO_INCREMENT PRIMARY KEY,
    dateDebut DATE NOT NULL,
    dateFin DATE NOT NULL,
    prix DECIMAL(15,2) NOT NULL,
    status VARCHAR(50) NOT NULL,
    commissionPhotographe DECIMAL(15,2) NOT NULL,
    Id_UsageContrat INT NOT NULL,
    Id_TypeContrat INT NOT NULL,
    Id_Photo INT NOT NULL,
    Id_Utilisateur INT NOT NULL,
    FOREIGN KEY (Id_UsageContrat) REFERENCES UsageContrat(Id_UsageContrat),
    FOREIGN KEY (Id_TypeContrat) REFERENCES TypeContrat(Id_TypeContrat),
    FOREIGN KEY (Id_Photo) REFERENCES Photo(Id_Photo),
    FOREIGN KEY (Id_Utilisateur) REFERENCES Utilisateur(Id_Utilisateur)
) ENGINE=InnoDB;

CREATE TABLE Etiqueter (
    Id_Photo INT NOT NULL,
    Id_Tag INT NOT NULL,
    PRIMARY KEY (Id_Photo, Id_Tag),
    FOREIGN KEY (Id_Photo) REFERENCES Photo(Id_Photo),
    FOREIGN KEY (Id_Tag) REFERENCES Tag(Id_Tag)
) ENGINE=InnoDB;

/* ============================================================
   INSERTS
   ============================================================ */


INSERT INTO Utilisateur (nom, email, role, motDePasse)
VALUES 
('Adrian Toledo', 'aftc1999@gmail.com', 'photographe', 'password1'),
('Fede Campoverde', 'fede@example.com', 'client', 'password2'),
('Admin', 'admin@prophotostock.ch', 'responsable', 'password3');


INSERT INTO Photographers (Id_Utilisateur)
VALUES (1);


INSERT INTO Tag (typeTag)
VALUES 
('Sport'),
('Paysage'),
('Portrait');


INSERT INTO TypeContrat (nomType, exclusif)
VALUES
('Exclusif', TRUE),
('Diffusion', FALSE);


INSERT INTO UsageContrat (usageNom, prixExclusif, prixDiffusion)
VALUES
('Publicité', 740.00, 370.00),
('Graphisme', 1280.00, 640.00),
('Média', 1400.00, 700.00);


INSERT INTO Photo (titre, url, dateUpload, estVisible, status, Id_Photographers)
VALUES
('Portrait Urbain', 'https://images.unsplash.com/photo-1504208434309-cb69f4fe52b0', '2024-01-10', TRUE, 'disponible', 1),
('Montagne Suisse', 'https://images.unsplash.com/photo-1501785888041-af3ef285b470', '2024-01-12', TRUE, 'disponible', 1),
('Chat Noir', 'https://images.unsplash.com/photo-1518791841217-8f162f1e1131', '2024-01-15', TRUE, 'disponible', 1);


INSERT INTO Etiqueter (Id_Photo, Id_Tag)
VALUES
(1, 3),
(2, 2),
(3, 1);


INSERT INTO Contrat (dateDebut, dateFin, prix, status, commissionPhotographe, Id_UsageContrat, Id_TypeContrat, Id_Photo, Id_Utilisateur)
VALUES
('2024-02-10', '2024-04-10', 90.00, 'actif', 18.00, 2, 2, 2, 2);
