CREATE TABLE [dbo].[SavingThrow] (
    [SavingThrowID] INT IDENTITY (1, 1) NOT NULL,
    [MonsterID]     INT NOT NULL,
    [DeathSave]     INT NOT NULL,
    [WandsSave]     INT NOT NULL,
    [ParalysisSave] INT NOT NULL,
    [BreathSave]    INT NOT NULL,
    [SpellsSave]    INT NOT NULL,
    CONSTRAINT [PK_SavingThrows] PRIMARY KEY CLUSTERED ([SavingThrowID] ASC)
);

