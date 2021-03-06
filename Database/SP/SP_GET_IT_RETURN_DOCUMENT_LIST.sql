USE [LitigationApp]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_IT_RETURN_DOCUMENT_LIST]    Script Date: 4/19/2019 4:56:27 PM ******/
DROP PROCEDURE [dbo].[SP_GET_IT_RETURN_DOCUMENT_LIST]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_IT_RETURN_DOCUMENT_LIST]    Script Date: 4/19/2019 4:56:27 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[SP_GET_IT_RETURN_DOCUMENT_LIST]
(	
	@COMPANY_ID BIGINT = NULL,
	@FYAY_ID BIGINT = NULL,
	@IT_RETURN_DETAILS_ID BIGINT = NULL,
	@IT_HEAD_ID BIGINT = NULL,
	@IT_RETURN_DOCUMENT_ID BIGINT = NULL,
	@DOCUMENT_CATEGORY_ID BIGINT = NULL,
	@SUB_DOCUMENT_CATEGORY_ID BIGINT = NULL,
	@IT_SECTION_ID  BIGINT = NULL,
	@IT_SECTION_CATEGORY_ID  BIGINT = NULL
)
AS

BEGIN

--EXEC [SP_GET_IT_RETURN_DOCUMENT_LIST] 1, 1

SELECT ITRD.Id
	, ITRD.ITReturnDetailsId
	, ITSM.Id AS ITSectionId
	, ITSM.Description AS ITSectionDescription
	, ITSC.Id AS SectionCategoryId
	, ITSC.CategoryDesc AS SectionCategoryDescription
	, ITRD.ITHeadId
	, ITRD.DocumentCategoryId
	, DCM.Description DocumentCategoryName
	, SDCM.Description SubDocumentCategoryName
	, ITRD.SubDocumentCategoryId
	, ITH.ExcelSrNo
	, ITH.[Description] ITHeadDescription
	, ITH.PropertyName
	, COMP.Id AS CompanyId
	, COMP.CompanyName
	, FYAY.Id AS FYAYId
	, FYAY.FinancialYear
	, FYAY.AssessmentYear
	, ITRD.[FileName]
	, ITRD.FilePath
	, ITRD.Active
	, ITRD.AddedBy
	, ITRD.AddedDate
	, ITRD.ModifiedBy
	, ITRD.ModifiedDate	
	FROM [ITReturnDocuments] ITRD
	INNER JOIN ITReturnDetails ITR on ITR.Id = ITRD.ITReturnDetailsId
	INNER JOIN ITSectionMaster ITSM ON ITSM.Id = ITR.ITSectionID
	INNER JOIN ITSectionCategory ITSC ON ITSC.Id = ITSM.SectionCategoryId
	LEFT OUTER JOIN ITHeadMaster ITH ON ITH.Id = ITRD.ITHeadId
	INNER JOIN CompanyMaster COMP ON COMP.Id = ITR.CompanyId
	INNER JOIN FYAYMaster FYAY ON FYAY.Id = ITR.FYAYId
	INNER JOIN DocumentCategoryMaster DCM ON DCM.Id = ITRD.DocumentCategoryId
	LEFT OUTER JOIN SubDocumentCategoryMaster SDCM ON SDCM.Id = ITRD.SubDocumentCategoryId
	WHERE ITRD.Active = 1
	AND (@COMPANY_ID IS NULL OR COMP.Id = @COMPANY_ID)
	AND (@FYAY_ID IS NULL OR FYAY.Id = @FYAY_ID)
	AND (@IT_RETURN_DETAILS_ID IS NULL OR ITR.Id = @IT_RETURN_DETAILS_ID)
	AND (@IT_HEAD_ID IS NULL OR ITH.Id = @IT_HEAD_ID)
	AND (@IT_RETURN_DOCUMENT_ID IS NULL OR ITRD.Id = @IT_RETURN_DOCUMENT_ID)
	AND (@DOCUMENT_CATEGORY_ID IS NULL OR DCM.Id = @DOCUMENT_CATEGORY_ID)
	AND (@SUB_DOCUMENT_CATEGORY_ID IS NULL OR SDCM.Id = @SUB_DOCUMENT_CATEGORY_ID)
	AND (@IT_SECTION_ID IS NULL OR ITSM.Id = @IT_SECTION_ID)
	AND (@IT_SECTION_CATEGORY_ID IS NULL OR ITSC.Id = @IT_SECTION_CATEGORY_ID)
	ORDER BY  COMP.CompanyName, FYAY.FinancialYear, DCM.[Description], SDCM.[Description], ITRD.AddedDate DESC
END






GO


