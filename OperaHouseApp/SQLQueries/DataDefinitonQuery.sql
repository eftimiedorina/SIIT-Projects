--CREATE TABLE Zones (
--    ZoneId VARCHAR(50) PRIMARY KEY,
--    Name VARCHAR(100) NOT NULL,
--    Price DECIMAL(10,2) NOT NULL
--);

--CREATE TABLE Users (
--    UserId INT IDENTITY(1,1) PRIMARY KEY,
--    Username VARCHAR(100) NOT NULL UNIQUE,
--    PasswordHash VARCHAR(255) NOT NULL,
--    UserType INT NOT NULL
--);

--CREATE TABLE Seats (
--    SeatId INT IDENTITY(1,1) PRIMARY KEY,
--    ZoneId VARCHAR(50) NOT NULL,
--    Number INT NOT NULL,
--    IsOccupied BIT NOT NULL DEFAULT 0,
--    FOREIGN KEY (ZoneId) REFERENCES Zones(ZoneId)
--);

--CREATE TABLE Tickets (
--    TicketId INT IDENTITY(1,1) PRIMARY KEY,
--    UserId INT NOT NULL,
--    ZoneId VARCHAR(50) NOT NULL,
--    TotalPrice DECIMAL(10,2) NOT NULL,
--    FOREIGN KEY (UserId) REFERENCES Users(UserId),
--    FOREIGN KEY (ZoneId) REFERENCES Zones(ZoneId)
--);

CREATE TABLE TicketSeats (
    TicketId INT NOT NULL,
    SeatId INT NOT NULL,
    FOREIGN KEY (TicketId) REFERENCES Tickets(TicketId),
    FOREIGN KEY (SeatId) REFERENCES Seats(SeatId),
    PRIMARY KEY (TicketId, SeatId)
);
