USE [LitigationApp]
GO

/****** Object:  StoredProcedure [dbo].[SP_CREATE_COMPANY]    Script Date: 4/8/2018 5:34:57 PM ******/
DROP PROCEDURE [dbo].[SP_CREATE_COMPANY]
GO

/****** Object:  StoredProcedure [dbo].[SP_CREATE_COMPANY]    Script Date: 4/8/2018 5:34:57 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






CREATE PROCEDURE [dbo].[SP_CREATE_COMPANY]
(
	@COMPANY_DETAIL_XML AS XML,
	@USER_ID BIGINT = 1 --SET THE DEFAULT VALUE TO 1 IF NOT PASSED
)
AS

BEGIN

/*BLOCK TO READ THE VARIABLES*/

DECLARE @Id AS BIGINT
DECLARE @CompanyName AS NVARCHAR(MAX), @PANNumber AS NVARCHAR(MAX),@LoggedInUserID as BIGINT  

SELECT 
	   @CompanyName = CompanyDetailList.Columns.value('CompanyName[1]', 'nvarchar(max)')
	   , @PANNumber = CompanyDetailList.Columns.value('PANNumber[1]', 'nvarchar(max)')
FROM   @COMPANY_DETAIL_XML.nodes('Company') AS CompanyDetailList(Columns)

/*BLOCK TO READ THE VARIABLES ENDS HERE*/




BEGIN
		
			INSERT INTO [dbo].[CompanyMaster]
			   ([CompanyName]
			   ,[PANNumber]
			   ,[Active]
			   ,[AddedBy]
			   ,[AddedDate])
			VALUES
			   (@CompanyName
			   ,@PANNumber
			   ,1
			   ,@USER_ID
			   ,GETUTCDATE()
				)
			/*INSERT USER DETAIL BLOCK ENDS HERE*/

			

END
END


GO


