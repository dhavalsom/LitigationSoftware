USE [LitigationApp]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_BUSINESS_LOSS_DETAILS_LIST]    Script Date: 1/14/2019 10:24:27 PM ******/
DROP PROCEDURE [dbo].[SP_GET_BUSINESS_LOSS_DETAILS_LIST]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_BUSINESS_LOSS_DETAILS_LIST]    Script Date: 1/14/2019 10:24:27 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[SP_GET_BUSINESS_LOSS_DETAILS_LIST]
(	
	@COMPANY_ID BIGINT = NULL,
	@FYAY_ID BIGINT = NULL,
	@IT_SECTION_CATEGORY_ID BIGINT = NULL,
	@BUSINESS_LOSS_DETAILS_ID BIGINT = NULL
)
AS

BEGIN

--EXEC [SP_GET_BUSINESS_LOSS_DETAILS_LIST] 1, 1, 1

DECLARE @PREV_YEAR_BL_DETAILS_ID AS BIGINT = 0, @PREV_YEAR_FY_AY_ID AS BIGINT = 0, @BL_DETAILS_ID AS BIGINT = 0 
DECLARE @IT_RETURN_DETAILS_ID AS BIGINT = 0

/*HANDLE THE CASE FOR THE LOGIC IN THE DISPLAY FOR INPUT SHEET*/
IF @COMPANY_ID IS NOT NULL AND @FYAY_ID IS NOT NULL AND @IT_SECTION_CATEGORY_ID IS NOT NULL
BEGIN

	SELECT TOP 1 @IT_RETURN_DETAILS_ID = ISNULL(ITR.Id, 0)
	FROM ITReturnDetails ITR
	WHERE ITR.CompanyID = @COMPANY_ID	
	AND  ITR.ITSectionID IN (SELECT Id FROM ITSectionMaster ITSM WHERE ITSM.SECTIONCATEGORYID = @IT_SECTION_CATEGORY_ID)
	AND ITR.FYAYID = @FYAY_ID
	ORDER BY ITR.ITReturnFillingDate DESC
	
	SELECT @PREV_YEAR_FY_AY_ID = ISNULL(FYAY.Id, 0)
	FROM FYAYMaster FYAY
	WHERE FYAY.SortOrder = (SELECT FYAY_INNER.SortOrder - 1 FROM FYAYMaster FYAY_INNER WHERE FYAY_INNER.Id = @FYAY_ID)

	IF @PREV_YEAR_FY_AY_ID > 0 
	BEGIN
		SELECT @PREV_YEAR_BL_DETAILS_ID = ISNULL(BL.Id, 0)
		FROM BusinessLossDetails BL
		WHERE BL.CompanyId = @COMPANY_ID
		AND BL.FYAYId = @PREV_YEAR_FY_AY_ID
		AND BL.ITSectionCategoryId = @IT_SECTION_CATEGORY_ID
	END

	SELECT @BL_DETAILS_ID = ISNULL(BL.Id, 0)
		FROM BusinessLossDetails BL
		WHERE BL.CompanyId = @COMPANY_ID
		AND BL.FYAYId = @FYAY_ID
		AND BL.ITSectionCategoryId = @IT_SECTION_CATEGORY_ID

SELECT
	   CASE BL.[Id] WHEN @BL_DETAILS_ID THEN CONVERT(BIT, 1) ELSE CONVERT(BIT, 0) END AS IsCurrentYear 
	  ,BL.[Id]
      ,BL.[CompanyId]
	  ,COMP.CompanyName
      ,BL.[FYAYId]
	  ,FYAY.AssessmentYear
	  ,FYAY.FinancialYear
      ,BL.[ITSectionCategoryId]
	  ,ITS.CategoryDesc
      ,BL.[IncomeCapGainsLTCG_BF]
      ,BL.[IncomeCapGainsSTCG_BF]
      ,BL.[IncomeBusinessProf_BF]
      ,BL.[IncomeSpeculativeBusiness_BF]
      ,BL.[UnabsorbedDepreciation_BF]
      ,BL.[HousePropIncome_BF]
      ,BL.[IncomeOtherSources_BF]
      ,BL.[IncomeCapGainsLTCG_CY]
	  ,BL.[IncomeCapGainsSTCG_CY]
	  ,BL.[IncomeBusinessProf_CY]
      ,BL.[IncomeSpeculativeBusiness_CY]
      ,BL.[UnabsorbedDepreciation_CY]
      ,BL.[HousePropIncome_CY]
	  ,BL.[IncomeOtherSources_CY]
      ,BL.[IncomeCapGainsLTCG_UL]
      ,BL.[IncomeCapGainsSTCG_UL]
      ,BL.[IncomeBusinessProf_UL]
      ,BL.[IncomeSpeculativeBusiness_UL]
      ,BL.[UnabsorbedDepreciation_UL]
      ,BL.[HousePropIncome_UL]
      ,BL.[IncomeOtherSources_UL]
      ,BL.[IncomeCapGainsLTCG_UALL]
      ,BL.[IncomeCapGainsSTCG_UALL]
      ,BL.[IncomeBusinessProf_UALL]
      ,BL.[IncomeSpeculativeBusiness_UALL]
      ,BL.[UnabsorbedDepreciation_UALL]
      ,BL.[HousePropIncome_UALL]
      ,BL.[IncomeOtherSources_UALL]
      ,BL.[Active]
	FROM [BusinessLossDetails] BL
	INNER JOIN CompanyMaster COMP ON COMP.Id = BL.CompanyId
	INNER JOIN FYAYMaster FYAY ON FYAY.Id = BL.FYAYId
	INNER JOIN ITSectionCategory ITS ON ITS.Id = BL.ITSectionCategoryId
	WHERE BL.Active = 1
	AND BL.Id IN (@PREV_YEAR_BL_DETAILS_ID, @BL_DETAILS_ID)

END

ELSE 
BEGIN
	SELECT
	   CONVERT(BIT, 0) AS IsCurrentYear 
	  ,BL.[Id]
      ,BL.[CompanyId]
	  ,COMP.CompanyName
      ,BL.[FYAYId]
	  ,FYAY.AssessmentYear
	  ,FYAY.FinancialYear
      ,BL.[ITSectionCategoryId]
	  ,ITS.CategoryDesc
      ,BL.[IncomeCapGainsLTCG_BF]
      ,BL.[IncomeCapGainsSTCG_BF]
      ,BL.[IncomeBusinessProf_BF]
      ,BL.[IncomeSpeculativeBusiness_BF]
      ,BL.[UnabsorbedDepreciation_BF]
      ,BL.[HousePropIncome_BF]
      ,BL.[IncomeOtherSources_BF]
      ,BL.[IncomeCapGainsLTCG_CY]
	  ,BL.[IncomeCapGainsSTCG_CY]
	  ,BL.[IncomeBusinessProf_CY]
      ,BL.[IncomeSpeculativeBusiness_CY]
      ,BL.[UnabsorbedDepreciation_CY]
      ,BL.[HousePropIncome_CY]
	  ,BL.[IncomeOtherSources_CY]
      ,BL.[IncomeCapGainsLTCG_UL]
      ,BL.[IncomeCapGainsSTCG_UL]
      ,BL.[IncomeBusinessProf_UL]
      ,BL.[IncomeSpeculativeBusiness_UL]
      ,BL.[UnabsorbedDepreciation_UL]
      ,BL.[HousePropIncome_UL]
      ,BL.[IncomeOtherSources_UL]
      ,BL.[IncomeCapGainsLTCG_UALL]
      ,BL.[IncomeCapGainsSTCG_UALL]
      ,BL.[IncomeBusinessProf_UALL]
      ,BL.[IncomeSpeculativeBusiness_UALL]
      ,BL.[UnabsorbedDepreciation_UALL]
      ,BL.[HousePropIncome_UALL]
      ,BL.[IncomeOtherSources_UALL]
      ,BL.[Active]
	FROM [BusinessLossDetails] BL
	INNER JOIN CompanyMaster COMP ON COMP.Id = BL.CompanyId
	INNER JOIN FYAYMaster FYAY ON FYAY.Id = BL.FYAYId
	INNER JOIN ITSectionCategory ITS ON ITS.Id = BL.ITSectionCategoryId
	WHERE BL.Active = 1
	AND (@COMPANY_ID IS NULL OR BL.CompanyId = @COMPANY_ID)
	AND (@FYAY_ID IS NULL OR BL.FYAYId = @FYAY_ID)
	AND (@IT_SECTION_CATEGORY_ID IS NULL OR BL.ITSectionCategoryId = @IT_SECTION_CATEGORY_ID)
END
SELECT *
	FROM ITReturnDetails ITR
	WHERE ITR.Id = @IT_RETURN_DETAILS_ID			
END

GO


