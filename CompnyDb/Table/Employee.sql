CREATE TABLE [dbo].[Employee]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[FirstName] nvarchar(256) NOT NULL,
	[LastName] nvarchar(256) NOT NULL,
	[Birthday] date,
	[CreationTime] datetime2 NOT NULL DEFAULT GETDATE(),
	[DeleteTime] datetime2,
	[DepartmentId] INT NOT NULL REFERENCES Department(Id),
	[AddressId] INT REFERENCES [Address](Id), 
)
