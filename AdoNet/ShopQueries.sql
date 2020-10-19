CREATE DATABASE Shop;
USE Shop;

CREATE TABLE dbo.Categories
(
[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
[Name] NVARCHAR(50) NOT NULL
);

CREATE TABLE dbo.Products
(
[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
[Name] NVARCHAR(50) NOT NULL,
[Price] INT NOT NULL,
[CategoryId] INT NOT NULL,
FOREIGN KEY (CategoryId) REFERENCES Categories(id)
);

 INSERT INTO dbo.Categories (Name)
VALUES (N'фрукты'), (N'овощи'), (N'€годы');

INSERT INTO products (name, price, categoryId)
VALUES (N'огурцы', 60, 2), (N'бананы', 90, 1), (N'абрикосы', 150, 1), (N'картофель', 50, 2), (N'черешн€', 300, 3);