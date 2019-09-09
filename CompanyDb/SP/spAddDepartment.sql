CREATE PROCEDURE [dbo].[spAddDepartment]
	@Name NVARCHAR(256),
	@Discription NVARCHAR(MAX),
	@CompanyId INT
AS
	IF NOT EXISTS (
		SELECT	[Id] 
		FROM	[viDepartment]
		WHERE	[Name] = @Name AND 
				[Description]  = @Discription AND
				CompanyId = @CompanyId
		)
		BEGIN
			INSERT INTO [Department] 
				([Name],
				[Description],
				CompanyId)
			VALUES 
				(@Name,
				@Discription,
				@CompanyId)

			RETURN 1
		END
	ELSE
		RETURN 0
