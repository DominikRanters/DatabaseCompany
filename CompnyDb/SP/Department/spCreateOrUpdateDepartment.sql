CREATE PROCEDURE [dbo].[spCreateOrUpdateDepartment]
	@DepartmentId int = 0,
	@Name nvarchar(128),
	@Description nvarchar(max),
	@CompanyId int  = 0
AS
	
	declare @dbId int = (SELECT Id FROM viDepartment WHERE Id = @DepartmentId)

		if(@dbId IS NULL)
	begin 
		INSERT INTO [dbo].Department
					([Name]
					,[Description]
					,[CompanyId])
		VALUES
					(@Name
					,@Description
					,@CompanyId)

		set @dbId = SCOPE_IDENTITY();
	end
		else
	begin
		UPDATE		[dbo].Department SET
					[Name] = case when @Name is null then [Name] else @Name end,
					[Description] = case when @Description is null then [Description] else @Description end,
					[CompanyId] = case when @CompanyId is null then [CompanyId] else @CompanyId end
		WHERE		Id = @dbId;
	end

SELECT @dbId
