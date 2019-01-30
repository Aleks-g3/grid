
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/30/2019 17:35:06
-- Generated from EDMX file: D:\repos\WebApplication2\DBAccess\School.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [School];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_KlasaWychowawca]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Klasa] DROP CONSTRAINT [FK_KlasaWychowawca];
GO
IF OBJECT_ID(N'[dbo].[FK_UczniowieKlasa]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Uczniowie] DROP CONSTRAINT [FK_UczniowieKlasa];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Klasa]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Klasa];
GO
IF OBJECT_ID(N'[dbo].[Uczniowie]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Uczniowie];
GO
IF OBJECT_ID(N'[dbo].[Wychowawca]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Wychowawca];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Klasas'
CREATE TABLE [dbo].[Klasas] (
    [id] int IDENTITY(1,1) NOT NULL,
    [Nazwa] nvarchar(2)  NOT NULL,
    [Id_Wychowawca] int  NOT NULL
);
GO

-- Creating table 'Uczniowies'
CREATE TABLE [dbo].[Uczniowies] (
    [id] int IDENTITY(1,1) NOT NULL,
    [Imie] nvarchar(10)  NOT NULL,
    [Nazwisko] nvarchar(10)  NOT NULL,
    [Id_Klasy] int  NOT NULL
);
GO

-- Creating table 'Wychowawcas'
CREATE TABLE [dbo].[Wychowawcas] (
    [id] int IDENTITY(1,1) NOT NULL,
    [Imie] nvarchar(10)  NOT NULL,
    [Nazwisko] nvarchar(10)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [id] in table 'Klasas'
ALTER TABLE [dbo].[Klasas]
ADD CONSTRAINT [PK_Klasas]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Uczniowies'
ALTER TABLE [dbo].[Uczniowies]
ADD CONSTRAINT [PK_Uczniowies]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Wychowawcas'
ALTER TABLE [dbo].[Wychowawcas]
ADD CONSTRAINT [PK_Wychowawcas]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Id_Wychowawca] in table 'Klasas'
ALTER TABLE [dbo].[Klasas]
ADD CONSTRAINT [FK_KlasaWychowawca]
    FOREIGN KEY ([Id_Wychowawca])
    REFERENCES [dbo].[Wychowawcas]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_KlasaWychowawca'
CREATE INDEX [IX_FK_KlasaWychowawca]
ON [dbo].[Klasas]
    ([Id_Wychowawca]);
GO

-- Creating foreign key on [Id_Klasy] in table 'Uczniowies'
ALTER TABLE [dbo].[Uczniowies]
ADD CONSTRAINT [FK_UczniowieKlasa]
    FOREIGN KEY ([Id_Klasy])
    REFERENCES [dbo].[Klasas]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UczniowieKlasa'
CREATE INDEX [IX_FK_UczniowieKlasa]
ON [dbo].[Uczniowies]
    ([Id_Klasy]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------