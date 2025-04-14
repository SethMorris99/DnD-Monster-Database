CREATE TABLE [dbo].[MonsterGenre] (
    [MonsterID] INT NOT NULL,
    [GenreID]   INT NOT NULL,
    PRIMARY KEY CLUSTERED ([MonsterID] ASC, [GenreID] ASC),
    FOREIGN KEY ([GenreID]) REFERENCES [dbo].[Genre] ([GenreID]),
    CONSTRAINT [FK__MonsterGe__Monst__6FE99F9F] FOREIGN KEY ([MonsterID]) REFERENCES [dbo].[Monster] ([MonsterID])
);

