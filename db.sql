-- Create the database
CREATE DATABASE testdb;
GO

-- Use the newly created database
USE testdb;
GO

-- Create the Movies table
CREATE TABLE dbo.Movies
(
    Id                     UNIQUEIDENTIFIER DEFAULT NEWID() NOT NULL PRIMARY KEY,
    Title                  NVARCHAR(255) NOT NULL,
    OriginalTitle          NVARCHAR(255),
    OriginalTitleRomanised NVARCHAR(255),
    Description            NVARCHAR(MAX),
    Director               NVARCHAR(255),
    Producer               NVARCHAR(255),
    ReleaseDate            CHAR(4),
    CreatedAt              DATETIME2 DEFAULT GETUTCDATE() NOT NULL,
    UpdatedAt              DATETIME2
);
GO