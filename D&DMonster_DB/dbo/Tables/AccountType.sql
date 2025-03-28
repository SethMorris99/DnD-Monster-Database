CREATE TABLE [dbo].[AccountType] (
    [AccountTypeID]   INT            IDENTITY (1, 1) NOT NULL,
    [AccountTypeName] NVARCHAR (100) NOT NULL,
    CONSTRAINT [PK_AccountType] PRIMARY KEY CLUSTERED ([AccountTypeID] ASC)
);

