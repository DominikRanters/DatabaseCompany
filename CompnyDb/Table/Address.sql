﻿CREATE TABLE [dbo].[Address]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Street] NVARCHAR(128) NOT NULL,
	[City] NVARCHAR(128) NOT NULL,
	[Zip] NVARCHAR(128) NOT NULL,
	[Country] NVARCHAR(128) NOT NULL,
	[CreationTime] datetime2 NOT NULL DEFAULT GETDATE(),
	[DeleteTime] datetime2
)
