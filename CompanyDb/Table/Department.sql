CREATE TABLE [dbo].[Department]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Name] nvarchar(256) NOT NULL,
	[Description] nvarchar(max),
	[CreationTime] datetime2 NOT NULL DEFAULT GETDATE(),
	[DeleteTime] datetime2,
	[CompanyId] int NOT NULL REFERENCES Company(Id),
)
