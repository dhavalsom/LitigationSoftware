USE [LitigationApp]
GO

/****** Object:  StoredProcedure [dbo].[SP_LOG_ERRORS]    Script Date: 6/09/2019 10:54:50 PM ******/
DROP PROCEDURE [dbo].[SP_GET_COMPETITOR_TAX_RATE_REPORT]
GO

/****** Object:  StoredProcedure [dbo].[SP_LOG_ERRORS]    Script Date: 6/09/2019 10:54:50 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE SP_GET_COMPETITOR_TAX_RATE_REPORT
(	
	@COMPANY_ID bigint = NULL
)
AS

BEGIN
		
	Select CM.Id As CompetitorId, CM.Description As CompetitorName,  FYM.FinancialYear, IsNull(CTR.TaxRate, 0) As TaxRate
	  From CompetitorMaster CM 
	  Left Join CompetitorTaxRate CTR ON CTR.CompetitorId = CM.Id
	  Left Join FYAYMaster FYM On FYM.Id = CTR.FYAYId
	 Where CM.CompanyId = @COMPANY_ID
	 Order By 1, 3;

END

GO