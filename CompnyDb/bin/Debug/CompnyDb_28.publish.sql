﻿/*
Deployment script for Training-DS-Company

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "Training-DS-Company"
:setvar DefaultFilePrefix "Training-DS-Company"
:setvar DefaultDataPath "D:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\"
:setvar DefaultLogPath "D:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
PRINT N'Altering [dbo].[spCreateOrUpdsteEmployee]...';


GO
ALTER PROCEDURE [dbo].[spCreateOrUpdsteEmployee]
	@EmployeeId int = 0, 
    @FirstName nvarchar(128), 
    @LastName nvarchar(128), 
    @BirthDay date, 
    @DepartmentId int ,
	@A NVARCHAR(64) = "Carl"
AS 

      declare @dbId int = (select id from viEmployee where id = @EmployeeId) 

      if(@dbId is null) 
      begin 
             INSERT INTO	[dbo].[Employee] 
						   ([FirstName] 
						   ,[LastName] 
						   ,[BirthDay] 
						   ,[DepartmentId]) 
             VALUES 
							(@FirstName,
							@LastName,
							@BirthDay,
							@DepartmentId) 

            set @dbId = @@IDENTITY 
      end 
            else 
      begin 
            update		[dbo].[Employee] set 
                        FirstName = case when @FirstName is null then (SELECT FirstName FROM Employee WHERE @dbId = Id) else @A end, 
                        Birthday = case when @Birthday is null then Birthday else @Birthday end, 
						LastName = case when @LastName is null then LastName else @LastName end, 
						DepartmentId = case when @DepartmentId is null then DepartmentId else @DepartmentId end
			WHERE		@dbId = Employee.id;
      end 

RETURN @dbId;
GO
PRINT N'Update complete.';


GO
