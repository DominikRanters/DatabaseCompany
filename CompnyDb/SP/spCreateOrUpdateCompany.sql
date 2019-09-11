CREATE PROCEDURE [dbo].[spCreateOrUpdateCompany]
	@CompanyId int = 0, 
    @Name nvarchar(128), 
    @FoundedDate date = null
AS 

      declare @dbId int = (select Id from viCompany where Id = @CompanyId) 

			if(@dbId is null) 
      begin 
             INSERT INTO	[dbo].[Company] 
						   ([Name]
						   ,FoundedDate) 
             VALUES 
							(@Name,
							@FoundedDate) 

            set @dbId = SCOPE_IDENTITY(); 
      end 
            else 
      begin 
            update		[dbo].[Company] set 
                        [Name] = case when @Name is null then [Name] else @Name end, 
						FoundedDate = case when @FoundedDate is null then FoundedDate else @FoundedDate end
			WHERE		@dbId = Company.Id;
      end 

SELECT	@dbId
