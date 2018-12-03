USE [LitigationApp]
GO

/****** Object:  StoredProcedure [dbo].[SP_IT_SUB_HEAD_MANAGER]    Script Date: 12/2/2018 12:54:02 AM ******/
DROP PROCEDURE [dbo].[SP_IT_SUB_HEAD_MANAGER]
GO

/****** Object:  StoredProcedure [dbo].[SP_IT_SUB_HEAD_MANAGER]    Script Date: 12/2/2018 12:54:02 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SP_IT_SUB_HEAD_MANAGER]
(
	@IT_SUB_HEAD_XML AS XML,
	@OPERATION AS NVARCHAR(100) = NULL, --NULL FOR INSERT/UPDATE ALLERGY RECORD
	@USER_ID BIGINT = 1 --SET THE DEFAULT VALUE TO 1 IF NOT PASSED,
)
AS

BEGIN


/*BLOCK TO READ THE VARIABLES*/

DECLARE @Id AS BIGINT, @ITHeadId AS BIGINT
DECLARE @ReturnMessage as NVARCHAR(MAX), @Description as NVARCHAR(MAX)
DECLARE @Active AS BIT, @Result as BIT, @IsAllowance AS BIT, @HasDate AS BIT

SELECT	 @Id = ITSubHeadMasterList.Columns.value('Id[1]', 'BIGINT')
	   , @ITHeadId = ITSubHeadMasterList.Columns.value('ITHeadId[1]', 'BIGINT')
	   , @Description = ITSubHeadMasterList.Columns.value('Description[1]', 'NVARCHAR(MAX)')
	   , @IsAllowance = ITSubHeadMasterList.Columns.value('IsAllowance[1]', 'bit')
	   , @HasDate = ITSubHeadMasterList.Columns.value('HasDate[1]', 'bit')
	   , @Active = ITSubHeadMasterList.Columns.value('Active[1]', 'bit')
FROM   @IT_SUB_HEAD_XML.nodes('ITSubHeadMaster') AS ITSubHeadMasterList(Columns)

/*BLOCK TO READ THE VARIABLES ENDS HERE*/
IF @OPERATION IS NULL
BEGIN

	IF @Id IS NULL OR @Id = 0 
	BEGIN

	INSERT INTO [dbo].[ITSubHeadMaster]
           ([ITHeadId]
           ,[Description]
           ,[IsAllowance]
		   ,[HasDate]
           ,[Active]
           ,[AddedBy]
           ,[AddedDate])
     VALUES
           (@ITHeadId
           ,@Description
		   ,@IsAllowance
		   ,@HasDate
           ,@Active
		   ,@USER_ID
		   ,GETUTCDATE())
	SET @Id = SCOPE_IDENTITY()
	SET @Result = 1;
	SET @ReturnMessage = 'Record created successfully.'
	END

	ELSE
	BEGIN

		UPDATE [dbo].[ITSubHeadMaster]
		   SET [Description] = ISNULL(@Description,[Description])
			  ,[IsAllowance] = ISNULL(@IsAllowance,[IsAllowance])
			  ,[HasDate] = ISNULL(@HasDate,[HasDate])
			  ,[ModifiedBy] = @USER_ID
			  ,[ModifiedDate] = GETUTCDATE()
		WHERE Id = @Id
		SET @Result = 1;
		SET @ReturnMessage = 'Record updated successfully.'
	END
END

SELECT @Id AS Id, @Result AS Result, @ReturnMessage AS ReturnMessage
END















GO


