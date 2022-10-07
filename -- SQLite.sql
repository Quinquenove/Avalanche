-- SQLite
CREATE TABLE Schwierigkeit(
    Id INTEGER  PRIMARY KEY AUTOINCREMENT,
    Name Text NOT NULL
);

CREATE TABLE Gebirge(
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name Text NOT NULL
);

CREATE Table Berg(
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL,
    Gebirge_Id INTEGER NOT NULL,
    Schwierigkeit_Id INTEGER,
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
    Haus_Berg_Id INTEGER,
    FOREIGN KEY (Haus_Berg_Id)
        REFERENCES Berg (Id)
        ON DELETE SET NULL
        ON UPDATE NO ACTION
);

CREATE TABLE Trick (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name Text NOT NULL,
    Beschreibung TEXT NOT NULL
);

CREATE TABLE Profi(
    Lizenznummer Text PRIMARY KEY,
    Weltcuppunkte INTEGER,
    Mitgliedsnummer Text NOT NULL UNIQUE,
    Best_Trick_Id INTEGER,
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
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name Text NOT NULL
);

CREATE TABLE Sponsor (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name Text NOT NULL
);

CREATE TABLE Sponsoring (
    Snowboarder Text,
    Sponsor INTEGER,
    Vertragsart INTEGER,
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
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name Text NOT NULL,
    Jahr INTEGER NOT NULL,
    Sponsor_Id INTEGER,
    Berg_Id INTEGER,
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
    Wettkampf_Id INTEGER,
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