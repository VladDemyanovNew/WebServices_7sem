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
VALUES('�������� ��������� ����������'),
('�������� ������ ����������'),
('�������� ������ Ը�������');

INSERT INTO Marks([Subject], [StudentId], [Value])
VALUES ('����������', 1, 7),
('����������', 2, 8),
('����������', 3, 6),
('���������� ����', 1, 6),
('���������� ����', 2, 8),
('���������� ����', 3, 9),
('������', 1, 8),
('������', 2, 8),
('������', 3, 7);