-- add column for field _gender
ALTER TABLE [StagedPartner] ADD [Gender] varchar(100) NULL

go

UPDATE [StagedPartner] SET [Gender] = ' '

go

ALTER TABLE [StagedPartner] ALTER COLUMN [Gender] varchar(100) NOT NULL

go

