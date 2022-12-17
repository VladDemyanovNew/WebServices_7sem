USE master;
GO

CREATE DATABASE WsDvr;
GO

USE WsDvr;
GO

CREATE TABLE Students
(
	Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Name] NVARCHAR(300) NOT NULL
);

CREATE TABLE Marks
(
	Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Subject] NVARCHAR(300) NOT NULL,
	StudentId INT FOREIGN KEY REFERENCES Students(Id) ON DELETE CASCADE NOT NULL,
	[Value] INT NOT NULL
);

GO

INSERT INTO Students([Name])
VALUES('Демьянов Владислав Русланович'),
('Демьянов Ильдар Русланович'),
('Демьянов Руслан Фёдорович');

INSERT INTO Marks([Subject], [StudentId], [Value])
VALUES ('Математика', 1, 7),
('Математика', 2, 8),
('Математика', 3, 6),
('Английский язык', 1, 6),
('Английский язык', 2, 8),
('Английский язык', 3, 9),
('Физика', 1, 8),
('Физика', 2, 8),
('Физика', 3, 7);