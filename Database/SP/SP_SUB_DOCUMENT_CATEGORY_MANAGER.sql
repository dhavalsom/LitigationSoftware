USE [LitigationApp]
GO

/****** Object:  StoredProcedure [dbo].[SP_SUB_DOCUMENT_CATEGORY_MANAGER]    Script Date: 2/24/2019 10:18:17 AM ******/
DROP PROCEDURE [dbo].[SP_SUB_DOCUMENT_CATEGORY_MANAGER]
GO

/****** Object:  StoredProcedure [dbo].[SP_SUB_DOCUMENT_CATEGORY_MANAGER]    Script Date: 2/24/2019 10:18:17 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SP_SUB_DOCUMENT_CATEGORY_MANAGER]
(
	@SUB_DOCUMENT_CATEGORY_XML AS XML,
	@OPERATION AS NVARCHAR(100) = NULL, --NULL FOR INSERT/UPDATE SUB_DOCUMENT_CATEGORY RECORD
	@USER_ID BIGINT = 1 --SET THE DEFAULT VALUE TO 1 IF NOT PASSED,
)
AS

BEGIN


/*BLOCK TO READ THE VARIABLES*/

DECLARE @Id AS BIGINT, @DocumentCategoryId AS BIGINT
DECLARE @ReturnMessage as NVARCHAR(MAX), @Description as NVARCHAR(MAX)
DECLARE @Active AS BIT, @Result as BIT

SELECT	 @Id = SubDocumentCategoryList.Columns.value('Id[1]', 'BIGINT')
	   , @DocumentCategoryId = SubDocumentCategoryList.Columns.value('DocumentCategoryId[1]', 'BIGINT')
	   , @Description = SubDocumentCategoryList.Columns.value('Description[1]', 'NVARCHAR(MAX)')
	   , @Active = SubDocumentCategoryList.Columns.value('Active[1]', 'bit')
FROM   @SUB_DOCUMENT_CATEGORY_XML.nodes('SubDocumentCategoryMaster') AS SubDocumentCategoryList(Columns)

/*BLOCK TO READ THE VARIABLES ENDS HERE*/
IF @OPERATION IS NULL
BEGIN

	IF @Id IS NULL OR @Id = 0 
	BEGIN

	INSERT INTO [dbo].[SubDocumentCategoryMaster]
           ([DocumentCategoryId]
           ,[Description]
           ,[Active]
           ,[AddedBy]
           ,[AddedDate])
     VALUES
           (@DocumentCategoryId
           ,@Description
           ,@Active
		   ,@USER_ID
		   ,GETUTCDATE())
	SET @Id = SCOPE_IDENTITY()
	SET @Result = 1;
	SET @ReturnMessage = 'Record created successfully.'
	END

	ELSE
	BEGIN

		UPDATE [dbo].[SubDocumentCategoryMaster]
		   SET [Description] = ISNULL(@Description,[Description])
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


