CREATE TABLE [dbo].[User] (
    [UserID]          INT             IDENTITY (1, 1) NOT NULL,
    [AccountTypeID]   INT             NOT NULL,
    [UserName]        NVARCHAR (100)  NOT NULL,
    [ProfileImageURL] NVARCHAR (2083) NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([UserID] ASC),
    CONSTRAINT [FK_Users_AccountType] FOREIGN KEY ([AccountTypeID]) REFERENCES [dbo].[AccountType] ([AccountTypeID])
);

