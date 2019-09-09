CREATE VIEW [dbo].[viCompany]
	AS SELECT 
		[Company].[Id], 
		[Company].[Name], 
		[Company].[FoundedDate], 
		COUNT(Company2Address.AddressId) AS Locations
	FROM [Company] 
		LEFT OUTER JOIN [Company2Address] 
		ON [Company].[Id] = [Company2Address].[CompanyId]
	WHERE [Company].[DeleteTime] IS NULL
	GROUP BY 
		[Company].[Name], 
		[Company].[Id], 
		[Company].[FoundedDate]
