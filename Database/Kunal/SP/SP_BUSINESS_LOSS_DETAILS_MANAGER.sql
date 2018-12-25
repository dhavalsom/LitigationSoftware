USE [LitigationApp]
GO

/****** Object:  StoredProcedure [dbo].[SP_BUSINESS_LOSS_DETAILS_MANAGER]    Script Date: 12/25/2018 9:29:14 AM ******/
DROP PROCEDURE [dbo].[SP_BUSINESS_LOSS_DETAILS_MANAGER]
GO

/****** Object:  StoredProcedure [dbo].[SP_BUSINESS_LOSS_DETAILS_MANAGER]    Script Date: 12/25/2018 9:29:14 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_BUSINESS_LOSS_DETAILS_MANAGER]
(
	@BUSINESS_LOSS_DETAILS_XML AS XML,
	@OPERATION AS NVARCHAR(100) = NULL, --COMPLIANCE_DOCUMENT FOR START/UPDATE BUSINESS_LOSS_DETAILS RECORD
	@USER_ID BIGINT = 1 --SET THE DEFAULT VALUE TO 1 IF NOT PASSED,
)
AS

BEGIN


/*BLOCK TO READ THE VARIABLES*/

DECLARE @Id AS BIGINT, @CompanyId AS BIGINT, @FYAYId AS BIGINT, @ITSectionCategoryId AS BIGINT

DECLARE @IncomeCapGainsLTCG_BF AS DECIMAL(18, 2), @IncomeCapGainsSTCG_BF AS DECIMAL(18, 2), @IncomeBusinessProf_BF AS DECIMAL(18, 2)
, @IncomeSpeculativeBusiness_BF AS DECIMAL(18, 2), @UnabsorbedDepreciation_BF AS DECIMAL(18, 2)
, @HousePropIncome_BF AS DECIMAL(18, 2), @IncomeOtherSources_BF AS DECIMAL(18, 2)

DECLARE @IncomeCapGainsLTCG_CY AS DECIMAL(18, 2), @IncomeCapGainsSTCG_CY AS DECIMAL(18, 2), @IncomeBusinessProf_CY AS DECIMAL(18, 2)
, @IncomeSpeculativeBusiness_CY AS DECIMAL(18, 2), @UnabsorbedDepreciation_CY AS DECIMAL(18, 2)
, @HousePropIncome_CY AS DECIMAL(18, 2), @IncomeOtherSources_CY AS DECIMAL(18, 2)

DECLARE @IncomeCapGainsLTCG_UL AS DECIMAL(18, 2), @IncomeCapGainsSTCG_UL AS DECIMAL(18, 2), @IncomeBusinessProf_UL AS DECIMAL(18, 2)
, @IncomeSpeculativeBusiness_UL AS DECIMAL(18, 2), @UnabsorbedDepreciation_UL AS DECIMAL(18, 2)
, @HousePropIncome_UL AS DECIMAL(18, 2), @IncomeOtherSources_UL AS DECIMAL(18, 2)

DECLARE @IncomeCapGainsLTCG_UALL AS DECIMAL(18, 2), @IncomeCapGainsSTCG_UALL AS DECIMAL(18, 2), @IncomeBusinessProf_UALL AS DECIMAL(18, 2)
, @IncomeSpeculativeBusiness_UALL AS DECIMAL(18, 2), @UnabsorbedDepreciation_UALL AS DECIMAL(18, 2)
, @HousePropIncome_UALL AS DECIMAL(18, 2), @IncomeOtherSources_UALL AS DECIMAL(18, 2)

DECLARE @ReturnMessage as NVARCHAR(MAX)
DECLARE @Active AS BIT, @Result as BIT

SELECT	 @Id = BusinessLossDetailsList.Columns.value('Id[1]', 'BIGINT')
	   , @CompanyId = BusinessLossDetailsList.Columns.value('CompanyId[1]', 'BIGINT')
	   , @FYAYId = BusinessLossDetailsList.Columns.value('FYAYId[1]', 'BIGINT')
	   , @ITSectionCategoryId = BusinessLossDetailsList.Columns.value('ITSectionCategoryId[1]', 'BIGINT')

	   , @IncomeCapGainsLTCG_BF = BusinessLossDetailsList.Columns.value('IncomeCapGainsLTCG_BF[1]', 'DECIMAL(18, 2)')
	   , @IncomeCapGainsSTCG_BF = BusinessLossDetailsList.Columns.value('IncomeCapGainsSTCG_BF[1]', 'DECIMAL(18, 2)')
	   , @IncomeBusinessProf_BF = BusinessLossDetailsList.Columns.value('IncomeBusinessProf_BF[1]', 'DECIMAL(18, 2)')
	   , @IncomeSpeculativeBusiness_BF = BusinessLossDetailsList.Columns.value('IncomeSpeculativeBusiness_BF[1]', 'DECIMAL(18, 2)')
	   , @UnabsorbedDepreciation_BF = BusinessLossDetailsList.Columns.value('UnabsorbedDepreciation_BF[1]', 'DECIMAL(18, 2)')
	   , @HousePropIncome_BF = BusinessLossDetailsList.Columns.value('HousePropIncome_BF[1]', 'DECIMAL(18, 2)')
	   , @IncomeOtherSources_BF = BusinessLossDetailsList.Columns.value('IncomeOtherSources_BF[1]', 'DECIMAL(18, 2)')

	   , @IncomeCapGainsLTCG_CY = BusinessLossDetailsList.Columns.value('IncomeCapGainsLTCG_CY[1]', 'DECIMAL(18, 2)')
	   , @IncomeCapGainsSTCG_CY = BusinessLossDetailsList.Columns.value('IncomeCapGainsSTCG_CY[1]', 'DECIMAL(18, 2)')
	   , @IncomeBusinessProf_CY = BusinessLossDetailsList.Columns.value('IncomeBusinessProf_CY[1]', 'DECIMAL(18, 2)')
	   , @IncomeSpeculativeBusiness_CY = BusinessLossDetailsList.Columns.value('IncomeSpeculativeBusiness_CY[1]', 'DECIMAL(18, 2)')
	   , @UnabsorbedDepreciation_CY = BusinessLossDetailsList.Columns.value('UnabsorbedDepreciation_CY[1]', 'DECIMAL(18, 2)')
	   , @HousePropIncome_CY = BusinessLossDetailsList.Columns.value('HousePropIncome_CY[1]', 'DECIMAL(18, 2)')
	   , @IncomeOtherSources_CY = BusinessLossDetailsList.Columns.value('IncomeOtherSources_CY[1]', 'DECIMAL(18, 2)')

	   , @IncomeCapGainsLTCG_UL = BusinessLossDetailsList.Columns.value('IncomeCapGainsLTCG_UL[1]', 'DECIMAL(18, 2)')
	   , @IncomeCapGainsSTCG_UL = BusinessLossDetailsList.Columns.value('IncomeCapGainsSTCG_UL[1]', 'DECIMAL(18, 2)')
	   , @IncomeBusinessProf_UL = BusinessLossDetailsList.Columns.value('IncomeBusinessProf_UL[1]', 'DECIMAL(18, 2)')
	   , @IncomeSpeculativeBusiness_UL = BusinessLossDetailsList.Columns.value('IncomeSpeculativeBusiness_UL[1]', 'DECIMAL(18, 2)')
	   , @UnabsorbedDepreciation_UL = BusinessLossDetailsList.Columns.value('UnabsorbedDepreciation_UL[1]', 'DECIMAL(18, 2)')
	   , @HousePropIncome_UL = BusinessLossDetailsList.Columns.value('HousePropIncome_UL[1]', 'DECIMAL(18, 2)')
	   , @IncomeOtherSources_UL = BusinessLossDetailsList.Columns.value('IncomeOtherSources_UL[1]', 'DECIMAL(18, 2)')

	   , @IncomeCapGainsLTCG_UALL = BusinessLossDetailsList.Columns.value('IncomeCapGainsLTCG_UALL[1]', 'DECIMAL(18, 2)')
	   , @IncomeCapGainsSTCG_UALL = BusinessLossDetailsList.Columns.value('IncomeCapGainsSTCG_UALL[1]', 'DECIMAL(18, 2)')
	   , @IncomeBusinessProf_UALL = BusinessLossDetailsList.Columns.value('IncomeBusinessProf_UALL[1]', 'DECIMAL(18, 2)')
	   , @IncomeSpeculativeBusiness_UALL = BusinessLossDetailsList.Columns.value('IncomeSpeculativeBusiness_UALL[1]', 'DECIMAL(18, 2)')
	   , @UnabsorbedDepreciation_UALL = BusinessLossDetailsList.Columns.value('UnabsorbedDepreciation_UALL[1]', 'DECIMAL(18, 2)')
	   , @HousePropIncome_UALL = BusinessLossDetailsList.Columns.value('HousePropIncome_UALL[1]', 'DECIMAL(18, 2)')
	   , @IncomeOtherSources_UALL = BusinessLossDetailsList.Columns.value('IncomeOtherSources_UALL[1]', 'DECIMAL(18, 2)')

	   , @Active = BusinessLossDetailsList.Columns.value('Active[1]', 'bit')
FROM   @BUSINESS_LOSS_DETAILS_XML.nodes('BusinessLossDetails') AS BusinessLossDetailsList(Columns)

/*BLOCK TO READ THE VARIABLES ENDS HERE*/
IF @OPERATION IS NULL OR @OPERATION = ''
BEGIN

	IF @Id IS NULL OR @Id = 0 
	BEGIN

	INSERT INTO [dbo].[BusinessLossDetails]
           ([CompanyId]
           ,[FYAYId]
           ,[ITSectionCategoryId]
           ,[IncomeCapGainsLTCG_BF]
           ,[IncomeCapGainsSTCG_BF]
           ,[IncomeBusinessProf_BF]
           ,[IncomeSpeculativeBusiness_BF]
           ,[UnabsorbedDepreciation_BF]
           ,[HousePropIncome_BF]
           ,[IncomeOtherSources_BF]
           ,[IncomeCapGainsLTCG_CY]
           ,[IncomeCapGainsSTCG_CY]
           ,[IncomeBusinessProf_CY]
           ,[IncomeSpeculativeBusiness_CY]
           ,[UnabsorbedDepreciation_CY]
           ,[HousePropIncome_CY]
           ,[IncomeOtherSources_CY]
           ,[IncomeCapGainsLTCG_UL]
           ,[IncomeCapGainsSTCG_UL]
           ,[IncomeBusinessProf_UL]
           ,[IncomeSpeculativeBusiness_UL]
           ,[UnabsorbedDepreciation_UL]
           ,[HousePropIncome_UL]
           ,[IncomeOtherSources_UL]
           ,[IncomeCapGainsLTCG_UALL]
           ,[IncomeCapGainsSTCG_UALL]
           ,[IncomeBusinessProf_UALL]
           ,[IncomeSpeculativeBusiness_UALL]
           ,[UnabsorbedDepreciation_UALL]
           ,[HousePropIncome_UALL]
           ,[IncomeOtherSources_UALL]
           ,[Active]
           ,[AddedBy]
           ,[AddedDate])
     VALUES
           (@CompanyId
		   ,@FYAYId
		   ,@ITSectionCategoryId

		   ,@IncomeCapGainsLTCG_BF
		   ,@IncomeCapGainsSTCG_BF
		   ,@IncomeBusinessProf_BF
		   ,@IncomeSpeculativeBusiness_BF
		   ,@UnabsorbedDepreciation_BF
		   ,@HousePropIncome_BF
		   ,@IncomeOtherSources_BF
           
		   ,@IncomeCapGainsLTCG_CY
		   ,@IncomeCapGainsSTCG_CY
		   ,@IncomeBusinessProf_CY
		   ,@IncomeSpeculativeBusiness_CY
		   ,@UnabsorbedDepreciation_CY
		   ,@HousePropIncome_CY
		   ,@IncomeOtherSources_CY

		   ,@IncomeCapGainsLTCG_UL
		   ,@IncomeCapGainsSTCG_UL
		   ,@IncomeBusinessProf_UL
		   ,@IncomeSpeculativeBusiness_UL
		   ,@UnabsorbedDepreciation_UL
		   ,@HousePropIncome_UL
		   ,@IncomeOtherSources_UL

		   ,@IncomeCapGainsLTCG_UALL
		   ,@IncomeCapGainsSTCG_UALL
		   ,@IncomeBusinessProf_UALL
		   ,@IncomeSpeculativeBusiness_UALL
		   ,@UnabsorbedDepreciation_UALL
		   ,@HousePropIncome_UALL
		   ,@IncomeOtherSources_UALL
		   ,@Active
		   ,@USER_ID
		   ,GETUTCDATE())

	SET @Result = 1;
	SET @ReturnMessage = 'Record created successfully.'
	END

	ELSE
	BEGIN

		UPDATE [dbo].[BusinessLossDetails]
		SET  [IncomeCapGainsLTCG_BF] = ISNULL(@IncomeCapGainsLTCG_BF,[IncomeCapGainsLTCG_BF])
			,[IncomeCapGainsSTCG_BF] = ISNULL(@IncomeCapGainsSTCG_BF,[IncomeCapGainsSTCG_BF])
			,[IncomeBusinessProf_BF] = ISNULL(@IncomeBusinessProf_BF,[IncomeBusinessProf_BF])
			,[IncomeSpeculativeBusiness_BF] = ISNULL(@IncomeSpeculativeBusiness_BF,[IncomeSpeculativeBusiness_BF])
			,[UnabsorbedDepreciation_BF] = ISNULL(@UnabsorbedDepreciation_BF,[UnabsorbedDepreciation_BF])
			,[HousePropIncome_BF] = ISNULL(@HousePropIncome_BF,[HousePropIncome_BF])
			,[IncomeOtherSources_BF] = ISNULL(@IncomeOtherSources_BF,[IncomeOtherSources_BF])

			,[IncomeCapGainsLTCG_CY] = ISNULL(@IncomeCapGainsLTCG_CY,[IncomeCapGainsLTCG_CY])
			,[IncomeCapGainsSTCG_CY] = ISNULL(@IncomeCapGainsSTCG_CY,[IncomeCapGainsSTCG_CY])
			,[IncomeBusinessProf_CY] = ISNULL(@IncomeBusinessProf_CY,[IncomeBusinessProf_CY])
			,[IncomeSpeculativeBusiness_CY] = ISNULL(@IncomeSpeculativeBusiness_CY,[IncomeSpeculativeBusiness_CY])
			,[UnabsorbedDepreciation_CY] = ISNULL(@UnabsorbedDepreciation_CY,[UnabsorbedDepreciation_CY])
			,[HousePropIncome_CY] = ISNULL(@HousePropIncome_CY,[HousePropIncome_CY])
			,[IncomeOtherSources_CY] = ISNULL(@IncomeOtherSources_CY,[IncomeOtherSources_CY])

			,[IncomeCapGainsLTCG_UL] = ISNULL(@IncomeCapGainsLTCG_UL,[IncomeCapGainsLTCG_UL])
			,[IncomeCapGainsSTCG_UL] = ISNULL(@IncomeCapGainsSTCG_UL,[IncomeCapGainsSTCG_UL])
			,[IncomeBusinessProf_UL] = ISNULL(@IncomeBusinessProf_UL,[IncomeBusinessProf_UL])
			,[IncomeSpeculativeBusiness_UL] = ISNULL(@IncomeSpeculativeBusiness_UL,[IncomeSpeculativeBusiness_UL])
			,[UnabsorbedDepreciation_UL] = ISNULL(@UnabsorbedDepreciation_UL,[UnabsorbedDepreciation_UL])
			,[HousePropIncome_UL] = ISNULL(@HousePropIncome_UL,[HousePropIncome_UL])
			,[IncomeOtherSources_UL] = ISNULL(@IncomeOtherSources_UL,[IncomeOtherSources_UL])

			,[IncomeCapGainsLTCG_UALL] = ISNULL(@IncomeCapGainsLTCG_UALL,[IncomeCapGainsLTCG_UALL])
			,[IncomeCapGainsSTCG_UALL] = ISNULL(@IncomeCapGainsSTCG_UALL,[IncomeCapGainsSTCG_UALL])
			,[IncomeBusinessProf_UALL] = ISNULL(@IncomeBusinessProf_UALL,[IncomeBusinessProf_UALL])
			,[IncomeSpeculativeBusiness_UALL] = ISNULL(@IncomeSpeculativeBusiness_UALL,[IncomeSpeculativeBusiness_UALL])
			,[UnabsorbedDepreciation_UALL] = ISNULL(@UnabsorbedDepreciation_UALL,[UnabsorbedDepreciation_UALL])
			,[HousePropIncome_UALL] = ISNULL(@HousePropIncome_UALL,[HousePropIncome_UALL])
			,[IncomeOtherSources_UALL] = ISNULL(@IncomeOtherSources_UALL,[IncomeOtherSources_UALL])


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


