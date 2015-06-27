-- Zakar.Models.Currency
CREATE TABLE [Currency] (
    [ConversionRateToDefault] numeric(20,10) NOT NULL, -- _conversionRateToDefault
    [Id] int IDENTITY NOT NULL,             -- _id
    [IsDefaultCurrency] tinyint NOT NULL,   -- _isDefaultCurrency
    [nme] varchar(255) NULL,                -- _name
    [Symbol] varchar(255) NULL,             -- _symbol
    CONSTRAINT [pk_Currency] PRIMARY KEY ([Id])
)

go

ALTER TABLE [Partnership] ADD CONSTRAINT [ref_Partnership_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Currency]([Id])

go

ALTER TABLE [Partnership] ADD CONSTRAINT [ref_Partnership_Partner] FOREIGN KEY ([PartnerId]) REFERENCES [Partner]([Id])

go

ALTER TABLE [Partnership] ADD CONSTRAINT [ref_Partnership_PartnershipArm] FOREIGN KEY ([PartnershipArmId]) REFERENCES [PartnershipArm]([Id])

go

-- Index 'idx_Partnership_CurrencyId' was not detected in the database. It will be created
CREATE INDEX [idx_Partnership_CurrencyId] ON [Partnership]([CurrencyId])

go

-- Index 'idx_Partnership_PartnerId' was not detected in the database. It will be created
CREATE INDEX [idx_Partnership_PartnerId] ON [Partnership]([PartnerId])

go

-- Index 'idx_Prtnrship_PartnershipArmId' was not detected in the database. It will be created
CREATE INDEX [idx_Prtnrship_PartnershipArmId] ON [Partnership]([PartnershipArmId])

go

