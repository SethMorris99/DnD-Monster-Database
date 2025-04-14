/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
SET IDENTITY_INSERT [dbo].[SourceBook] ON 
GO
INSERT [dbo].[SourceBook] ([SourceBookID], [Title], [Edition], [YearPublished], [PageNumber]) VALUES (1, N'Terrors of the Withered Wilds', N'1st Edition', 2025, 42)
GO
INSERT [dbo].[SourceBook] ([SourceBookID], [Title], [Edition], [YearPublished], [PageNumber]) VALUES (2, N'Creatures of the Forgotten Wilds', N'1st Edition', 2024, 95)
GO
INSERT [dbo].[SourceBook] ([SourceBookID], [Title], [Edition], [YearPublished], [PageNumber]) VALUES (3, N'Mysteries of the Deep Abyss', N'2nd Edition', 2023, 78)
GO
SET IDENTITY_INSERT [dbo].[SourceBook] OFF
GO
SET IDENTITY_INSERT [dbo].[Monster] ON 
GO
INSERT [dbo].[Monster] ([MonsterID], [SourceBookID], [MonsterName], [ArmorClass], [HitDice], [Attacks], [Alignment], [XP_award], [NumberAppearing], [TreasureType], [SpecialAbilities], [Description], [ImageURL], [UserID]) VALUES (1015, 1, N'Abyssal Thornmaw', 16, 78, N'123', N'1', 1800, N'1', N'C', N'Shadow Merge: Once per day, the Thornmaw can merge with dim light or darkness for 1 minute, gaining resistance to all damage except force and radiant.', N'The Abyssal Thornmaw is a grotesque fusion of carnivorous flora and demonic essence. Resembling a massive, thorn-covered bloom with a gaping maw at its center, it emits a constant low hum that unsettles all nearby. Sprouting from its core are whip-like vines capable of dragging prey into its maw. Born from corrupted druidic rituals gone awry, Thornmaws guard blighted groves and abyss-tainted ruins with fanatical rage.', N'https://static1.cbrimages.com/wordpress/wp-content/uploads/2022/04/ezgif-5-c03a28a068-Cropped.jpg?q=50&fit=crop&w=825&dpr=1.5', 1)
GO
INSERT [dbo].[Monster] ([MonsterID], [SourceBookID], [MonsterName], [ArmorClass], [HitDice], [Attacks], [Alignment], [XP_award], [NumberAppearing], [TreasureType], [SpecialAbilities], [Description], [ImageURL], [UserID]) VALUES (2015, 3, N'Abyssal Thornmaw', 16, 78, N'123', N'1', 1800, N'1', N'C', N'Shadow Merge: Once per day, the Thornmaw can merge with dim light or darkness for 1 minute, gaining resistance to all damage except force and radiant.', N'The Abyssal Thornmaw is a grotesque fusion of carnivorous flora and demonic essence. Resembling a massive, thorn-covered bloom with a gaping maw at its center, it emits a constant low hum that unsettles all nearby. Sprouting from its core are whip-like vines capable of dragging prey into its maw. Born from corrupted druidic rituals gone awry, Thornmaws guard blighted groves and abyss-tainted ruins with fanatical rage.', N'https://static1.cbrimages.com/wordpress/wp-content/uploads/2022/04/ezgif-5-c03a28a068-Cropped.jpg?q=50&fit=crop&w=825&dpr=1.5', 1)
GO
INSERT [dbo].[Monster] ([MonsterID], [SourceBookID], [MonsterName], [ArmorClass], [HitDice], [Attacks], [Alignment], [XP_award], [NumberAppearing], [TreasureType], [SpecialAbilities], [Description], [ImageURL], [UserID]) VALUES (2018, 2, N'Abyssal Thornmaw', 16, 78, N'123', N'1', 1800, N'1', N'C', N'Shadow Merge: Once per day, the Thornmaw can merge with dim light or darkness for 1 minute, gaining resistance to all damage except force and radiant.', N'The Abyssal Thornmaw is a grotesque fusion of carnivorous flora and demonic essence. Resembling a massive, thorn-covered bloom with a gaping maw at its center, it emits a constant low hum that unsettles all nearby. Sprouting from its core are whip-like vines capable of dragging prey into its maw. Born from corrupted druidic rituals gone awry, Thornmaws guard blighted groves and abyss-tainted ruins with fanatical rage.', N'https://static1.cbrimages.com/wordpress/wp-content/uploads/2022/04/ezgif-5-c03a28a068-Cropped.jpg?q=50&fit=crop&w=825&dpr=1.5', 1)
GO
SET IDENTITY_INSERT [dbo].[Monster] OFF
GO
INSERT [dbo].[MonsterGenre] ([MonsterID], [GenreID]) VALUES (1015, 1)
GO
INSERT [dbo].[MonsterGenre] ([MonsterID], [GenreID]) VALUES (2015, 4)
GO
SET IDENTITY_INSERT [dbo].[SavingThrow] ON 
GO
INSERT [dbo].[SavingThrow] ([SavingThrowID], [MonsterID], [DeathSave], [WandsSave], [ParalysisSave], [BreathSave], [SpellsSave]) VALUES (1, 1, 12, 10, 14, 8, 10)
GO
INSERT [dbo].[SavingThrow] ([SavingThrowID], [MonsterID], [DeathSave], [WandsSave], [ParalysisSave], [BreathSave], [SpellsSave]) VALUES (2, 2, 10, 12, 14, 6, 8)
GO
INSERT [dbo].[SavingThrow] ([SavingThrowID], [MonsterID], [DeathSave], [WandsSave], [ParalysisSave], [BreathSave], [SpellsSave]) VALUES (3, 3, 14, 10, 12, 8, 6)
GO
INSERT [dbo].[SavingThrow] ([SavingThrowID], [MonsterID], [DeathSave], [WandsSave], [ParalysisSave], [BreathSave], [SpellsSave]) VALUES (4, 4, 18, 14, 16, 10, 8)
GO
INSERT [dbo].[SavingThrow] ([SavingThrowID], [MonsterID], [DeathSave], [WandsSave], [ParalysisSave], [BreathSave], [SpellsSave]) VALUES (5, 5, 14, 12, 10, 6, 12)
GO
INSERT [dbo].[SavingThrow] ([SavingThrowID], [MonsterID], [DeathSave], [WandsSave], [ParalysisSave], [BreathSave], [SpellsSave]) VALUES (6, 6, 16, 18, 14, 10, 12)
GO
INSERT [dbo].[SavingThrow] ([SavingThrowID], [MonsterID], [DeathSave], [WandsSave], [ParalysisSave], [BreathSave], [SpellsSave]) VALUES (7, 7, 14, 12, 10, 12, 18)
GO
INSERT [dbo].[SavingThrow] ([SavingThrowID], [MonsterID], [DeathSave], [WandsSave], [ParalysisSave], [BreathSave], [SpellsSave]) VALUES (8, 8, 10, 8, 6, 4, 10)
GO
INSERT [dbo].[SavingThrow] ([SavingThrowID], [MonsterID], [DeathSave], [WandsSave], [ParalysisSave], [BreathSave], [SpellsSave]) VALUES (9, 9, 20, 18, 16, 14, 12)
GO
INSERT [dbo].[SavingThrow] ([SavingThrowID], [MonsterID], [DeathSave], [WandsSave], [ParalysisSave], [BreathSave], [SpellsSave]) VALUES (10, 10, 12, 10, 8, 6, 16)
GO
SET IDENTITY_INSERT [dbo].[SavingThrow] OFF
GO
SET IDENTITY_INSERT [dbo].[AccountType] ON 
GO
INSERT [dbo].[AccountType] ([AccountTypeID], [AccountTypeName]) VALUES (1, N'Admin')
GO
INSERT [dbo].[AccountType] ([AccountTypeID], [AccountTypeName]) VALUES (2, N'User')
GO
INSERT [dbo].[AccountType] ([AccountTypeID], [AccountTypeName]) VALUES (3, N'Moderator')
GO
INSERT [dbo].[AccountType] ([AccountTypeID], [AccountTypeName]) VALUES (4, N'Guest')
GO
INSERT [dbo].[AccountType] ([AccountTypeID], [AccountTypeName]) VALUES (5, N'Contributor')
GO
INSERT [dbo].[AccountType] ([AccountTypeID], [AccountTypeName]) VALUES (6, N'Editor')
GO
INSERT [dbo].[AccountType] ([AccountTypeID], [AccountTypeName]) VALUES (7, N'Viewer')
GO
INSERT [dbo].[AccountType] ([AccountTypeID], [AccountTypeName]) VALUES (8, N'Subscriber')
GO
INSERT [dbo].[AccountType] ([AccountTypeID], [AccountTypeName]) VALUES (9, N'Manager')
GO
INSERT [dbo].[AccountType] ([AccountTypeID], [AccountTypeName]) VALUES (10, N'Customer')
GO
SET IDENTITY_INSERT [dbo].[AccountType] OFF
GO
SET IDENTITY_INSERT [dbo].[SystemUser] ON 
GO
INSERT [dbo].[SystemUser] ([SystemUserID], [AccountTypeID], [UserFirstName], [UserLastName], [UserDisplayName], [UserProfileImage], [UserEmail], [UserPassword], [ProfileImageURL], [LastLoginTime]) VALUES (1, 10, N'qwerty', N'qwertty', N'qwerty25', N'default.jpg', N'qwerty@gmail.com', N'$2a$11$5wyLK7fvUmi7p1jiyoo.V.F3fEigddTXq1wt3jyHfWdzz1zS4sCxK', N'https:///static.vecteezy.com/system/resources/previews/009/292/244/non_2x/default-avatar-icon-of-social-media-user-vector.jpg', CAST(N'2025-04-14T11:08:22.840' AS DateTime))
GO
INSERT [dbo].[SystemUser] ([SystemUserID], [AccountTypeID], [UserFirstName], [UserLastName], [UserDisplayName], [UserProfileImage], [UserEmail], [UserPassword], [ProfileImageURL], [LastLoginTime]) VALUES (3, 10, N'Randy', N'Stephens', N'randystephens', N'default.jpg', N'randystephens@gmail.com', N'$2a$11$OJGW.lbqCdcEFcHFXZ/J8OqQrBZaIHbh93836BPQleXioQIOUOXE6', N'https:///static.vecteezy.com/system/resources/previews/009/292/244/non_2x/default-avatar-icon-of-social-media-user-vector.jpg', CAST(N'2025-04-07T14:41:15.847' AS DateTime))
GO
INSERT [dbo].[SystemUser] ([SystemUserID], [AccountTypeID], [UserFirstName], [UserLastName], [UserDisplayName], [UserProfileImage], [UserEmail], [UserPassword], [ProfileImageURL], [LastLoginTime]) VALUES (4, 10, N'Randy', N'Stephens', N'randystephens', N'default.jpg', N'randystephens@gmail.com', N'$2a$11$zl.z/Eb2.IUFsT4xS5umV.ZeF8tcD/1SDEXAI.aRp39/uZhx6jWtS', N'https:///static.vecteezy.com/system/resources/previews/009/292/244/non_2x/default-avatar-icon-of-social-media-user-vector.jpg', CAST(N'2025-04-07T14:39:25.993' AS DateTime))
GO
INSERT [dbo].[SystemUser] ([SystemUserID], [AccountTypeID], [UserFirstName], [UserLastName], [UserDisplayName], [UserProfileImage], [UserEmail], [UserPassword], [ProfileImageURL], [LastLoginTime]) VALUES (1002, 10, N'Tanak', N'Solanki', N'tanakD&DMaster', N'default.jpg', N'tanak.solanki05@gmail.com', N'$2a$11$wFuF8DPzNUwD8868XpNOremMhlMYC6gObrveqc6NW5tpXAVLuM/W.', N'https:///static.vecteezy.com/system/resources/previews/009/292/244/non_2x/default-avatar-icon-of-social-media-user-vector.jpg', CAST(N'2025-04-14T11:26:33.010' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[SystemUser] OFF
GO
SET IDENTITY_INSERT [dbo].[Genre] ON 
GO
INSERT [dbo].[Genre] ([GenreID], [GenreName]) VALUES (1, N'Sci-fi')
GO
INSERT [dbo].[Genre] ([GenreID], [GenreName]) VALUES (2, N'Fantasy')
GO
INSERT [dbo].[Genre] ([GenreID], [GenreName]) VALUES (3, N'Horror')
GO
INSERT [dbo].[Genre] ([GenreID], [GenreName]) VALUES (4, N'Other')
GO
SET IDENTITY_INSERT [dbo].[Genre] OFF
GO
