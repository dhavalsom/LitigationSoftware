USE [LitigationApp]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_MAT_CREDIT_DETAILS_LIST]    Script Date: 12/30/2018 11:27:36 AM ******/
DROP PROCEDURE [dbo].[SP_GET_MAT_CREDIT_DETAILS_LIST]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_MAT_CREDIT_DETAILS_LIST]    Script Date: 12/30/2018 11:27:36 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[SP_GET_MAT_CREDIT_DETAILS_LIST]
(	
	@COMPANY_ID BIGINT = NULL,
	@FYAY_ID BIGINT = NULL,
	@IT_SECTION_CATEGORY_ID BIGINT = NULL,
	@MAT_CREDIT_DETAILS_ID BIGINT = NULL
)
AS

BEGIN

--EXEC [SP_GET_MAT_CREDIT_DETAILS_LIST] 1, 1, 1

SELECT
	   MATC.[Id]
      ,MATC.[CompanyId]
	  ,COMP.CompanyName
      ,MATC.[FYAYId]
	  ,FYAY.AssessmentYear
	  ,FYAY.FinancialYear
      ,MATC.[ITSectionCategoryId]
	  ,ITS.CategoryDesc
      ,MATC.[BusinessLosses_BF]
      ,MATC.[UnabsorbedDepreciation_BF]
      ,MATC.[BusinessLosses_CY]
      ,MATC.[UnabsorbedDepreciation_CY]
      ,MATC.[BusinessLosses_UL]
      ,MATC.[UnabsorbedDepreciation_UL]
      ,MATC.[Active]
	FROM [MATCreditDetails] MATC
	INNER JOIN CompanyMaster COMP ON COMP.Id = MATC.CompanyId
	INNER JOIN FYAYMaster FYAY ON FYAY.Id = MATC.FYAYId
	INNER JOIN ITSectionCategory ITS ON ITS.Id = MATC.ITSectionCategoryId
	WHERE MATC.Active = 1
	AND (@COMPANY_ID IS NULL OR MATC.CompanyId = @COMPANY_ID)
	AND (@FYAY_ID IS NULL OR MATC.FYAYId = @FYAY_ID)
	AND (@IT_SECTION_CATEGORY_ID IS NULL OR MATC.ITSectionCategoryId = @IT_SECTION_CATEGORY_ID)
	AND (@MAT_CREDIT_DETAILS_ID IS NULL OR MATC.Id = @MAT_CREDIT_DETAILS_ID)
END

GO


