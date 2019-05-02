USE [LitigationApp]
GO

/****** Object:  StoredProcedure [dbo].[SP_COMPETITOR_MANAGER]    Script Date: 5/3/2019 12:03:49 AM ******/
DROP PROCEDURE [dbo].[SP_COMPETITOR_MANAGER]
GO

/****** Object:  StoredProcedure [dbo].[SP_COMPETITOR_MANAGER]    Script Date: 5/3/2019 12:03:49 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_COMPETITOR_MANAGER]
(
	@COMPETITOR_XML AS XML,
	@OPERATION AS NVARCHAR(100) = NULL, --NULL FOR INSERT/UPDATE COMPETITOR RECORD
	@USER_ID BIGINT = 1 --SET THE DEFAULT VALUE TO 1 IF NOT PASSED,
)
AS

BEGIN


/*BLOCK TO READ THE VARIABLES*/

DECLARE @Id AS BIGINT, @CompanyId AS BIGINT
DECLARE @ReturnMessage as NVARCHAR(MAX), @Description as NVARCHAR(MAX)
DECLARE @Active AS BIT, @Result as BIT

SELECT	 @Id = Competitor.Columns.value('Id[1]', 'BIGINT')
	   , @CompanyId = Competitor.Columns.value('CompanyId[1]', 'BIGINT')
	   , @Description = Competitor.Columns.value('Description[1]', 'NVARCHAR(MAX)')
	   , @Active = Competitor.Columns.value('Active[1]', 'bit')
FROM   @COMPETITOR_XML.nodes('Competitor') AS ITSectionList(Columns)

/*BLOCK TO READ THE VARIABLES ENDS HERE*/
IF @OPERATION IS NULL
BEGIN

	IF @Id IS NULL OR @Id = 0 
	BEGIN

	INSERT INTO [dbo].[CompetitorMaster]
           ([CompanyId]
           ,[Description]
           ,[Active]
           ,[AddedBy]
           ,[AddedDate])
     VALUES
           (@CompanyId
           ,@Description
           ,@Active
		   ,@USER_ID
		   ,GETUTCDATE())

	SET @Result = 1;
	SET @ReturnMessage = 'Record created successfully.'
	END

	ELSE
	BEGIN

		UPDATE [dbo].[CompetitorMaster]
		   SET [Description] = ISNULL(@Description,[Description])
			  ,[CompanyId] = ISNULL(@CompanyId,[CompanyId])
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


