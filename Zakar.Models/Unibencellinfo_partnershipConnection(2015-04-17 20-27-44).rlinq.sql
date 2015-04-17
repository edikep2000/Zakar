-- Zakar.Models.Cell
CREATE TABLE [Cell] (
    [Id] int IDENTITY NOT NULL,             -- _id
    [nme] varchar(255) NULL,                -- _name
    [PCFId] int NOT NULL,                   -- _pCF
    [UniqueId] varchar(255) NULL,           -- _uniqueId
    CONSTRAINT [pk_Cell] PRIMARY KEY ([Id])
)

go

-- Zakar.Models.IdentityRole
CREATE TABLE [IdentityRole] (
    [Id] varchar(255) NOT NULL,             -- _id
    [nme] varchar(255) NOT NULL,            -- _name
    CONSTRAINT [pk_IdentityRole] PRIMARY KEY ([Id])
)

go

-- Zakar.Models.IdentityUser
CREATE TABLE [IdentityUser] (
    [ChurchId] int NULL,                    -- _churchId
    [DateCreated] varchar(255) NOT NULL,    -- _dateCreated
    [FirstName] varchar(255) NOT NULL,      -- _firstName
    [Id] varchar(255) NOT NULL,             -- _id
    [LastName] varchar(255) NOT NULL,       -- _lastName
    [PasswordHash] varchar(255) NOT NULL,   -- _passwordHash
    [PhoneNumber] varchar(255) NOT NULL,    -- _phoneNumber
    [SecurityStamp] varchar(255) NOT NULL,  -- _securityStamp
    [UserName] varchar(255) NOT NULL,       -- _userName
    CONSTRAINT [pk_IdentityUser] PRIMARY KEY ([Id])
)

go

-- Zakar.Models.IdentityUserClaim
CREATE TABLE [IdentityUserClaim] (
    [ClaimType] varchar(255) NOT NULL,      -- _claimType
    [ClaimValue] varchar(255) NOT NULL,     -- _claimValue
    [Id] int IDENTITY NOT NULL,             -- _id
    [UserId] varchar(255) NOT NULL,         -- _identityUser
    CONSTRAINT [pk_IdentityUserClaim] PRIMARY KEY ([Id])
)

go

-- Zakar.Models.IdentityUserInRole
CREATE TABLE [IdentityUserInRole] (
    [Id] int IDENTITY NOT NULL,             -- _id
    [RoleId] varchar(255) NOT NULL,         -- _identityRole
    [UserId] varchar(255) NOT NULL,         -- _identityUser
    CONSTRAINT [pk_IdentityUserInRole] PRIMARY KEY ([Id])
)

go

-- Zakar.Models.IdentityUserLogin
CREATE TABLE [IdentityUserLogin] (
    [Id] varchar(255) NOT NULL,             -- _id
    [LoginProvider] varchar(255) NULL,      -- _loginProvider
    [ProviderKey] varchar(255) NULL,        -- _providerKey
    CONSTRAINT [pk_IdentityUserLogin] PRIMARY KEY ([Id])
)

go

-- Zakar.Models.PCF
CREATE TABLE [PCF] (
    [ChurchId] int NOT NULL,                -- _church
    [Id] int IDENTITY NOT NULL,             -- _id
    [nme] varchar(255) NULL,                -- _name
    [UniqueId] varchar(255) NULL,           -- _uniqueId
    CONSTRAINT [pk_PCF] PRIMARY KEY ([Id])
)

go

-- Zakar.Models.Partner
CREATE TABLE [Partner] (
    [CellId] int NULL,                      -- _cellId
    [ChurchId] int NOT NULL,                -- _church
    [DateCreated] datetime NOT NULL,        -- _dateCreated
    [DateDeleted] datetime NULL,            -- _dateDeleted
    [Deleted] tinyint NOT NULL,             -- _deleted
    [Email] varchar(255) NULL,              -- _email
    [FirstName] varchar(255) NULL,          -- _firstName
    [Id] int IDENTITY NOT NULL,             -- _id
    [LastName] varchar(255) NULL,           -- _lastName
    [PCFId] int NULL,                       -- _pCFId
    [Phone] varchar(255) NULL,              -- _phone
    [Title] varchar(255) NULL,              -- _title
    [YookosId] varchar(255) NULL,           -- _yookosId
    CONSTRAINT [pk_Partner] PRIMARY KEY ([Id])
)

go

-- Zakar.Models.Zone
CREATE TABLE [Zone] (
    [Id] int IDENTITY NOT NULL,             -- _id
    [nme] varchar(255) NULL,                -- _name
    [UniqueId] varchar(255) NULL,           -- _uniqueId
    CONSTRAINT [pk_Zone] PRIMARY KEY ([Id])
)

go

-- Zakar.Models.Church
CREATE TABLE [dbo].[Churches] (
    [DefaultCurrencyId] int NULL,           -- _defaultCurrencyId
    [GroupId] int NOT NULL,                 -- _group
    [churchId] int IDENTITY NOT NULL,       -- _id
    [Name] nvarchar(100) NOT NULL,          -- _name
    [unique_id] varchar(255) NULL,          -- _uniqueId
    CONSTRAINT [pk_Churches] PRIMARY KEY ([churchId])
)

go

-- Zakar.Models.Currency
CREATE TABLE [dbo].[Currencies] (
    [ConversionRateToDefault] decimal(18,2) NOT NULL, -- _conversionRateToDefault
    [Id] int IDENTITY NOT NULL,             -- _id
    [IsDefaultCurrency] bit NULL,           -- _isDefaultCurrency
    [Name] nvarchar(max) NOT NULL,          -- _name
    [Symbol] nvarchar(max) NOT NULL,        -- _symbol
    CONSTRAINT [pk_Currencies] PRIMARY KEY ([Id])
)

go

-- Zakar.Models.Group
CREATE TABLE [dbo].[Groups] (
    [Id] int IDENTITY NOT NULL,             -- _id
    [Name] nvarchar(100) NOT NULL,          -- _name
    [unique_id] varchar(255) NULL,          -- _uniqueId
    [zone_id] int NOT NULL,                 -- _zone
    CONSTRAINT [pk_Groups] PRIMARY KEY ([Id])
)

go

-- Zakar.Models.NonValidatedPartnershipRecord
CREATE TABLE [dbo].[NonValidatedPartnershipRecords] (
    [Amount] decimal(18,2) NOT NULL,        -- _amount
    [Currency] int NOT NULL,                -- _currency
    [DateCreated] datetime NOT NULL,        -- _dateCreated
    [Id] int IDENTITY NOT NULL,             -- _id
    [Month] int NOT NULL,                   -- _month
    [Partner] int NOT NULL,                 -- _partner1
    [PartnershipArm] int NOT NULL,          -- _partnershipArm1
    [Year] int NOT NULL,                    -- _year
    CONSTRAINT [pk_NnVldtdPrtnrshpRcr_4C5AF85A] PRIMARY KEY ([Id])
)

go

-- Zakar.Models.NotificationCategory
CREATE TABLE [dbo].[NotificationCategories] (
    [CategoryId] int IDENTITY NOT NULL,     -- _categoryId
    [Name] nvarchar(max) NOT NULL,          -- _name
    CONSTRAINT [pk_NotificationCategories] PRIMARY KEY ([CategoryId])
)

go

-- Zakar.Models.Notification
CREATE TABLE [dbo].[Notifications] (
    [ChurchId] int NULL,                    -- _churchId
    [DateSent] datetime NOT NULL,           -- _dateSent
    [Id] int IDENTITY NOT NULL,             -- _id
    [IsSent] bit NOT NULL,                  -- _isSent
    [Message] nvarchar(max) NOT NULL,       -- _message
    [NotificationCateoryCategoryId] int NOT NULL, -- _notificationCategory
    [RecipientAddress] nvarchar(max) NOT NULL, -- _recipientAddress
    CONSTRAINT [pk_Notifications] PRIMARY KEY ([Id])
)

go

-- Zakar.Models.PartnershipArm
CREATE TABLE [dbo].[PartnershipArms] (
    [Description] nvarchar(max) NULL,       -- _description
    [Id] int IDENTITY NOT NULL,             -- _id
    [Name] nvarchar(max) NOT NULL,          -- _name
    [ShortFormName] nvarchar(max) NOT NULL, -- _shortFormName
    CONSTRAINT [pk_PartnershipArms] PRIMARY KEY ([Id])
)

go

-- Zakar.Models.Partnership
CREATE TABLE [dbo].[Partnerships] (
    [Amount] decimal(18,2) NOT NULL,        -- _amount
    [CurrencyId] int NOT NULL,              -- _currency
    [DateCreated] datetime NULL,            -- _dateCreated
    [Id] int IDENTITY NOT NULL,             -- _id
    [Month] int NOT NULL,                   -- _month
    [PartnerId] int NOT NULL,               -- _partner
    [PartnershipArmId] int NOT NULL,        -- _partnershipArm
    [Year] int NOT NULL,                    -- _year
    CONSTRAINT [pk_Partnerships] PRIMARY KEY ([Id])
)

go

-- Zakar.Models.QueuedNotification
CREATE TABLE [dbo].[QueuedNotifications] (
    [ChurchId] int NULL,                    -- _churchId
    [DateToBeSent] datetime NOT NULL,       -- _dateToBeSent
    [Id] int IDENTITY NOT NULL,             -- _id
    [LastTried] datetime NULL,              -- _lastTried
    [Message] nvarchar(max) NOT NULL,       -- _message
    [NotificationCateoryCategoryId] int NOT NULL, -- _notificationCategory
    [RecipientAddress] nvarchar(max) NOT NULL, -- _recipientAddress
    [RetriesAttempt] int NOT NULL,          -- _retriesAttempt
    CONSTRAINT [pk_QueuedNotifications] PRIMARY KEY ([Id])
)

go

-- Zakar.Models.Setting
CREATE TABLE [dbo].[Settings] (
    [Id] int IDENTITY NOT NULL,             -- _id
    [Name] nvarchar(max) NOT NULL,          -- _name
    [Value] nvarchar(max) NOT NULL,         -- _value
    CONSTRAINT [pk_Settings] PRIMARY KEY ([Id])
)

go

-- Zakar.Models.SystemSetting
CREATE TABLE [dbo].[SystemSettings] (
    [Id] int IDENTITY NOT NULL,             -- _id
    [Name] nvarchar(max) NOT NULL,          -- _name
    [Value] nvarchar(max) NOT NULL,         -- _value
    CONSTRAINT [pk_SystemSettings] PRIMARY KEY ([Id])
)

go

ALTER TABLE [Cell] ADD CONSTRAINT [ref_Cell_PCF] FOREIGN KEY ([PCFId]) REFERENCES [PCF]([Id])

go

ALTER TABLE [IdentityUserClaim] ADD CONSTRAINT [ref_IdnttyUsrClm_Idnt_0E34DD03] FOREIGN KEY ([UserId]) REFERENCES [IdentityUser]([Id])

go

ALTER TABLE [IdentityUserInRole] ADD CONSTRAINT [ref_IdnttyUsrInRl_Idn_FD43865E] FOREIGN KEY ([RoleId]) REFERENCES [IdentityRole]([Id])

go

ALTER TABLE [IdentityUserInRole] ADD CONSTRAINT [ref_IdnttyUsrInRl_Idn_27B9DA7D] FOREIGN KEY ([UserId]) REFERENCES [IdentityUser]([Id])

go

ALTER TABLE [IdentityUserLogin] ADD CONSTRAINT [ref_IdnttyUsrLgn_Idnt_84216ED0] FOREIGN KEY ([Id]) REFERENCES [IdentityUser]([Id])

go

ALTER TABLE [PCF] ADD CONSTRAINT [ref_PCF_Churches] FOREIGN KEY ([ChurchId]) REFERENCES [dbo].[Churches]([churchId])

go

ALTER TABLE [Partner] ADD CONSTRAINT [ref_Partner_Churches] FOREIGN KEY ([ChurchId]) REFERENCES [dbo].[Churches]([churchId])

go

ALTER TABLE [dbo].[Churches] ADD CONSTRAINT [FK_GroupChurch] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[Groups]([Id])

go

ALTER TABLE [dbo].[Groups] ADD CONSTRAINT [ref_Groups_Zone] FOREIGN KEY ([zone_id]) REFERENCES [Zone]([Id])

go

ALTER TABLE [dbo].[NonValidatedPartnershipRecords] ADD CONSTRAINT [FK_NonValidatedPartnershipRecords_Partners] FOREIGN KEY ([Partner]) REFERENCES [Partner]([Id])

go

ALTER TABLE [dbo].[NonValidatedPartnershipRecords] ADD CONSTRAINT [FK_NonValidatedPartnershipRecords_PartnershipArms] FOREIGN KEY ([PartnershipArm]) REFERENCES [dbo].[PartnershipArms]([Id])

go

ALTER TABLE [dbo].[Notifications] ADD CONSTRAINT [FK_NotificationCateoryNotifications] FOREIGN KEY ([NotificationCateoryCategoryId]) REFERENCES [dbo].[NotificationCategories]([CategoryId])

go

ALTER TABLE [dbo].[Partnerships] ADD CONSTRAINT [FK_CurrencyPartnership] FOREIGN KEY ([CurrencyId]) REFERENCES [dbo].[Currencies]([Id])

go

ALTER TABLE [dbo].[Partnerships] ADD CONSTRAINT [FK_PartnerPartnership] FOREIGN KEY ([PartnerId]) REFERENCES [Partner]([Id])

go

ALTER TABLE [dbo].[Partnerships] ADD CONSTRAINT [FK_PartnershipArmPartnership] FOREIGN KEY ([PartnershipArmId]) REFERENCES [dbo].[PartnershipArms]([Id])

go

ALTER TABLE [dbo].[QueuedNotifications] ADD CONSTRAINT [FK_NotificationCateoryQueuedNotifications] FOREIGN KEY ([NotificationCateoryCategoryId]) REFERENCES [dbo].[NotificationCategories]([CategoryId])

go

-- Index 'idx_Cell_PCFId' was not detected in the database. It will be created
CREATE INDEX [idx_Cell_PCFId] ON [Cell]([PCFId])

go

-- Index 'idx_IdentityUserClaim_UserId' was not detected in the database. It will be created
CREATE INDEX [idx_IdentityUserClaim_UserId] ON [IdentityUserClaim]([UserId])

go

-- Index 'idx_IdentityUserInRole_UserId' was not detected in the database. It will be created
CREATE INDEX [idx_IdentityUserInRole_UserId] ON [IdentityUserInRole]([UserId])

go

-- Index 'idx_IdentityUserInRole_RoleId' was not detected in the database. It will be created
CREATE INDEX [idx_IdentityUserInRole_RoleId] ON [IdentityUserInRole]([RoleId])

go

-- Index 'idx_PCF_ChurchId' was not detected in the database. It will be created
CREATE INDEX [idx_PCF_ChurchId] ON [PCF]([ChurchId])

go

-- Index 'idx_Partner_ChurchId' was not detected in the database. It will be created
CREATE INDEX [idx_Partner_ChurchId] ON [Partner]([ChurchId])

go

-- Index 'IX_FK_GroupChurch' was not detected in the database. It will be created
CREATE INDEX [IX_FK_GroupChurch] ON [dbo].[Churches]([GroupId])

go

-- Index 'idx_Groups_zone_id' was not detected in the database. It will be created
CREATE INDEX [idx_Groups_zone_id] ON [dbo].[Groups]([zone_id])

go

-- Index 'idx_NnVldtdPrtnrshpRcrds_Prtnr' was not detected in the database. It will be created
CREATE INDEX [idx_NnVldtdPrtnrshpRcrds_Prtnr] ON [dbo].[NonValidatedPartnershipRecords]([Partner])

go

-- Index 'idx_NnVldtdPrtnrshpRcrds_Prtn2' was not detected in the database. It will be created
CREATE INDEX [idx_NnVldtdPrtnrshpRcrds_Prtn2] ON [dbo].[NonValidatedPartnershipRecords]([PartnershipArm])

go

-- Index 'IX_FK_NotificationCateoryNotifications' was not detected in the database. It will be created
CREATE INDEX [IX_FK_NotificationCateoryNotifications] ON [dbo].[Notifications]([NotificationCateoryCategoryId])

go

-- Index 'IX_FK_PartnershipArmPartnership' was not detected in the database. It will be created
CREATE INDEX [IX_FK_PartnershipArmPartnership] ON [dbo].[Partnerships]([PartnershipArmId])

go

-- Index 'IX_FK_PartnerPartnership' was not detected in the database. It will be created
CREATE INDEX [IX_FK_PartnerPartnership] ON [dbo].[Partnerships]([PartnerId])

go

-- Index 'IX_FK_CurrencyPartnership' was not detected in the database. It will be created
CREATE INDEX [IX_FK_CurrencyPartnership] ON [dbo].[Partnerships]([CurrencyId])

go

-- Index 'IX_FK_NotificationCateoryQueuedNotifications' was not detected in the database. It will be created
CREATE INDEX [IX_FK_NotificationCateoryQueuedNotifications] ON [dbo].[QueuedNotifications]([NotificationCateoryCategoryId])

go
