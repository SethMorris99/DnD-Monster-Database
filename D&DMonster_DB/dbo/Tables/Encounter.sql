CREATE TABLE [dbo].[Encounter] (
    [EncounterID]    INT            IDENTITY (1, 1) NOT NULL,
    [SystemUserID]   INT            NOT NULL,
    [MonsterID]      INT            NOT NULL,
    [Location]       NVARCHAR (255) NOT NULL,
    [EncounterLevel] INT            NOT NULL,
    CONSTRAINT [PK_Ecounters] PRIMARY KEY CLUSTERED ([EncounterID] ASC),
    CONSTRAINT [FK_Encounters_Monsters] FOREIGN KEY ([MonsterID]) REFERENCES [dbo].[Monster] ([MonsterID]),
    CONSTRAINT [FK_Encounters_SystemUsers] FOREIGN KEY ([SystemUserID]) REFERENCES [dbo].[SystemUser] ([SystemUserID])
);

