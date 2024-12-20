CREATE TABLE EventManagers (
    ManagerId INT PRIMARY KEY IDENTITY(1,1), -- Auto-incremented primary key for unique Manager IDs
    FirstName VARCHAR(50) NOT NULL,          -- First name of the manager (maximum 50 characters)
    LastName VARCHAR(50) NOT NULL,           -- Last name of the manager (maximum 50 characters)
    Username VARCHAR(50) UNIQUE NOT NULL,    -- Username (must be unique)
    Password VARCHAR(255) NOT NULL,         -- Password (hashed, typically larger length)
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
);


CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY(1,1), -- Auto-incremented primary key for unique User IDs
    FirstName VARCHAR(50) NOT NULL,          -- First name of the manager (maximum 50 characters)
    LastName VARCHAR(50) NOT NULL,           -- Last name of the manager (maximum 50 characters)
    Username VARCHAR(50) UNIQUE NOT NULL,    -- Username (must be unique)
    Password VARCHAR(255) NOT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
);

CREATE TABLE Events (
    EventId INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(255) NOT NULL,
    Date NVARCHAR(50) NOT NULL,
    Time NVARCHAR(50) NOT NULL,
    Location NVARCHAR(255) NOT NULL
);

INSERT INTO events (Title, Date, Time, Location)
VALUES ('Clemson vs South Carolina', 'November 25, 2024', '8:00 PM', 'Death Valley');


CREATE TABLE Tickets (
    TicketId INT IDENTITY(1,1) PRIMARY KEY, -- Auto-incrementing unique identifier
    EventId INT NOT NULL,                  -- Foreign key or reference to an Event
    TicketName NVARCHAR(255) NOT NULL,     -- Name of the ticket (supports Unicode characters)
    Price DECIMAL(10, 2) NOT NULL          -- Price with precision (e.g., 99999999.99)
);

INSERT INTO Tickets (EventId, TicketName, Price)
VALUES (1, 'Hill Ticket', 40.00);
