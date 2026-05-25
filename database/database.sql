CREATE DATABASE parking_db;

USE parking_db;

CREATE TABLE VehicleRecords (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Plate VARCHAR(10) NOT NULL,
    VehicleType INT NOT NULL,
    EntryTime DATETIME NOT NULL,
    ExitTime DATETIME NULL,
    TotalMinutes INT NULL,
    TotalAmount DECIMAL(10,2) NULL,
    Status INT NOT NULL
);

CREATE INDEX IX_VehicleRecords_Plate
ON VehicleRecords(Plate);

CREATE INDEX IX_VehicleRecords_Status
ON VehicleRecords(Status);