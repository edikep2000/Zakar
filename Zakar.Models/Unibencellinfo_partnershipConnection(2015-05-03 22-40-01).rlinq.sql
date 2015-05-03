-- add column for field _dateOfLastFailedAccessAttempt
ALTER TABLE [IdentityUser] ADD [DateOfLastFailedAccessAttempt] datetime NULL

go

UPDATE [IdentityUser] SET [DateOfLastFailedAccessAttempt] = getdate()

go

ALTER TABLE [IdentityUser] ALTER COLUMN [DateOfLastFailedAccessAttempt] datetime NOT NULL

go

-- add column for field _failedAccessAttempts
ALTER TABLE [IdentityUser] ADD [FailedAccessAttempts] int NULL

go

UPDATE [IdentityUser] SET [FailedAccessAttempts] = 0

go

ALTER TABLE [IdentityUser] ALTER COLUMN [FailedAccessAttempts] int NOT NULL

go

-- Column was read from database as: [FirstName] varchar(255) not null
-- modify column for field _firstName
ALTER TABLE [IdentityUser] ALTER COLUMN [FirstName] varchar(255) NULL

go

-- Column was read from database as: [LastName] varchar(255) not null
-- modify column for field _lastName
ALTER TABLE [IdentityUser] ALTER COLUMN [LastName] varchar(255) NULL

go

-- Column was read from database as: [PasswordHash] varchar(255) not null
-- modify column for field _passwordHash
ALTER TABLE [IdentityUser] ALTER COLUMN [PasswordHash] varchar(255) NULL

go

-- Column was read from database as: [PhoneNumber] varchar(255) not null
-- modify column for field _phoneNumber
ALTER TABLE [IdentityUser] ALTER COLUMN [PhoneNumber] varchar(255) NULL

go

-- Column was read from database as: [UserName] varchar(255) not null
-- modify column for field _userName
ALTER TABLE [IdentityUser] ALTER COLUMN [UserName] varchar(255) NULL

go

