-- Column was read from database as: [FirstName] varchar(255) null
-- modify column for field _firstName
UPDATE [Partner]
   SET [FirstName] = ' ' -- Add your own default value here, for when [FirstName] is null.
 WHERE [FirstName] IS NULL

go

ALTER TABLE [Partner] ALTER COLUMN [FirstName] varchar(255) NOT NULL

go

-- Column was read from database as: [Phone] varchar(255) null
-- modify column for field _phone
UPDATE [Partner]
   SET [Phone] = ' ' -- Add your own default value here, for when [Phone] is null.
 WHERE [Phone] IS NULL

go

ALTER TABLE [Partner] ALTER COLUMN [Phone] varchar(255) NOT NULL

go

-- Column was read from database as: [Title] varchar(255) null
-- modify column for field _title
UPDATE [Partner]
   SET [Title] = ' ' -- Add your own default value here, for when [Title] is null.
 WHERE [Title] IS NULL

go

ALTER TABLE [Partner] ALTER COLUMN [Title] varchar(255) NOT NULL

go

-- add column for field _uniqueId
ALTER TABLE [Partner] ADD [UniqueId] varchar(255) NULL

go

-- dropping unknown column [DateDeleted]
ALTER TABLE [Partner] DROP COLUMN [DateDeleted]

go

-- dropping unknown column [Deleted]
ALTER TABLE [Partner] DROP COLUMN [Deleted]

go

-- dropping unknown column [YookosId]
ALTER TABLE [Partner] DROP COLUMN [YookosId]

go

-- Zakar.Models.StagedPartnerService
CREATE TABLE [StagedPartnerService] (
    [CellId] int NOT NULL,                  -- _cellId
    [ChurchId] int NOT NULL,                -- _churchId
    [DateCreated] datetime NULL,            -- _dateCreated
    [Email] varchar(255) NOT NULL,          -- _email
    [FirstName] varchar(255) NOT NULL,      -- _firstName
    [Id] int IDENTITY NOT NULL,             -- _id
    [LastName] varchar(255) NOT NULL,       -- _lastName
    [PCFId] int NULL,                       -- _pCFId
    [Phone] varchar(255) NOT NULL,          -- _phone
    [Title] varchar(255) NOT NULL,          -- _title
    [UniqueId] varchar(255) NOT NULL,       -- _uniqueId
    CONSTRAINT [pk_StagedPartnerService] PRIMARY KEY ([Id])
)

go

