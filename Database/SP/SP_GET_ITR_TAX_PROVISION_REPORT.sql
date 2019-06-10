USE [LitigationApp]
GO

/****** Object:  StoredProcedure [dbo].[SP_LOG_ERRORS]    Script Date: 6/09/2019 10:54:50 PM ******/
DROP PROCEDURE [dbo].[SP_GET_ITR_TAX_PROVISION_REPORT]
GO

/****** Object:  StoredProcedure [dbo].[SP_LOG_ERRORS]    Script Date: 6/09/2019 10:54:50 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE SP_GET_ITR_TAX_PROVISION_REPORT
(	
	@COMPANY_ID bigint = NULL,
	@NO_OF_YEARS INT = 5
)
AS

BEGIN
		
	With FYAY_I As (
		Select FYM.Id As FYAYID, FYM.FinancialYear, Row_Number() Over(Order By FinancialYear Desc) As Row_Index 
		 From FYAYMaster FYM 
		Where FYM.Active = 1
	), FYAY As (
		Select FYAYID, FinancialYear From FYAY_I Where Row_Index <= @NO_OF_YEARS
	), ITR As (
		Select FYAY.FYAYID, FYAY.FinancialYear, IsNull(ITR.TaxProvisions, 0) As TaxProvisions, IsNull(ITR.TaxAssets, 0) As TaxAssets, IsNull(ITR.ContingentLiabilities, 0) As ContingentLiabilities,
			   Row_number() Over(Partition By FYAY.FYAYID ORder By ITR.ITReturnFillingDate Desc) As Row_Index
		  From FYAY
		  Left Join ITReturnDetails ITR On ITR.CompanyID = @COMPANY_ID And ITR.FYAYID = FYAY.FYAYID
	)
	Select FYAYID, FinancialYear, TaxProvisions, TaxAssets, ContingentLiabilities 
	  From ITR Where Row_index = 1
	 Order by FinancialYear;

END

GO