CREATE TABLE [dbo].[MonsterGenre] (
    [MonsterID] INT NOT NULL,
    [GenreID]   INT NOT NULL,
    CONSTRAINT [PK_MonsterGenre_1] PRIMARY KEY CLUSTERED ([MonsterID] ASC, [GenreID] ASC),
    CONSTRAINT [FK_MonsterGenre_Monster] FOREIGN KEY ([MonsterID]) REFERENCES [dbo].[Monster] ([MonsterID])
);

