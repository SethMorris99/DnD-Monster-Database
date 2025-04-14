CREATE TABLE [dbo].[MonsterAbility] (
    [AbilityID]     INT            NOT NULL,
    [MonsterID]     INT            IDENTITY (1, 1) NOT NULL,
    [AbilityName]   NVARCHAR (500) NOT NULL,
    [AbilityEffect] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_MonsterAbilites] PRIMARY KEY CLUSTERED ([AbilityID] ASC),
    CONSTRAINT [FK_MonsterAbilities_Monsters] FOREIGN KEY ([MonsterID]) REFERENCES [dbo].[Monster] ([MonsterID])
);

