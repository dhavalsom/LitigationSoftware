USE [LitigationApp]
GO

/****** Object:  StoredProcedure [dbo].[SP_MAT_CREDIT_DETAILS_MANAGER]    Script Date: 12/30/2018 11:17:03 AM ******/
DROP PROCEDURE [dbo].[SP_MAT_CREDIT_DETAILS_MANAGER]
GO

/****** Object:  StoredProcedure [dbo].[SP_MAT_CREDIT_DETAILS_MANAGER]    Script Date: 12/30/2018 11:17:03 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[SP_MAT_CREDIT_DETAILS_MANAGER]
(
	@MAT_CREDIT_DETAILS_XML AS XML,
	@OPERATION AS NVARCHAR(100) = NULL, --COMPLIANCE_DOCUMENT FOR START/UPDATE MAT_CREDIT_DETAILS RECORD
	@USER_ID BIGINT = 1 --SET THE DEFAULT VALUE TO 1 IF NOT PASSED,
)
AS

BEGIN


/*BLOCK TO READ THE VARIABLES*/

DECLARE @Id AS BIGINT, @CompanyId AS BIGINT, @FYAYId AS BIGINT, @ITSectionCategoryId AS BIGINT

DECLARE @BusinessLosses_BF AS DECIMAL(18, 2), @UnabsorbedDepreciation_BF AS DECIMAL(18, 2)

DECLARE @BusinessLosses_CY AS DECIMAL(18, 2), @UnabsorbedDepreciation_CY AS DECIMAL(18, 2)

DECLARE @BusinessLosses_UL AS DECIMAL(18, 2), @UnabsorbedDepreciation_UL AS DECIMAL(18, 2)

DECLARE @ReturnMessage as NVARCHAR(MAX)
DECLARE @Active AS BIT, @Result as BIT

SELECT	 @Id = MATCreditDetailsList.Columns.value('Id[1]', 'BIGINT')
	   , @CompanyId = MATCreditDetailsList.Columns.value('CompanyId[1]', 'BIGINT')
	   , @FYAYId = MATCreditDetailsList.Columns.value('FYAYId[1]', 'BIGINT')
	   , @ITSectionCategoryId = MATCreditDetailsList.Columns.value('ITSectionCategoryId[1]', 'BIGINT')

	   , @BusinessLosses_BF = MATCreditDetailsList.Columns.value('BusinessLosses_BF[1]', 'DECIMAL(18, 2)')
	   , @UnabsorbedDepreciation_BF = MATCreditDetailsList.Columns.value('UnabsorbedDepreciation_BF[1]', 'DECIMAL(18, 2)')

	   , @BusinessLosses_CY = MATCreditDetailsList.Columns.value('BusinessLosses_CY[1]', 'DECIMAL(18, 2)')
	   , @UnabsorbedDepreciation_CY = MATCreditDetailsList.Columns.value('UnabsorbedDepreciation_CY[1]', 'DECIMAL(18, 2)')

	   , @BusinessLosses_UL = MATCreditDetailsList.Columns.value('BusinessLosses_UL[1]', 'DECIMAL(18, 2)')
	   , @UnabsorbedDepreciation_UL = MATCreditDetailsList.Columns.value('UnabsorbedDepreciation_UL[1]', 'DECIMAL(18, 2)')

	   , @Active = MATCreditDetailsList.Columns.value('Active[1]', 'bit')
FROM   @MAT_CREDIT_DETAILS_XML.nodes('MATCreditDetails') AS MATCreditDetailsList(Columns)

/*BLOCK TO READ THE VARIABLES ENDS HERE*/
IF @OPERATION IS NULL OR @OPERATION = ''
BEGIN

	IF @Id IS NULL OR @Id = 0 
	BEGIN

	INSERT INTO [dbo].[MATCreditDetails]
           ([CompanyId]
           ,[FYAYId]
           ,[ITSectionCategoryId]
           ,[BusinessLosses_BF]
           ,[UnabsorbedDepreciation_BF]
           ,[BusinessLosses_CY]
           ,[UnabsorbedDepreciation_CY]
           ,[BusinessLosses_UL]
           ,[UnabsorbedDepreciation_UL]
           ,[Active]
           ,[AddedBy]
           ,[AddedDate])
     VALUES
           (@CompanyId
		   ,@FYAYId
		   ,@ITSectionCategoryId
		   ,@BusinessLosses_BF
		   ,@UnabsorbedDepreciation_BF
		   ,@BusinessLosses_CY
		   ,@UnabsorbedDepreciation_CY
		   ,@BusinessLosses_UL
		   ,@UnabsorbedDepreciation_UL
		   ,@Active
		   ,@USER_ID
		   ,GETUTCDATE())

	SET @Result = 1;
	SET @ReturnMessage = 'Record created successfully.'
	END

	ELSE
	BEGIN

		UPDATE [dbo].[MATCreditDetails]
		SET  [BusinessLosses_BF] = ISNULL(@BusinessLosses_BF,[BusinessLosses_BF])
			,[UnabsorbedDepreciation_BF] = ISNULL(@UnabsorbedDepreciation_BF,[UnabsorbedDepreciation_BF])
			,[BusinessLosses_CY] = ISNULL(@BusinessLosses_CY,[BusinessLosses_CY])
			,[UnabsorbedDepreciation_CY] = ISNULL(@UnabsorbedDepreciation_CY,[UnabsorbedDepreciation_CY])
			,[BusinessLosses_UL] = ISNULL(@BusinessLosses_UL,[BusinessLosses_UL])
			,[UnabsorbedDepreciation_UL] = ISNULL(@UnabsorbedDepreciation_UL,[UnabsorbedDepreciation_UL])
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


