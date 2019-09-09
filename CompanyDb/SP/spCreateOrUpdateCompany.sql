CREATE PROCEDURE [dbo].[spCreateOrUpdateCompany]
	@CompanyId int = 0, 
    @Name nvarchar(128), 
    @FoundedDate date = null
AS 

      declare @dbId int = (select id from viEmployee where id = @CompanyId) 

			if(@dbId is null) 
      begin 
             INSERT INTO	[dbo].[Company] 
						   ([Name]
						   ,FoundedDate) 
             VALUES 
							(@Name,
							@FoundedDate) 

            set @dbId = @@IDENTITY 
      end 
            else 
      begin 
            update		[dbo].[Company] set 
                        [Name] = case when @Name is null then [Name] else @Name end, 
						FoundedDate = case when @FoundedDate is null then FoundedDate else @FoundedDate end
			WHERE		@dbId = Company.id;
      end 

RETURN	@dbId
