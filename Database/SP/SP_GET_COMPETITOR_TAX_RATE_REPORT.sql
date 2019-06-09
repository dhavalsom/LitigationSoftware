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
		
	With CM_FY As (
		Select CM.Id As CompetitorId, CM.Description As CompetitorName, FYM.FinancialYear, FYM.Id As FYId
		  From CompetitorMaster CM
		 Inner Join FYAYMaster FYM ON FYM.Active = 1
		 Where CM.CompanyId = @COMPANY_ID
		   And CM.Active = 1
	), CMP_FY As (
		Select CM.Id As CompanyId, CM.CompanyName, FYM.FinancialYear, FYM.Id As FYId
		  From CompanyMaster CM
		 Inner Join FYAYMaster FYM ON FYM.Active = 1
		 Where CM.Id = @COMPANY_ID
	), ITRD As (
		Select RD.FYAYID, RD.ITReturnFillingDate, SM.Description, 10 As TaxRate,
			   Row_Number() Over(Partition By RD.FYAYID Order by RD.ITReturnFillingDate Desc) As Row_Index
		  From ITReturnDetails RD
		 Inner Join ITSectionMaster SM ON RD.ITSectionID = SM.Id 
		 Where RD.CompanyID = @COMPANY_ID
		   And SM.SECTIONCATEGORYID = 1
	), ITRD_I As (
		Select * From ITRD Where Row_Index = 1
	)
	Select 0 As CompetitorId, C.CompanyName As CompetitorName, C.FinancialYear, IsNull(I.TaxRate, 0) As TaxRate
	  From CMP_FY C
	  Left Join ITRD_I I On I.FYAYID = C.FYId

	Union All

	Select CM.CompetitorId, CM.CompetitorName,  CM.FinancialYear, IsNull(CTR.TaxRate, 0) As TaxRate
	  From CM_FY CM 
	  Left Join CompetitorTaxRate CTR ON CTR.CompetitorId = CM.CompetitorId And CTR.FYAYId = CM.FYId  
	 Order By 1, 3;	 

END

GO