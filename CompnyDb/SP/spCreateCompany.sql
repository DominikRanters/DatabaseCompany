CREATE PROCEDURE [dbo].[spCreateCompany]
	@Name NVARCHAR(256),
	@FoundedDate DATE
AS
	IF NOT EXISTS (
		SELECT [Id] 
		FROM [Company]
		WHERE [Name] = @Name AND [FoundedDate] = @FoundedDate
	)
		BEGIN
			INSERT INTO [Company] 
				([Name],
				[FoundedDate])
			VALUES 
				(@Name,
				@FoundedDate)

			RETURN 1
		END
	ELSE
		RETURN 0
