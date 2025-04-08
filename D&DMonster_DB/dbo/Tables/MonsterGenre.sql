CREATE TABLE [dbo].[MonsterGenre] (
    [MonsterID] INT NOT NULL,
    [GenreID]   INT NOT NULL,
    PRIMARY KEY CLUSTERED ([MonsterID] ASC, [GenreID] ASC),
    FOREIGN KEY ([GenreID]) REFERENCES [dbo].[Genre] ([GenreID]),
    FOREIGN KEY ([MonsterID]) REFERENCES [dbo].[Monster] ([MonsterID])
);

