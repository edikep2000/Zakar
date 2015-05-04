-- Zakar.Models.StagedCells
CREATE TABLE [StagedCells] (
    [Id] int IDENTITY NOT NULL,             -- _id
    [nme] varchar(255) NOT NULL,            -- _name
    [PCFId] int NOT NULL,                   -- _pCFId
    [UniqueId] varchar(255) NOT NULL,       -- _uniqueId
    CONSTRAINT [pk_StagedCells] PRIMARY KEY ([Id])
)

go

-- Zakar.Models.StagedChurch
CREATE TABLE [StagedChurch] (
    [GroupId] int NOT NULL,                 -- _groupId
    [Id] int IDENTITY NOT NULL,             -- _id
    [nme] varchar(255) NOT NULL,            -- _name
    [UniqueId] varchar(255) NOT NULL,       -- _uniqueId
    CONSTRAINT [pk_StagedChurch] PRIMARY KEY ([Id])
)

go

-- Zakar.Models.StagedGroup
CREATE TABLE [StagedGroup] (
    [Id] int IDENTITY NOT NULL,             -- _id
    [nme] varchar(255) NOT NULL,            -- _name
    [UniqueId] varchar(255) NOT NULL,       -- _uniqueId
    [ZoneId] int NOT NULL,                  -- _zoneId
    CONSTRAINT [pk_StagedGroup] PRIMARY KEY ([Id])
)

go

-- Zakar.Models.StagedPCFs
CREATE TABLE [StagedPCFs] (
    [ChurchId] int NOT NULL,                -- _churchId
    [Id] int IDENTITY NOT NULL,             -- _id
    [nme] varchar(255) NOT NULL,            -- _name
    [UniqueId] varchar(255) NOT NULL,       -- _uniqueId
    CONSTRAINT [pk_StagedPCFs] PRIMARY KEY ([Id])
)

go

-- Zakar.Models.StagedZone
CREATE TABLE [StagedZone] (
    [Id] int IDENTITY NOT NULL,             -- _id
    [nme] varchar(255) NOT NULL,            -- _name
    [UniqueId] varchar(255) NOT NULL,       -- _uniqueId
    CONSTRAINT [pk_StagedZone] PRIMARY KEY ([Id])
)

go

