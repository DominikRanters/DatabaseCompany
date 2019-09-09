CREATE TABLE [dbo].[Company]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Name] nvarchar(256) NOT NULL,
	[FoundedDate] date DEFAULT GETDATE(),
	[CreationTime] datetime2 NOT NULL DEFAULT GETDATE(),
	[DeleteTime] datetime2 ,)
