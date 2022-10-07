-- SQLite
CREATE TABLE Schwierigkeit(
    Id INT PRIMARY KEY,
    Name Text NOT NULL
);

CREATE TABLE Gebirge(
    Id INT PRIMARY KEY,
    Name Text NOT NULL
);

CREATE Table Berg(
    Id INT PRIMARY KEY,
    Name TEXT NOT NULL,
    Gebirge_Id INT NOT NULL,
    Schwierigkeit_Id INT,
    FOREIGN KEY (Gebirge_Id)
        REFERENCES Gebirge (Id)
        ON DELETE CASCADE
        ON UPDATE NO ACTION,
    FOREIGN KEY (Schwierigkeit_Id)
        REFERENCES Schwierigkeit (Id)
        ON DELETE SET NULL 
        ON UPDATE NO ACTION
);

CREATE TABLE Snowboarder(
    Mitgliedsnummer TEXT PRIMARY KEY,
    Nachname Text NOT NULL,
    Vorname Text NOT NULL,
    Kuenstlername Text NOT NULL,
    Geburtstag Date NOT NULL,
    Haus_Berg_Id INT,
    FOREIGN KEY (Haus_Berg_Id)
        REFERENCES Berg (Id)
        ON DELETE SET NULL
        ON UPDATE NO ACTION
);

CREATE TABLE Trick (
    Id INT PRIMARY KEY,
    Name Text NOT NULL,
    Beschreibung NOT NULL
);

CREATE TABLE Profi(
    Lizenznummer Text PRIMARY KEY,
    Weltcuppunkte INT,
    Mitgliedsnummer Text NOT NULL UNIQUE,
    Best_Trick_Id INT,
    FOREIGN KEY (Mitgliedsnummer)
        REFERENCES Snowboarder (Mitgliedsnummer)
        ON DELETE CASCADE
        ON UPDATE NO ACTION,
    FOREIGN KEY (Best_Trick_Id)
        REFERENCES Trick (Id)
        ON DELETE SET NULL
        ON UPDATE NO ACTION
);

CREATE TABLE Vertragsart (
    Id INT PRIMARY KEY,
    Name Text NOT NULL
);

CREATE TABLE Sponsor (
    Id INT PRIMARY KEY,
    Name Text NOT NULL
);

CREATE TABLE Sponsoring (
    Snowboarder Text,
    Sponsor INT,
    Vertragsart INT,
    PRIMARY KEY (Snowboarder, Sponsor),
    FOREIGN KEY (Snowboarder)
        REFERENCES Snowboarder (Mitgliedsnummer)
        ON DELETE CASCADE
        ON UPDATE NO ACTION,
    FOREIGN KEY (Sponsor)
        REFERENCES Sponsor (Id)
        ON DELETE CASCADE
        ON UPDATE NO ACTION,
    FOREIGN KEY (Vertragsart)
        REFERENCES Vertragsart (Id)
        ON DELETE SET NULL
        ON UPDATE NO ACTION
);

CREATE TABLE Wettkampf (
    Id INT PRIMARY KEY,
    Name Text NOT NULL,
    Jahr INT NOT NULL,
    Sponsor_Id INT,
    Berg_Id INT,
    Preisgeld REAL,
    FOREIGN KEY (Sponsor_Id)
        REFERENCES Sponsor (Id)
        ON DELETE SET NULL
        ON UPDATE NO ACTION,
    FOREIGN KEY (Berg_Id)
        REFERENCES Berg (Id)
        ON DELETE SET NULL
        ON UPDATE NO ACTION
);

CREATE TABLE Wettkaempfer (
    Snowboarder TEXT,
    Wettkampf_Id INT,
    PRIMARY KEY (Snowboarder, Wettkampf_Id),
    FOREIGN KEY (Snowboarder)
        REFERENCES Snowboarder (Mitgliedsnummer)
        ON DELETE CASCADE
        ON UPDATE NO ACTION,
    FOREIGN KEY (Wettkampf_Id)
        REFERENCES Wettkampf (Id)
        ON DELETE CASCADE
        ON UPDATE NO ACTION
);