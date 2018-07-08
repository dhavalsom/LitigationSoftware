USE [LitigationApp]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_COMPLIANCE_DOCUMENT_LIST]    Script Date: 7/6/2018 11:46:52 AM ******/
DROP PROCEDURE [dbo].[SP_GET_COMPLIANCE_DOCUMENT_LIST]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_COMPLIANCE_DOCUMENT_LIST]    Script Date: 7/6/2018 11:46:52 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[SP_GET_COMPLIANCE_DOCUMENT_LIST]
(	
	@COMPANY_ID BIGINT,
	@FYAY_ID BIGINT,
	@COMPLIANCE_ID BIGINT = NULL,
	@COMPLIANCE_DOCUMENT_ID BIGINT = NULL
)
AS

BEGIN

--EXEC [SP_GET_COMPLIANCE_DOCUMENT_LIST] 1, 1

SELECT CD.Id
	, CD.ComplianceId
	, CM.[Description]  AS ComplianceDescription
	, CD.CompanyId
	, COMP.CompanyName
	, CD.FYAYId
	, FYAY.FinancialYear
	, FYAY.AssessmentYear
	, CD.[FileName]
	, CD.FilePath
	, CD.Active
	, CD.AddedBy
	, CD.AddedDate
	, CD.ModifiedBy
	, CD.ModifiedDate	
	FROM [ComplianceDocuments] CD
	INNER JOIN ComplianceMaster CM ON CD.ComplianceId = CM.Id
	INNER JOIN CompanyMaster COMP ON COMP.Id = CD.CompanyId
	INNER JOIN FYAYMaster FYAY ON FYAY.Id = CD.FYAYId
	WHERE CD.CompanyId = @COMPANY_ID AND CD.FYAYId = @FYAY_ID AND CD.Active = 1
	AND (@COMPLIANCE_ID IS NULL OR CD.ComplianceId = @COMPLIANCE_ID)
	AND (@COMPLIANCE_DOCUMENT_ID IS NULL OR CD.Id = @COMPLIANCE_DOCUMENT_ID)
	ORDER BY CD.AddedDate DESC
END









GO


