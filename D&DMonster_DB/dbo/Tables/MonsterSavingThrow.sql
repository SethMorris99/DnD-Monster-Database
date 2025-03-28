CREATE TABLE [dbo].[MonsterSavingThrow] (
    [MonsterSavingThrowID] INT IDENTITY (1, 1) NOT NULL,
    [MonsterID]            INT NOT NULL,
    [SavingThrowID]        INT NOT NULL,
    CONSTRAINT [PK_MonsterSavingThrows] PRIMARY KEY CLUSTERED ([MonsterSavingThrowID] ASC),
    CONSTRAINT [FK_MonsterSavingThrows_Monsters] FOREIGN KEY ([MonsterID]) REFERENCES [dbo].[Monster] ([MonsterID]),
    CONSTRAINT [FK_MonsterSavingThrows_SavingThrows] FOREIGN KEY ([SavingThrowID]) REFERENCES [dbo].[SavingThrow] ([SavingThrowID])
);

