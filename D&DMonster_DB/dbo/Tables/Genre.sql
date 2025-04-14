CREATE TABLE [dbo].[Genre] (
    [GenreID]   INT           IDENTITY (1, 1) NOT NULL,
    [GenreName] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK__Genre__0385055E86841F40] PRIMARY KEY CLUSTERED ([GenreID] ASC),
    CONSTRAINT [FK_Genre_Genre] FOREIGN KEY ([GenreID]) REFERENCES [dbo].[Genre] ([GenreID])
);

