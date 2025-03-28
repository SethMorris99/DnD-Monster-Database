CREATE TABLE [dbo].[SourceBook] (
    [SourceBookID]  INT            IDENTITY (1, 1) NOT NULL,
    [Title]         NVARCHAR (255) NOT NULL,
    [Edition]       NVARCHAR (50)  NOT NULL,
    [YearPublished] INT            NOT NULL,
    [PageNumber]    INT            NULL,
    CONSTRAINT [PK_SourceBook] PRIMARY KEY CLUSTERED ([SourceBookID] ASC)
);

