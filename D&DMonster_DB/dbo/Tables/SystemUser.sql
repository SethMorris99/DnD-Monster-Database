CREATE TABLE [dbo].[SystemUser] (
    [SystemUserID]     INT             IDENTITY (1, 1) NOT NULL,
    [AccountTypeID]    INT             NOT NULL,
    [UserFirstName]    NVARCHAR (50)   NOT NULL,
    [UserLastName]     NVARCHAR (50)   NOT NULL,
    [UserProfileImage] NVARCHAR (50)   NULL,
    [UserEmail]        NVARCHAR (200)  NOT NULL,
    [UserPassword]     NVARCHAR (100)  NOT NULL,
    [ProfileImageURL]  NVARCHAR (2083) NOT NULL,
    [LastLoginTime]    DATETIME        NULL,
    CONSTRAINT [PK_SystemUser] PRIMARY KEY CLUSTERED ([SystemUserID] ASC),
    CONSTRAINT [FK_SystemUser_AccountType] FOREIGN KEY ([AccountTypeID]) REFERENCES [dbo].[AccountType] ([AccountTypeID])
);

