-- Zakar.Models.StagedPartnership
CREATE TABLE [StagedPartnership] (
    [Amount] numeric(20,10) NOT NULL,       -- _amount
    [ArmId] int NULL,                       -- _armId
    [CurrencyId] int NULL,                  -- _currencyId
    [DateCreated] datetime NOT NULL,        -- _dateCreated
    [Id] int IDENTITY NOT NULL,             -- _id
    [mnth] int NULL,                        -- _month
    [PartnerId] int NULL,                   -- _partnerId
    [yr] int NULL,                          -- _year
    CONSTRAINT [pk_StagedPartnership] PRIMARY KEY ([Id])
)

go

