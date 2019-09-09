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
PRINT N'Altering [dbo].[viCompany]...';


GO
ALTER VIEW [dbo].[viCompany]
	AS SELECT [Company].[Id], [Company].[Name], [Company].[FoundedDate], COUNT(Company2Address.AddressId) AS LocationNumber FROM [Company2Address] 
	FULL OUTER JOIN [Company] 
	ON [Company].[Id] = [Company2Address].[CompanyId]
	LEFT JOIN [Address]
	ON [Address].[Id] = [Company2Address].[AddressId]
	WHERE [Address].[DeleteTime] IS NULL OR [Company].[DeleteTime] IS NULL
	GROUP BY [Company].[Name], [Company].[Id], [Company].[FoundedDate]
GO
PRINT N'Update complete.';


GO
