USE [LitigationApp]
GO

/****** Object:  StoredProcedure [dbo].[SP_IT_RETURN_DOCUMENT_MANAGER]    Script Date: 9/3/2018 10:52:23 PM ******/
DROP PROCEDURE [dbo].[SP_IT_RETURN_DOCUMENT_MANAGER]
GO

/****** Object:  StoredProcedure [dbo].[SP_IT_RETURN_DOCUMENT_MANAGER]    Script Date: 9/3/2018 10:52:23 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_IT_RETURN_DOCUMENT_MANAGER]
(
	@IT_RETURN_DOCUMENT_XML AS XML,
	@OPERATION AS NVARCHAR(100) = NULL, --COMPLIANCE_DOCUMENT FOR START/UPDATE COMPLIANCE_DOCUMENT RECORD
	@USER_ID BIGINT = 1 --SET THE DEFAULT VALUE TO 1 IF NOT PASSED,
)
AS

BEGIN


/*BLOCK TO READ THE VARIABLES*/

DECLARE @Id AS BIGINT, @ITReturnDetailsId AS BIGINT, @ITHeadId AS BIGINT
DECLARE @FileName AS NVARCHAR(MAX),@FilePath AS NVARCHAR(MAX), @ReturnMessage as NVARCHAR(MAX)
DECLARE @Active AS BIT, @Result as BIT

SELECT	 @Id = ITReturnDocumentList.Columns.value('Id[1]', 'BIGINT')
	   , @ITReturnDetailsId = ITReturnDocumentList.Columns.value('ITReturnDetailsId[1]', 'BIGINT')
	   , @ITHeadId = ITReturnDocumentList.Columns.value('ITHeadId[1]', 'BIGINT')
	   , @FileName = ITReturnDocumentList.Columns.value('FileName[1]', 'nvarchar(max)')
	   , @FilePath = ITReturnDocumentList.Columns.value('FilePath[1]', 'nvarchar(max)')
	   , @Active = ITReturnDocumentList.Columns.value('Active[1]', 'bit')
FROM   @IT_RETURN_DOCUMENT_XML.nodes('ITReturnDocuments') AS ITReturnDocumentList(Columns)

/*BLOCK TO READ THE VARIABLES ENDS HERE*/
IF @OPERATION IS NULL OR @OPERATION = ''
BEGIN

	IF @Id IS NULL OR @Id =0 
	BEGIN

	INSERT INTO [dbo].[ITReturnDocuments]
           ([ITReturnDetailsId]
		   ,[ITHeadId]
           ,[FileName]
           ,[FilePath]
           ,[Active]
           ,[AddedBy]
           ,[AddedDate])
     VALUES
           (@ITReturnDetailsId
		   ,@ITHeadId
           ,@FileName
           ,@FilePath
           ,@Active
		   ,@USER_ID
		   ,GETUTCDATE())

			SET @Result = 1;
			SET @ReturnMessage = 'Record created successfully.'
	END

	ELSE
	BEGIN

		UPDATE [dbo].[ITReturnDocuments]
		   SET [FileName] = ISNULL(@FileName,[FileName])
			  ,[FilePath] = ISNULL(@FilePath, [FilePath])
			  ,[ModifiedBy] = @USER_ID
			  ,[ModifiedDate] = GETUTCDATE()
		WHERE Id = @Id
		SET @Result = 1;
		SET @ReturnMessage = 'Record updated successfully.'
	END
END

IF @OPERATION = 'Delete'
BEGIN
	UPDATE [dbo].[ITReturnDocuments]
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


