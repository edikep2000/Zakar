ALTER TABLE [PCF] DROP CONSTRAINT [ref_PCF_Churches]

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

-- Zakar.Models.UserProfile
CREATE TABLE [dbo].[UserProfile] (
    [ChurchId] int NULL,                    -- _churchId
    [FirstName] nvarchar(max) NULL,         -- _firstName
    [LastName] nvarchar(max) NULL,          -- _lastName
    [PhoneNumber] nvarchar(max) NULL,       -- _phoneNumber
    [UserId] int IDENTITY NOT NULL,         -- _userId
    [UserName] nvarchar(max) NULL,          -- _userName
    CONSTRAINT [pk_UserProfile] PRIMARY KEY ([UserId])
)

go

-- Zakar.Models.Webpages_Membership
CREATE TABLE [dbo].[webpages_Membership] (
    [ConfirmationToken] nvarchar(128) NULL, -- _confirmationToken
    [CreateDate] datetime NULL,             -- _createDate
    [IsConfirmed] bit NULL,                 -- _isConfirmed
    [LastPasswordFailureDate] datetime NULL, -- _lastPasswordFailureDate
    [Password] nvarchar(128) NOT NULL,      -- _password
    [PasswordChangedDate] datetime NULL,    -- _passwordChangedDate
    [PasswordFailuresSinceLastSuccess] int NOT NULL, -- _passwordFailuresSinceLastSuccess
    [PasswordSalt] nvarchar(128) NOT NULL,  -- _passwordSalt
    [PasswordVerificationToken] nvarchar(128) NULL, -- _passwordVerificationToken
    [PasswordVerificationTokenExpirationDate] datetime NULL, -- _passwordVerificationTokenExpirationDate
    [UserId] int NOT NULL,                  -- _userId
    CONSTRAINT [pk_webpages_Membership] PRIMARY KEY ([UserId])
)

go

-- Zakar.Models.Webpages_OAuthMembership
CREATE TABLE [dbo].[webpages_OAuthMembership] (
    [Provider] nvarchar(30) NOT NULL,       -- _provider
    [ProviderUserId] nvarchar(100) NOT NULL, -- _providerUserId
    [UserId] int NOT NULL,                  -- _userId
    CONSTRAINT [pk_webpages_OAuthMembership] PRIMARY KEY ([Provider], [ProviderUserId])
)

go

-- Zakar.Models.Webpages_Role
CREATE TABLE [dbo].[webpages_Roles] (
    [RoleId] int IDENTITY NOT NULL,         -- _roleId
    [RoleName] nvarchar(256) NOT NULL,      -- _roleName
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

ALTER TABLE [PCF] ADD CONSTRAINT [ref_PCF_Churches] FOREIGN KEY ([ChurchId]) REFERENCES [dbo].[Churches]([churchId])

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

-- Index 'UQ__webpages__8A2B6160007323F3' was not detected in the database. It will be created
CREATE UNIQUE INDEX [UQ__webpages__8A2B6160007323F3] ON [dbo].[webpages_Roles]([RoleName])

go

-- Index 'idx_wbpges_UsersInRoles_RoleId' was not detected in the database. It will be created
CREATE INDEX [idx_wbpges_UsersInRoles_RoleId] ON [dbo].[webpages_UsersInRoles]([RoleId])

go

