-- dropping table [StagedPartnerService]
DROP TABLE [StagedPartnerService]

go

-- Zakar.Models.StagedPartner
CREATE TABLE [StagedPartner] (
    [UniqueId] varchar(255) NULL,           -- _uniqueId
    [Title] varchar(255) NULL,              -- _title
    [Phone] varchar(255) NULL,              -- _phone
    [PCFId] int NULL,                       -- _pCFId
    [LastName] varchar(255) NULL,           -- _lastName
    [Id] int IDENTITY NOT NULL,             -- _id
    [FirstName] varchar(255) NULL,          -- _firstName
    [Email] varchar(255) NULL,              -- _email
    [DateOfBirth] datetime NULL,            -- _dateOfBirth
    [DateCreated] datetime NULL,            -- _dateCreated
    [ChurchId] int NULL,                    -- _churchId
    [CellId] int NULL,                      -- _cellId
    CONSTRAINT [pk_StagedPartner] PRIMARY KEY ([Id])
)

go

