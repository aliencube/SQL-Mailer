/*
		01. Register CLR assemblies
	**	02. Creates stored procedures
*/
USE [MyDatabase]
GO

-- Drops existing stored procedures.
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'usp_SendMail')
BEGIN
	DROP PROCEDURE [usp_SendMail]
END

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- Sends emails through CLR assembly.
CREATE PROCEDURE [dbo].[usp_SendMail]
    @applicationName	AS NVARCHAR(256),
    @body				AS NVARCHAR(MAX)
AS
EXTERNAL NAME [SqlMailer].[Aliencube.SqlMailer.Clr.SqlMailer].[SendMail]
GO

USE [master]
GO
