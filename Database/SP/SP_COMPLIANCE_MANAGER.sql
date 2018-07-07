USE [LitigationApp]
GO

/****** Object:  StoredProcedure [dbo].[SP_COMPLIANCE_MANAGER]    Script Date: 7/7/2018 4:28:29 PM ******/
DROP PROCEDURE [dbo].[SP_COMPLIANCE_MANAGER]
GO

/****** Object:  StoredProcedure [dbo].[SP_COMPLIANCE_MANAGER]    Script Date: 7/7/2018 4:28:29 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_COMPLIANCE_MANAGER]
(
	@COMPLIANCE_XML AS XML,
	@OPERATION AS NVARCHAR(100) = NULL, --NULL FOR INSERT/UPDATE ALLERGY RECORD
	@USER_ID BIGINT = 1 --SET THE DEFAULT VALUE TO 1 IF NOT PASSED,
)
AS

BEGIN


/*BLOCK TO READ THE VARIABLES*/

DECLARE @Id AS BIGINT, @SrNo AS BIGINT
DECLARE @ReturnMessage as NVARCHAR(MAX), @Description as NVARCHAR(MAX)
DECLARE @Active AS BIT, @Result as BIT

SELECT	 @Id = ComplianceMasterList.Columns.value('Id[1]', 'BIGINT')
	   , @Description = ComplianceMasterList.Columns.value('Description[1]', 'NVARCHAR(MAX)')
	   , @SrNo = ComplianceMasterList.Columns.value('SrNo[1]', 'BIGINT')
	   , @Active = ComplianceMasterList.Columns.value('Active[1]', 'bit')
FROM   @COMPLIANCE_XML.nodes('ComplianceMaster') AS ComplianceMasterList(Columns)

IF @SrNo = 0
BEGIN
	SELECT @SrNo = MAX(SrNo) + 1 FROM [dbo].[ComplianceMaster]
END
/*BLOCK TO READ THE VARIABLES ENDS HERE*/
IF @OPERATION IS NULL
BEGIN

	IF @Id IS NULL OR @Id = 0 
	BEGIN
	
	INSERT INTO [dbo].[ComplianceMaster]
           ([SrNo]
           ,[Description]
           ,[Active]
           ,[AddedBy]
           ,[AddedDate])     
     VALUES
           (@SrNo
		   ,@Description
           ,@Active
		   ,@USER_ID
		   ,GETUTCDATE())

	SET @Result = 1;
	SET @ReturnMessage = 'Record created successfully.'
	END

	ELSE
	BEGIN

		UPDATE [dbo].[ComplianceMaster]
		   SET [Description] = ISNULL(@Description,[Description])
			  ,[SrNo] = ISNULL(@SrNo,[SrNo])
			  ,[ModifiedBy] = @USER_ID
			  ,[ModifiedDate] = GETUTCDATE()
		WHERE Id = @Id
		SET @Result = 1;
		SET @ReturnMessage = 'Record updated successfully.'
	END
END

SELECT @Result AS Result, @ReturnMessage AS ReturnMessage
END














GO


