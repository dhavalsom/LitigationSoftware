USE [LitigationApp]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_BUSINESS_LOSS_DETAILS_LIST]    Script Date: 12/25/2018 2:22:32 PM ******/
DROP PROCEDURE [dbo].[SP_GET_BUSINESS_LOSS_DETAILS_LIST]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_BUSINESS_LOSS_DETAILS_LIST]    Script Date: 12/25/2018 2:22:32 PM ******/
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

--EXEC [SP_GET_BUSINESS_LOSS_DETAILS_LIST] 1, 1

SELECT BL.[Id]
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
	AND (@BUSINESS_LOSS_DETAILS_ID IS NULL OR BL.Id = @BUSINESS_LOSS_DETAILS_ID)
	ORDER BY BL.AddedDate DESC
END


GO


