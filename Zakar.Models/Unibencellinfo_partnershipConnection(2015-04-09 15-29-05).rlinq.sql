-- Zakar.Models.Partner
CREATE TABLE [Partner] (
    [YookosId] varchar(255) NULL,           -- _yookosId
    [Title] varchar(255) NULL,              -- _title
    [Phone] varchar(255) NULL,              -- _phone
    [PCFId] int NULL,                       -- _pCFId
    [LastName] varchar(255) NULL,           -- _lastName
    [Id] int IDENTITY NOT NULL,             -- _id
    [FirstName] varchar(255) NULL,          -- _firstName
    [Email] varchar(255) NULL,              -- _email
    [Deleted] tinyint NOT NULL,             -- _deleted
    [DateDeleted] datetime NULL,            -- _dateDeleted
    [DateCreated] datetime NOT NULL,        -- _dateCreated
    [ChurchId] int NOT NULL,                -- _church
    [CellId] int NULL,                      -- _cellId
    CONSTRAINT [pk_Partner] PRIMARY KEY ([Id])
)
go

-- Zakar.Models.Zone
CREATE TABLE [Zone] (
    [UniqueId] varchar(255) NULL,           -- _uniqueId
    [nme] varchar(255) NULL,                -- _name
    [Id] int IDENTITY NOT NULL,             -- _id
    CONSTRAINT [pk_Zone] PRIMARY KEY ([Id])
)
go

CREATE SCHEMA [dbo]
go

-- Zakar.Models.Church
CREATE TABLE [dbo].[Churches] (
    [unique_id] varchar(255) NULL,          -- _uniqueId
    [Name] nvarchar(100) NOT NULL,          -- _name
    [GroupId] int NOT NULL,                 -- _group
    [DefaultCurrencyId] int NULL,           -- _defaultCurrencyId
    [churchId] int IDENTITY NOT NULL,       -- _churchId
    CONSTRAINT [pk_Churches] PRIMARY KEY ([churchId])
)
go

-- Zakar.Models.Currency
CREATE TABLE [dbo].[Currencies] (
    [Symbol] nvarchar(max) NOT NULL,        -- _symbol
    [Name] nvarchar(max) NOT NULL,          -- _name
    [IsDefaultCurrency] bit NULL,           -- _isDefaultCurrency
    [Id] int IDENTITY NOT NULL,             -- _id
    [ConversionRateToDefault] decimal(18,2) NOT NULL, -- _conversionRateToDefault
    CONSTRAINT [pk_Currencies] PRIMARY KEY ([Id])
)
go

-- Zakar.Models.Group
CREATE TABLE [dbo].[Groups] (
    [zone_id] int NOT NULL,                 -- _zone
    [unique_id] varchar(255) NULL,          -- _uniqueId
    [Name] nvarchar(100) NOT NULL,          -- _name
    [Id] int IDENTITY NOT NULL,             -- _id
    CONSTRAINT [pk_Groups] PRIMARY KEY ([Id])
)
go

-- Zakar.Models.NonValidatedPartnershipRecord
CREATE TABLE [dbo].[NonValidatedPartnershipRecords] (
    [Year] int NOT NULL,                    -- _year
    [PartnershipArm] int NOT NULL,          -- _partnershipArm1
    [Partner] int NOT NULL,                 -- _partner1
    [Month] int NOT NULL,                   -- _month
    [Id] int IDENTITY NOT NULL,             -- _id
    [DateCreated] datetime NOT NULL,        -- _dateCreated
    [Currency] int NOT NULL,                -- _currency
    [Amount] decimal(18,2) NOT NULL,        -- _amount
    CONSTRAINT [pk_NnVldtdPrtnrshpRcr_4C5AF85A] PRIMARY KEY ([Id])
)
go

-- Zakar.Models.NotificationCategory
CREATE TABLE [dbo].[NotificationCategories] (
    [Name] nvarchar(max) NOT NULL,          -- _name
    [CategoryId] int IDENTITY NOT NULL,     -- _categoryId
    CONSTRAINT [pk_NotificationCategories] PRIMARY KEY ([CategoryId])
)
go

-- Zakar.Models.Notification
CREATE TABLE [dbo].[Notifications] (
    [RecipientAddress] nvarchar(max) NOT NULL, -- _recipientAddress
    [NotificationCateoryCategoryId] int NOT NULL, -- _notificationCategory
    [Message] nvarchar(max) NOT NULL,       -- _message
    [IsSent] bit NOT NULL,                  -- _isSent
    [Id] int IDENTITY NOT NULL,             -- _id
    [DateSent] datetime NOT NULL,           -- _dateSent
    [ChurchId] int NULL,                    -- _churchId
    CONSTRAINT [pk_Notifications] PRIMARY KEY ([Id])
)
go

-- Zakar.Models.PartnershipArm
CREATE TABLE [dbo].[PartnershipArms] (
    [ShortFormName] nvarchar(max) NOT NULL, -- _shortFormName
    [Name] nvarchar(max) NOT NULL,          -- _name
    [Id] int IDENTITY NOT NULL,             -- _id
    [Description] nvarchar(max) NULL,       -- _description
    [Deleted] bit NOT NULL,                 -- _deleted
    [DateDeleted] datetime NULL,            -- _dateDeleted
    CONSTRAINT [pk_PartnershipArms] PRIMARY KEY ([Id])
)
go

-- Zakar.Models.Partnership
CREATE TABLE [dbo].[Partnerships] (
    [Year] int NOT NULL,                    -- _year
    [PartnershipArmId] int NOT NULL,        -- _partnershipArm
    [PartnerId] int NOT NULL,               -- _partner
    [Month] int NOT NULL,                   -- _month
    [Id] int IDENTITY NOT NULL,             -- _id
    [DateCreated] datetime NULL,            -- _dateCreated
    [CurrencyId] int NOT NULL,              -- _currency
    [Amount] decimal(18,2) NOT NULL,        -- _amount
    CONSTRAINT [pk_Partnerships] PRIMARY KEY ([Id])
)
go

-- Zakar.Models.QueuedNotification
CREATE TABLE [dbo].[QueuedNotifications] (
    [RetriesAttempt] int NOT NULL,          -- _retriesAttempt
    [RecipientAddress] nvarchar(max) NOT NULL, -- _recipientAddress
    [NotificationCateoryCategoryId] int NOT NULL, -- _notificationCategory
    [Message] nvarchar(max) NOT NULL,       -- _message
    [LastTried] datetime NULL,              -- _lastTried
    [Id] int IDENTITY NOT NULL,             -- _id
    [DateToBeSent] datetime NOT NULL,       -- _dateToBeSent
    [ChurchId] int NULL,                    -- _churchId
    CONSTRAINT [pk_QueuedNotifications] PRIMARY KEY ([Id])
)
go

-- Zakar.Models.Setting
CREATE TABLE [dbo].[Settings] (
    [Value] nvarchar(max) NOT NULL,         -- _value
    [Name] nvarchar(max) NOT NULL,          -- _name
    [Id] int IDENTITY NOT NULL,             -- _id
    CONSTRAINT [pk_Settings] PRIMARY KEY ([Id])
)
go

-- Zakar.Models.SystemSetting
CREATE TABLE [dbo].[SystemSettings] (
    [Value] nvarchar(max) NOT NULL,         -- _value
    [Name] nvarchar(max) NOT NULL,          -- _name
    [Id] int IDENTITY NOT NULL,             -- _id
    CONSTRAINT [pk_SystemSettings] PRIMARY KEY ([Id])
)
go

-- Zakar.Models.UserProfile
CREATE TABLE [dbo].[UserProfile] (
    [UserName] nvarchar(max) NULL,          -- _userName
    [UserId] int IDENTITY NOT NULL,         -- _userId
    [PhoneNumber] nvarchar(max) NULL,       -- _phoneNumber
    [LastName] nvarchar(max) NULL,          -- _lastName
    [FirstName] nvarchar(max) NULL,         -- _firstName
    [ChurchId] int NULL,                    -- _churchId
    CONSTRAINT [pk_UserProfile] PRIMARY KEY ([UserId])
)
go

-- Zakar.Models.Webpages_Membership
CREATE TABLE [dbo].[webpages_Membership] (
    [UserId] int NOT NULL,                  -- _userId
    [PasswordVerificationTokenExpirationDate] datetime NULL, -- _passwordVerificationTokenExpirationDate
    [PasswordVerificationToken] nvarchar(128) NULL, -- _passwordVerificationToken
    [PasswordSalt] nvarchar(128) NOT NULL,  -- _passwordSalt
    [PasswordFailuresSinceLastSuccess] int NOT NULL, -- _passwordFailuresSinceLastSuccess
    [PasswordChangedDate] datetime NULL,    -- _passwordChangedDate
    [Password] nvarchar(128) NOT NULL,      -- _password
    [LastPasswordFailureDate] datetime NULL, -- _lastPasswordFailureDate
    [IsConfirmed] bit NULL,                 -- _isConfirmed
    [CreateDate] datetime NULL,             -- _createDate
    [ConfirmationToken] nvarchar(128) NULL, -- _confirmationToken
    CONSTRAINT [pk_webpages_Membership] PRIMARY KEY ([UserId])
)
go

-- Zakar.Models.Webpages_OAuthMembership
CREATE TABLE [dbo].[webpages_OAuthMembership] (
    [UserId] int NOT NULL,                  -- _userId
    [ProviderUserId] nvarchar(100) NOT NULL, -- _providerUserId
    [Provider] nvarchar(30) NOT NULL,       -- _provider
    CONSTRAINT [pk_webpages_OAuthMembership] PRIMARY KEY ([Provider], [ProviderUserId])
)
go

-- Zakar.Models.Webpages_Role
CREATE TABLE [dbo].[webpages_Roles] (
    [RoleName] nvarchar(256) NOT NULL,      -- _roleName
    [RoleId] int IDENTITY NOT NULL,         -- _roleId
    CONSTRAINT [pk_webpages_Roles] PRIMARY KEY ([RoleId])
)
go

-- System.Collections.Generic.IList`1 Zakar.Models.UserProfile._webpages_Roles
CREATE TABLE [dbo].[webpages_UsersInRoles] (
    [UserId] int NOT NULL,
    [RoleId] int NOT NULL,
    CONSTRAINT [pk_webpages_UsersInRoles] PRIMARY KEY ([UserId], [RoleId])
)
go

CREATE INDEX [idx_Partner_ChurchId] ON [Partner]([ChurchId])
go

CREATE INDEX [IX_FK_GroupChurch] ON [dbo].[Churches]([GroupId])
go

CREATE INDEX [idx_Groups_zone_id] ON [dbo].[Groups]([zone_id])
go

CREATE INDEX [idx_NnVldtdPrtnrshpRcrds_Prtnr] ON [dbo].[NonValidatedPartnershipRecords]([Partner])
go

CREATE INDEX [idx_NnVldtdPrtnrshpRcrds_Prtn2] ON [dbo].[NonValidatedPartnershipRecords]([PartnershipArm])
go

CREATE INDEX [IX_FK_NotificationCateoryNotifications] ON [dbo].[Notifications]([NotificationCateoryCategoryId])
go

CREATE INDEX [IX_FK_PartnershipArmPartnership] ON [dbo].[Partnerships]([PartnershipArmId])
go

CREATE INDEX [IX_FK_PartnerPartnership] ON [dbo].[Partnerships]([PartnerId])
go

CREATE INDEX [IX_FK_CurrencyPartnership] ON [dbo].[Partnerships]([CurrencyId])
go

CREATE INDEX [IX_FK_NotificationCateoryQueuedNotifications] ON [dbo].[QueuedNotifications]([NotificationCateoryCategoryId])
go

CREATE UNIQUE INDEX [UQ__webpages__8A2B6160007323F3] ON [dbo].[webpages_Roles]([RoleName])
go

CREATE INDEX [idx_wbpges_UsersInRoles_RoleId] ON [dbo].[webpages_UsersInRoles]([RoleId])
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

ALTER TABLE [dbo].[webpages_UsersInRoles] ADD CONSTRAINT [fk_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[UserProfile]([UserId])
go

ALTER TABLE [dbo].[webpages_UsersInRoles] ADD CONSTRAINT [fk_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[webpages_Roles]([RoleId])
go

