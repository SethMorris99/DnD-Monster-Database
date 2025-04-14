CREATE TABLE [dbo].[Monster] (
    [MonsterID]        INT            IDENTITY (1, 1) NOT NULL,
    [SourceBookID]     INT            NOT NULL,
    [MonsterName]      NVARCHAR (250) NOT NULL,
    [ArmorClass]       INT            NOT NULL,
    [HitDice]          INT            NOT NULL,
    [Attacks]          NVARCHAR (500) NOT NULL,
    [Alignment]        NVARCHAR (20)  NOT NULL,
    [XP_award]         INT            NOT NULL,
    [NumberAppearing]  NVARCHAR (50)  NOT NULL,
    [TreasureType]     CHAR (1)       NOT NULL,
    [SpecialAbilities] NVARCHAR (MAX) NOT NULL,
    [Description]      NVARCHAR (MAX) NOT NULL,
    [ImageURL]         NVARCHAR (MAX) NOT NULL,
    [UserID]           INT            NOT NULL,
    CONSTRAINT [PK_Monsters] PRIMARY KEY CLUSTERED ([MonsterID] ASC),
    CONSTRAINT [FK_Monsters_SourceBook] FOREIGN KEY ([SourceBookID]) REFERENCES [dbo].[SourceBook] ([SourceBookID])
);

