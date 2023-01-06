
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/04/2023 02:24:08
-- Generated from EDMX file: D:\GIT\EmployeeManager\EmployeeManager.DataLayer\RepositoryDataModel\EmployeeDataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [EmployeeDb];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK__Employeer__Depar__398D8EEE]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Employeers] DROP CONSTRAINT [FK__Employeer__Depar__398D8EEE];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Departments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Departments];
GO
IF OBJECT_ID(N'[dbo].[Employeers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Employeers];
GO
IF OBJECT_ID(N'[dbo].[HistoryLevelSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[HistoryLevelSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Departments'
CREATE TABLE [dbo].[Departments] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(250)  NULL
);
GO

-- Creating table 'Employeers'
CREATE TABLE [dbo].[Employeers] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Department] int  NULL,
    [FirstName] nvarchar(100)  NULL,
    [MiddleName] nvarchar(100)  NULL,
    [LastName] nvarchar(100)  NULL,
    [DateBirth] datetime  NULL,
    [Position] nvarchar(100)  NULL,
    [Phone] nvarchar(20)  NULL,
    [Email] nvarchar(50)  NULL
);
GO

-- Creating table 'HistoryLevelSet'
CREATE TABLE [dbo].[HistoryLevelSet] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [TableName] nvarchar(max)  NOT NULL,
    [RecordId] int  NOT NULL,
    [InsertMode] bit  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'Departments'
ALTER TABLE [dbo].[Departments]
ADD CONSTRAINT [PK_Departments]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Employeers'
ALTER TABLE [dbo].[Employeers]
ADD CONSTRAINT [PK_Employeers]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'HistoryLevelSet'
ALTER TABLE [dbo].[HistoryLevelSet]
ADD CONSTRAINT [PK_HistoryLevelSet]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Department] in table 'Employeers'
ALTER TABLE [dbo].[Employeers]
ADD CONSTRAINT [FK__Employeer__Depar__398D8EEE]
    FOREIGN KEY ([Department])
    REFERENCES [dbo].[Departments]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Employeer__Depar__398D8EEE'
CREATE INDEX [IX_FK__Employeer__Depar__398D8EEE]
ON [dbo].[Employeers]
    ([Department]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------