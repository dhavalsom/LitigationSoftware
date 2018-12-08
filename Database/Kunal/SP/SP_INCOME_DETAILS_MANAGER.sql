USE [LitigationApp]
GO

/****** Object:  StoredProcedure [dbo].[SP_INCOME_DETAILS_MANAGER]    Script Date: 12/8/2018 4:20:04 PM ******/
DROP PROCEDURE [dbo].[SP_INCOME_DETAILS_MANAGER]
GO

/****** Object:  StoredProcedure [dbo].[SP_INCOME_DETAILS_MANAGER]    Script Date: 12/8/2018 4:20:04 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_INCOME_DETAILS_MANAGER]
(
	@SP_INCOME_DETAILS_XML AS XML,
	@OPERATION AS NVARCHAR(100) = NULL, --NULL FOR INSERT/UPDATE INCOME DETAILS RECORD
	@USER_ID BIGINT = 1 --SET THE DEFAULT VALUE TO 1 IF NOT PASSED,
)
AS

BEGIN


/*BLOCK TO READ THE VARIABLES*/

DECLARE @Id AS BIGINT, @ITReturnDetailsId AS BIGINT, @ITHeadId AS BIGINT
DECLARE @ReturnMessage as NVARCHAR(MAX), @SPIncomeDescription as NVARCHAR(MAX)
DECLARE @SPIncomeValue as DECIMAL(18,2), @TaxRate as DECIMAL(18,2)
DECLARE @Active AS BIT, @Result AS BIT
DECLARE @SPIncomeDate AS DATETIME

SELECT	 @Id = SPIncomeDetailsList.Columns.value('Id[1]', 'BIGINT')
	   , @ITReturnDetailsId = SPIncomeDetailsList.Columns.value('ITReturnDetailsId[1]', 'BIGINT')
	   , @ITHeadId = SPIncomeDetailsList.Columns.value('ITHeadId[1]', 'BIGINT')
	   , @SPIncomeDescription = SPIncomeDetailsList.Columns.value('SPIncomeDescription[1]', 'NVARCHAR(MAX)')
	   , @SPIncomeValue = SPIncomeDetailsList.Columns.value('SPIncomeValue[1]', 'DECIMAL(18,2)')
	   , @TaxRate = SPIncomeDetailsList.Columns.value('TaxRate[1]', 'DECIMAL(18,2)')
	   , @SPIncomeDate = SPIncomeDetailsList.Columns.value('SPIncomeDate[1]', 'DECIMAL(18,2)')
	   , @Active = SPIncomeDetailsList.Columns.value('Active[1]', 'bit')
FROM   @SP_INCOME_DETAILS_XML.nodes('SPIncomeDetails') AS SPIncomeDetailsList(Columns)

/*BLOCK TO READ THE VARIABLES ENDS HERE*/
IF @OPERATION IS NULL
BEGIN

	IF @Id IS NULL OR @Id = 0 
	BEGIN

	INSERT INTO [dbo].[SPIncomeDetails]
           ([ITReturnDetailsId]
           ,[ITHeadId]
           ,[SPIncomeDescription]
           ,[SPIncomeValue]
           ,[TaxRate]
           ,[SPIncomeDate]
           ,[Active]
           ,[AddedBy]
           ,[AddedDate])
     VALUES
           (@ITReturnDetailsId
		   ,@ITHeadId
		   ,@SPIncomeDescription
		   ,@SPIncomeValue
		   ,@TaxRate
		   ,@SPIncomeDate
		   ,@Active
		   ,@USER_ID
		   ,GETUTCDATE())

	SET @Id = SCOPE_IDENTITY()
	SET @Result = 1;
	SET @ReturnMessage = 'Record created successfully.'
	END

	ELSE
	BEGIN

		UPDATE [dbo].[SPIncomeDetails]
		   SET [SPIncomeDescription] = ISNULL(@SPIncomeDescription,[SPIncomeDescription])
			  ,[SPIncomeValue] = ISNULL(@SPIncomeValue,[SPIncomeValue])
			  ,[TaxRate] = ISNULL(@TaxRate,[TaxRate])
			  ,[SPIncomeDate] = ISNULL(@SPIncomeDate,[SPIncomeDate])
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


