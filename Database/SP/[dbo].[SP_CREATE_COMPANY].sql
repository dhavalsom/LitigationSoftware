USE [LitigationApp]
GO

/****** Object:  StoredProcedure [dbo].[SP_CREATE_COMPANY]    Script Date: 5/18/2019 7:13:09 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO







ALTER PROCEDURE [dbo].[SP_CREATE_COMPANY]
(
	@COMPANY_DETAIL_XML AS XML,
	@USER_ID BIGINT = 1 --SET THE DEFAULT VALUE TO 1 IF NOT PASSED
)
AS

BEGIN

/*BLOCK TO READ THE VARIABLES*/

DECLARE @Id AS BIGINT
DECLARE @CompanyName AS NVARCHAR(MAX), @PANNumber AS NVARCHAR(MAX),@LoggedInUserID as BIGINT, 
@CategoryID AS BIGINT

SELECT 
	   @Id = CompanyDetailList.Columns.value('Id[1]','bigint')
	   ,@CompanyName = CompanyDetailList.Columns.value('CompanyName[1]', 'nvarchar(max)')
	   , @PANNumber = CompanyDetailList.Columns.value('PANNumber[1]', 'nvarchar(max)')
	   ,@CategoryID = CompanyDetailList.Columns.value('CategoryID[1]', 'bigint')
FROM   @COMPANY_DETAIL_XML.nodes('Company') AS CompanyDetailList(Columns)

/*BLOCK TO READ THE VARIABLES ENDS HERE*/



IF @Id IS NULL OR @Id = 0 
	BEGIN
		
			INSERT INTO [dbo].[CompanyMaster]
			   ([CompanyName]
			   ,[PANNumber]
			   ,[CategoryID]
			   ,[Active]
			   ,[AddedBy]
			   ,[AddedDate])
			VALUES
			   (@CompanyName
			   ,@PANNumber
			   ,@CategoryID
			   ,1
			   ,@USER_ID
			   ,GETUTCDATE()
				)
			/*INSERT USER DETAIL BLOCK ENDS HERE*/

			

END
ELSE
	BEGIN

	update [dbo].[CompanyMaster] set Active = 0,ModifiedBy=@USER_ID,ModifiedDate=GETUTCDATE() where id = @id

	END
END



GO


