USE [LitigationApp]
GO

/****** Object:  StoredProcedure [dbo].[SP_COMPETITOR_TAX_RATE_MANAGER]    Script Date: 5/3/2019 12:07:33 AM ******/
DROP PROCEDURE [dbo].[SP_COMPETITOR_TAX_RATE_MANAGER]
GO

/****** Object:  StoredProcedure [dbo].[SP_COMPETITOR_TAX_RATE_MANAGER]    Script Date: 5/3/2019 12:07:33 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_COMPETITOR_TAX_RATE_MANAGER]
(
	@COMPETITOR_TAX_RATE_XML AS XML,
	@OPERATION AS NVARCHAR(100) = NULL, --NULL FOR INSERT/UPDATE COMPETITOR RECORD
	@USER_ID BIGINT = 1 --SET THE DEFAULT VALUE TO 1 IF NOT PASSED,
)
AS

BEGIN


/*BLOCK TO READ THE VARIABLES*/

DECLARE @Id AS BIGINT, @CompetitorId AS BIGINT, @FYAYId AS BIGINT
DECLARE @ReturnMessage as NVARCHAR(MAX)
DECLARE @Active AS BIT, @Result as BIT
DECLARE @TaxRate AS DECIMAL(18,2)

SELECT	 @Id = CompetitorTaxRate.Columns.value('Id[1]', 'BIGINT')
	   , @CompetitorId = CompetitorTaxRate.Columns.value('CompetitorId[1]', 'BIGINT')
	   , @FYAYId = CompetitorTaxRate.Columns.value('FYAYId[1]', 'BIGINT')
	   , @TaxRate = CompetitorTaxRate.Columns.value('TaxRate[1]', 'DECIMAL(18,2)')
	   , @Active = CompetitorTaxRate.Columns.value('Active[1]', 'bit')
FROM   @COMPETITOR_TAX_RATE_XML.nodes('CompetitorTaxRate') AS ITSectionList(Columns)

/*BLOCK TO READ THE VARIABLES ENDS HERE*/
IF @OPERATION IS NULL
BEGIN

	IF @Id IS NULL OR @Id = 0 
	BEGIN

	INSERT INTO [dbo].[CompetitorTaxRate]
           ([CompetitorId]
           ,[FYAYId]
		   ,[TaxRate]
           ,[Active]
           ,[AddedBy]
           ,[AddedDate])
     VALUES
           (@CompetitorId
           ,@FYAYId
		   ,@TaxRate
           ,@Active
		   ,@USER_ID
		   ,GETUTCDATE())

	SET @Result = 1;
	SET @ReturnMessage = 'Record created successfully.'
	END

	ELSE
	BEGIN

		UPDATE [dbo].[CompetitorTaxRate]
		   SET [TaxRate] = ISNULL(@TaxRate,[TaxRate])
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


