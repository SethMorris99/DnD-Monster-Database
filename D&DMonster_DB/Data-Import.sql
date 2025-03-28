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
