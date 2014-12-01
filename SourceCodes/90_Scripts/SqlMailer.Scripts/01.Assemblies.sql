/*
	**	01. Register CLR assemblies
		02. Creates stored procedures
*/
USE [MyDatabase]
GO

-- Changes the database owner to aliencube.
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'MyDatabase' AND SUSER_SNAME(owner_sid) = 'aliencube')
BEGIN
	EXEC sp_changedbowner 'aliencube'
END

-- Turns on TRUSTWORTHY to register CLR assembly with EXTERNAL_ACCESS.
-- NOTE: This statement will roll back any open transactions.
IF EXISTS (SELECT * FROM sys.databases WHERE name = 'MyDatabase' AND is_trustworthy_on = 0)
BEGIN
	ALTER DATABASE [MyDatabase]
		SET TRUSTWORTHY ON
		WITH ROLLBACK IMMEDIATE
END
GO

-- Drops existing CLR assemblies.
IF EXISTS (SELECT * FROM sys.assemblies WHERE name = 'SqlMailer')
BEGIN
	DROP ASSEMBLY [SqlMailer]
END

-- Registers the CLR assembly for SqlMailer.
CREATE ASSEMBLY [SqlMailer]
	FROM 'C:\Temp\Aliencube.SqlMailer.Clr\Aliencube.SqlMailer.Clr.dll'
	WITH PERMISSION_SET = EXTERNAL_ACCESS   
GO

USE [master]
GO
