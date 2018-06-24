USE [LitigationApp]
GO

/****** Object:  StoredProcedure [dbo].[SP_IT_SECTION_MANAGER]    Script Date: 6/24/2018 8:44:56 PM ******/
DROP PROCEDURE [dbo].[SP_IT_SECTION_MANAGER]
GO

/****** Object:  StoredProcedure [dbo].[SP_IT_SECTION_MANAGER]    Script Date: 6/24/2018 8:44:56 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_IT_SECTION_MANAGER]
(
	@IT_SECTION_XML AS XML,
	@OPERATION AS NVARCHAR(100) = NULL, --NULL FOR INSERT/UPDATE ALLERGY RECORD
	@USER_ID BIGINT = 1 --SET THE DEFAULT VALUE TO 1 IF NOT PASSED,
)
AS

BEGIN


/*BLOCK TO READ THE VARIABLES*/

DECLARE @Id AS BIGINT
DECLARE @ReturnMessage as NVARCHAR(MAX), @Description as NVARCHAR(MAX)
DECLARE @Active AS BIT, @Result as BIT, @IsDefault AS BIT

SELECT	 @Id = ITSectionList.Columns.value('Id[1]', 'BIGINT')
	   , @Description = ITSectionList.Columns.value('Description[1]', 'NVARCHAR(MAX)')
	   , @IsDefault = ITSectionList.Columns.value('IsDefault[1]', 'bit')
	   , @Active = ITSectionList.Columns.value('Active[1]', 'bit')
FROM   @IT_SECTION_XML.nodes('ITSection') AS ITSectionList(Columns)

/*BLOCK TO READ THE VARIABLES ENDS HERE*/
IF @OPERATION IS NULL
BEGIN

	IF @Id IS NULL OR @Id = 0 
	BEGIN

	INSERT INTO [dbo].[ITSectionMaster]
           ([Description]
           ,[IsDefault]
           ,[Active]
           ,[AddedBy]
           ,[AddedDate])
     VALUES
           (@Description
           ,@IsDefault
           ,@Active
		   ,@USER_ID
		   ,GETUTCDATE())

	SET @Result = 1;
	SET @ReturnMessage = 'Record created successfully.'
	END

	ELSE
	BEGIN

		UPDATE [dbo].[ITSectionMaster]
		   SET [Description] = ISNULL(@Description,[Description])
			  ,[IsDefault] = ISNULL(@IsDefault,[IsDefault])
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


