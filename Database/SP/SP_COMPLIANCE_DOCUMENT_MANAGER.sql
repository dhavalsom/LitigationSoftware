USE [LitigationApp]
GO

/****** Object:  StoredProcedure [dbo].[SP_COMPLIANCE_DOCUMENT_MANAGER]    Script Date: 7/6/2018 11:46:18 AM ******/
DROP PROCEDURE [dbo].[SP_COMPLIANCE_DOCUMENT_MANAGER]
GO

/****** Object:  StoredProcedure [dbo].[SP_COMPLIANCE_DOCUMENT_MANAGER]    Script Date: 7/6/2018 11:46:18 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[SP_COMPLIANCE_DOCUMENT_MANAGER]
(
	@COMPLIANCE_DOCUMENT_XML AS XML,
	@OPERATION AS NVARCHAR(100) = NULL, --COMPLIANCE_DOCUMENT FOR START/UPDATE COMPLIANCE_DOCUMENT RECORD
	@USER_ID BIGINT = 1 --SET THE DEFAULT VALUE TO 1 IF NOT PASSED,
)
AS

BEGIN


/*BLOCK TO READ THE VARIABLES*/

DECLARE @Id AS BIGINT, @ComplianceId AS BIGINT, @CompanyId AS BIGINT, @FYAYId AS BIGINT
DECLARE @FileName AS NVARCHAR(MAX),@FilePath AS NVARCHAR(MAX), @ReturnMessage as NVARCHAR(MAX)
DECLARE @Active AS BIT, @Result as BIT

SELECT	 @Id = ComplianceDocumentsList.Columns.value('Id[1]', 'BIGINT')
	   , @ComplianceId = ComplianceDocumentsList.Columns.value('ComplianceId[1]', 'BIGINT')
	   , @CompanyId = ComplianceDocumentsList.Columns.value('CompanyId[1]', 'BIGINT')
	   , @FYAYId = ComplianceDocumentsList.Columns.value('FYAYId[1]', 'BIGINT')
	   , @FileName = ComplianceDocumentsList.Columns.value('FileName[1]', 'nvarchar(max)')
	   , @FilePath = ComplianceDocumentsList.Columns.value('FilePath[1]', 'nvarchar(max)')
	   , @Active = ComplianceDocumentsList.Columns.value('Active[1]', 'bit')
FROM   @COMPLIANCE_DOCUMENT_XML.nodes('ComplianceDocuments') AS ComplianceDocumentsList(Columns)

/*BLOCK TO READ THE VARIABLES ENDS HERE*/
IF @OPERATION IS NULL OR @OPERATION = ''
BEGIN

	IF @Id IS NULL OR @Id =0 
	BEGIN

	INSERT INTO [dbo].[ComplianceDocuments]
           ([ComplianceId]
		   ,[CompanyId]
		   ,[FYAYId]
           ,[FileName]
           ,[FilePath]
           ,[Active]
           ,[AddedBy]
           ,[AddedDate])
     VALUES
           (@ComplianceId
		   ,@CompanyId
		   ,@FYAYId
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

		UPDATE [dbo].[ComplianceDocuments]
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
	UPDATE [dbo].[ComplianceDocuments]
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


