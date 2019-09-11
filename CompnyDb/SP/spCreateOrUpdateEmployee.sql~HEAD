CREATE PROCEDURE [dbo].[spCreateOrUpdateEmployee]
	@EmployeeId int = 0, 
    @FirstName nvarchar(128), 
    @LastName nvarchar(128), 
    @BirthDay date, 
    @DepartmentId int,
	@AdressId int
AS 

      declare @dbId int = (select id from viEmployee where id = @EmployeeId) 

			if(@dbId is null) 
      begin 
             INSERT INTO	[dbo].[Employee] 
						   ([FirstName] 
						   ,[LastName] 
						   ,[BirthDay] 
						   ,[DepartmentId]
						   ,AddressId) 
             VALUES 
							(@FirstName,
							@LastName,
							@BirthDay,
							@DepartmentId,
							@AdressId) 

            set @dbId = @@IDENTITY 
      end 
            else 
      begin 
            update		[dbo].[Employee] set 
                        FirstName = case when @FirstName is null then FirstName else @FirstName end, 
                        Birthday = case when @Birthday is null then Birthday else @Birthday end, 
						LastName = case when @LastName is null then LastName else @LastName end, 
						DepartmentId = case when @DepartmentId is null then DepartmentId else @DepartmentId end,
						@AdressId = case when @AdressId is null then AddressId else @AdressId end
			WHERE		@dbId = Employee.id;
      end 

RETURN	@dbId