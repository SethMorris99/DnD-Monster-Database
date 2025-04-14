CREATE TABLE [dbo].[MonsterGenre] (
    [MonsterID] INT NOT NULL,
    [GenreID]   INT NOT NULL,
    CONSTRAINT [PK_MonsterGenre] PRIMARY KEY CLUSTERED ([MonsterID] ASC),
    CONSTRAINT [FK_MonsterGenre_Monster] FOREIGN KEY ([MonsterID]) REFERENCES [dbo].[Monster] ([MonsterID])
);

