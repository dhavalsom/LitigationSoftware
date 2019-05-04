USE [LitigationApp]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_SP_COMPETITOR_TAX_RATES]    Script Date: 5/4/2019 9:32:58 PM ******/
DROP PROCEDURE [dbo].[SP_GET_SP_COMPETITOR_TAX_RATES]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_SP_COMPETITOR_TAX_RATES]    Script Date: 5/4/2019 9:32:58 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SP_GET_SP_COMPETITOR_TAX_RATES]
(	
	@COMPANY_ID BIGINT,
	@INSERT_DUMMY_RECORDS BIT = NULL,
	@ACTIVE BIT = NULL
)
AS

BEGIN

--EXEC [SP_GET_SP_COMPETITOR_TAX_RATES] 1
	DECLARE @RECORD_COUNT AS INT

	SELECT @RECORD_COUNT = COUNT(1)
	FROM CompetitorMaster CM
	WHERE CM.CompanyId = @COMPANY_ID;

	IF ISNULL(@INSERT_DUMMY_RECORDS,0) = 1 AND @RECORD_COUNT = 0
	BEGIN
		
		INSERT INTO [dbo].[CompetitorMaster]
			   ([CompanyId],[Description],[Active],[AddedBy],[AddedDate])
     
		SELECT @COMPANY_ID AS CompanyId, 'Competitor 1' AS 'Description', 1 'Active' , 1 'AddedBy', GETUTCDATE() AS 'AddedDate'
		UNION
		SELECT @COMPANY_ID AS CompanyId, 'Competitor 2' AS 'Description', 1 'Active' , 1 'AddedBy', GETUTCDATE() AS 'AddedDate'
		UNION
		SELECT @COMPANY_ID AS CompanyId, 'Competitor 3' AS 'Description', 1 'Active' , 1 'AddedBy', GETUTCDATE() AS 'AddedDate'
		
		INSERT INTO [dbo].[CompetitorTaxRate]
           ([CompetitorId]
           ,[FYAYId]
           ,[TaxRate]
           ,[Active]
           ,[AddedBy]
           ,[AddedDate])

			SELECT CM.Id AS [CompetitorId] 
				, FYAY.Id AS [FYAYId]
				, 0 AS [TaxRate]
				, 1 'Active' 
				, 1 'AddedBy'
				, GETUTCDATE() AS 'AddedDate'
				FROM CompetitorMaster CM, FYAYMaster FYAY
			WHERE CM.CompanyId = @COMPANY_ID
	END

	SELECT 
	   CTR.[Id]
      , CTR.[CompetitorId]
	  , CM.[Description]
	  , CM.CompanyId
	  , C.CompanyName
      , CTR.[FYAYId]
	  , FYAY.AssessmentYear
	  , FYAY.FinancialYear
      , CTR.[TaxRate]
      , CTR.[Active]
      
	FROM [CompetitorTaxRate] CTR
	INNER JOIN CompetitorMaster CM ON CM.Id = CTR.CompetitorId
	INNER JOIN FYAYMaster FYAY ON FYAY.Id = CTR.[FYAYId]
	INNER JOIN CompanyMaster C ON C.Id = CM.CompanyId
	WHERE (@ACTIVE IS NULL OR CTR.[Active] = @ACTIVE)
	AND @COMPANY_ID = CM.CompanyId
	ORDER BY CM.[Description], CTR.[FYAYId]
END









GO


