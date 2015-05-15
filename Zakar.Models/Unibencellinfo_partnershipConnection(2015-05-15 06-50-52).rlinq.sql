-- dropping unknown column [AdminId]
ALTER TABLE [Church] DROP COLUMN [AdminId]

go

-- add column for field _churchId
ALTER TABLE [IdentityUser] ADD [ChurchId] int NULL

go

-- Column was read from database as: [FailedAccessAttempts] int not null
-- modify column for field _failedAccessAttempts
ALTER TABLE [IdentityUser] ALTER COLUMN [FailedAccessAttempts] int NULL

go

-- add column for field _groupId
ALTER TABLE [IdentityUser] ADD [GroupId] int NULL

go

-- Column was read from database as: [UserName] varchar(255) not null
-- modify column for field _userName
ALTER TABLE [IdentityUser] ALTER COLUMN [UserName] varchar(255) NULL

go

-- add column for field _zoneId
ALTER TABLE [IdentityUser] ADD [ZoneId] int NULL

go

-- dropping unknown column [AdminId]
ALTER TABLE [Zone] DROP COLUMN [AdminId]

go

