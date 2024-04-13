INSERT INTO Users (Username, PasswordHash, UserType) VALUES
('john_doe', 'hashed_password_1', 2),  -- Presupunem c? 2 este pentru AuthenticatedUser
('admin_user', 'hashed_password_2', 3), -- Presupunem c? 3 este pentru Administrator
('visitor_user', '', 1);               -- Presupunem c? 1 este pentru Visitor


INSERT INTO Zones (ZoneId, Name, Price) VALUES
('A1', 'Lodge Zone', 120.00),
('B1', 'Gallery Zone', 80.00),
('C1', 'Hall Zone', 40.00);


INSERT INTO Seats (ZoneId, Number, IsOccupied) VALUES
('A1', 1, 0),
('A1', 2, 0),
('A1', 3, 1),  -- Un loc ocupat pentru a simula un bilet vândut
('B1', 1, 0),
('B1', 2, 0),
('C1', 1, 0),
('C1', 2, 1);  -- Un loc ocupat pentru a simula un bilet vândut

INSERT INTO Tickets (UserId, ZoneId, TotalPrice) VALUES
(1, 'A1', 120.00),  -- Bilet vândut utilizatorului AuthenticatedUser pentru Lodge Zone
(1, 'C1', 40.00);   -- Un alt bilet pentru AuthenticatedUser

INSERT INTO TicketSeats (TicketId, SeatId) VALUES
(1, 3),  -- Rela?ioneaz? primul bilet cu locul ocupat
(2, 7);  -- Rela?ioneaz? al doilea bilet cu un alt loc ocupat
