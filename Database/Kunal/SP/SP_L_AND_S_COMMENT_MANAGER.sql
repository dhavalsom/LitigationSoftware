USE [LitigationApp]
GO

/****** Object:  StoredProcedure [dbo].[SP_L_AND_S_COMMENT_MANAGER]    Script Date: 11/25/2018 11:29:41 AM ******/
DROP PROCEDURE [dbo].[SP_L_AND_S_COMMENT_MANAGER]
GO

/****** Object:  StoredProcedure [dbo].[SP_L_AND_S_COMMENT_MANAGER]    Script Date: 11/25/2018 11:29:41 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SP_L_AND_S_COMMENT_MANAGER]
(
	@L_AND_S_COMMENT_XML AS XML,
	@OPERATION AS NVARCHAR(100) = NULL, --COMPLIANCE_DOCUMENT FOR START/UPDATE COMPLIANCE_DOCUMENT RECORD
	@USER_ID BIGINT = 1 --SET THE DEFAULT VALUE TO 1 IF NOT PASSED,
)
AS

BEGIN


/*BLOCK TO READ THE VARIABLES*/

DECLARE @Id AS BIGINT, @CompanyId AS BIGINT, @ITSubHeadId AS BIGINT
DECLARE @Comment AS NVARCHAR(MAX), @ReturnMessage as NVARCHAR(MAX)
DECLARE @Active AS BIT, @Result as BIT

SELECT	 @Id = LAndSCommentList.Columns.value('Id[1]', 'BIGINT')
	   , @CompanyId = LAndSCommentList.Columns.value('CompanyId[1]', 'BIGINT')
	   , @ITSubHeadId = LAndSCommentList.Columns.value('ITSubHeadId[1]', 'BIGINT')
	   , @Comment = LAndSCommentList.Columns.value('Comment[1]', 'nvarchar(max)')
	   , @Active = LAndSCommentList.Columns.value('Active[1]', 'bit')
FROM   @L_AND_S_COMMENT_XML.nodes('LAndSComments') AS LAndSCommentList(Columns)

/*BLOCK TO READ THE VARIABLES ENDS HERE*/
IF @OPERATION IS NULL OR @OPERATION = ''
BEGIN

	IF @Id IS NULL OR @Id =0 
	BEGIN

	INSERT INTO [dbo].[LAndSComments]
           ([CompanyId]
           ,[ITSubHeadId]
           ,[Comment]
           ,[Active]
           ,[AddedBy]
           ,[AddedDate])
     VALUES
           (@CompanyId
		   ,@ITSubHeadId
           ,@Comment
           ,@Active
		   ,@USER_ID
		   ,GETUTCDATE())

			SET @Result = 1;
			SET @ReturnMessage = 'Record created successfully.'
	END

	ELSE
	BEGIN

		UPDATE [dbo].[LAndSComments]
		   SET [Comment] = ISNULL(@Comment,[Comment])
			  ,[ModifiedBy] = @USER_ID
			  ,[ModifiedDate] = GETUTCDATE()
		WHERE Id = @Id
		SET @Result = 1;
		SET @ReturnMessage = 'Record updated successfully.'
	END
END

IF @OPERATION = 'Delete'
BEGIN
	UPDATE [dbo].[LAndSComments]
		   SET [Active] = 0
			  ,[DeletedBy] = @USER_ID
			  ,[DeletedDate] = GETUTCDATE()
		WHERE Id = @Id
		SET @Result = 1;
		SET @ReturnMessage = 'Record deleted successfully.'
END
SELECT @Result AS Result, @ReturnMessage AS ReturnMessage
END







GO


