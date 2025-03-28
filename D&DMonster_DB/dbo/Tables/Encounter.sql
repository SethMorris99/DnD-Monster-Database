CREATE TABLE [dbo].[Encounter] (
    [EncounterID]    INT            IDENTITY (1, 1) NOT NULL,
    [UserID]         INT            NOT NULL,
    [MonsterID]      INT            NOT NULL,
    [Location]       NVARCHAR (255) NOT NULL,
    [EncounterLevel] INT            NOT NULL,
    CONSTRAINT [PK_Ecounters] PRIMARY KEY CLUSTERED ([EncounterID] ASC),
    CONSTRAINT [FK_Ecounters_Users] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([UserID]),
    CONSTRAINT [FK_Encounters_Monsters] FOREIGN KEY ([MonsterID]) REFERENCES [dbo].[Monster] ([MonsterID])
);

