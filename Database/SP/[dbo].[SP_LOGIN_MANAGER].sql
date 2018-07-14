USE [LitigationApp]
GO

/****** Object:  StoredProcedure [dbo].[SP_LOGIN_MANAGER]    Script Date: 7/15/2018 1:11:56 AM ******/
DROP PROCEDURE [dbo].[SP_LOGIN_MANAGER]
GO

/****** Object:  StoredProcedure [dbo].[SP_LOGIN_MANAGER]    Script Date: 7/15/2018 1:11:56 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_LOGIN_MANAGER]
(
	@USERNAME NVARCHAR(100),
	@PASSWORD NVARCHAR(100)
)
AS

BEGIN

--EXEC [dbo].[SP_LOGIN_MANAGER_2] 'kunalsmehtajobs@gmail.com','10dulkar'
--EXEC [dbo].[SP_LOGIN_MANAGER_2] 'dhavalsom@gmail.com','admin'

DECLARE @USER_ID AS BIGINT
DECLARE @IS_USER_ACTIVE AS BIT
SET @IS_USER_ACTIVE = 0

	SELECT @USER_ID = UD.Id, @IS_USER_ACTIVE = UD.[Active]
	FROM UserDetail UD
		WHERE (EmailAddress = @USERNAME OR Phonenumber = @USERNAME)
		AND [Password] = @PASSWORD	

/*IF USER IS FOUND WITH SAME USER ID AND PWD, GO AHEAD*/
IF @USER_ID IS NOT NULL AND @IS_USER_ACTIVE = 1
	
	BEGIN
		SELECT Id, FirstName, LastName, EmailAddress, PhoneNumber, [Password]
		FROM UserDetail UD
		WHERE UD.Id = @USER_ID
	END

END











GO


