CREATE TABLE [dbo].[Company2Address]
(
	[CompanyId] INT NOT NULL REFERENCES [Company](Id),
	[AddressId] INT NOT NULL REFERENCES [Address](Id),
	[CreationTime] DATETIME2 NOT NULL DEFAULT GETDATE(),
	CONSTRAINT Id PRIMARY KEY ([CompanyId], [AddressId]),
)
