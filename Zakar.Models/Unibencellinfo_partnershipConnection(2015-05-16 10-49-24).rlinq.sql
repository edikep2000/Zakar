-- add column for field _gender
ALTER TABLE [Partner] ADD [Gender] varchar(100) NULL

go

UPDATE [Partner] SET [Gender] = ' '

go

ALTER TABLE [Partner] ALTER COLUMN [Gender] varchar(100) NOT NULL

go

