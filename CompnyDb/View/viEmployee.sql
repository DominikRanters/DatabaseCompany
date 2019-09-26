CREATE VIEW [dbo].[viEmployee]
	AS SELECT 

		[Employee].[Id], 
		[Employee].[FirstName], 
		[Employee].[LastName],  
		[Employee].[Birthday],  
		[Employee].[DepartmentId],
		[Employee].[AddressId]
	FROM [Employee]
	WHERE [Employee].[DeleteTime] IS NULL

	--	[Employee].[Id], 
	--	[Employee].[FirstName], 
	--	[Employee].[LastName],  
	--	[Employee].[Birthday],  
	--	[Employee].[DepartmentId], 
	--	[Address].[Zip], 
	--	[Address].[City], 
	--	[Address].[Street], 
	--	[Address].[Country] 
	--FROM [Employee] 
	--LEFT JOIN [Address]
	--	ON [Employee].[AddressId] = [Address].[Id]
	--WHERE [Employee].[DeleteTime] IS NULL OR [Address].[DeleteTime] IS NULL
	