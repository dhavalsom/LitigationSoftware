USE [LitigationApp]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_ITReturnDetails]    Script Date: 7/11/2018 4:35:57 AM ******/
DROP PROCEDURE [dbo].[SP_GET_ITReturnDetails]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_ITReturnDetails]    Script Date: 7/11/2018 4:35:57 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




--exec dbo.[SP_GET_ITReturnDetails] 3,1


CREATE PROCEDURE [dbo].[SP_GET_ITReturnDetails]
(	
	@COMPANY_ID bigint = NULL,
	@FYAYID bigint = NULL,
	@ITSectionID bigint = NULL,
	@ITReturnID bigint = NULL,
	@ACTIVE BIT = 1
)
AS

BEGIN
		
	SELECT 
	  itrd.id as ITReturnDetailsId,
      [CompanyID]
	  ,CompanyName
      ,[FYAYID]
      ,[ITSectionID]
	  ,ITSM.Description
	  ,ITSM.[SECTIONCATEGORYID]
	  ,ITSC.[CategoryDesc]
	  ,isnull(ITSM.IsReturn,0) as IsReturn
      ,[ITReturnFillingDate]
      ,[ITReturnDueDate]
      ,[HousePropIncome]
      ,[IncomefromCapGainsLTCG]
      ,[IncomefromCapGainsSTCG]
      ,[IncomefromBusinessProf]
      ,[IncomefromSpeculativeBusiness]
      ,[Broughtforwardlosses]
      ,[IncomeFromOtherSources]
      ,[DeductChapterVIA]
      ,[ProfitUS115JB]
      ,[AdvanceTax1installment]
      ,[AdvanceTax2installment]
      ,[AdvanceTax3installment]
      ,[AdvanceTax4installment]
      ,[TDS]
	  ,[TDS26AS] 
      ,[TDSasperBooks]
	  ,[IncomefromSalary]
      ,[TCSPaidbyCompany]
      ,[SelfassessmentTax]
      ,[MATCredit]
      ,[InterestUS234A]
      ,[InterestUS234B]
      ,[InterestUS234C]
      ,[InterestUS244A]
      ,[RefundReceived]
      ,[RevisedReturnFile]
      ,ITRD.[IsDefault]
  FROM [LitigationApp].[dbo].[ITReturnDetails] ITRD,[LitigationApp].[dbo].[ITSectionMaster] ITSM,
  [LitigationApp].[dbo].CompanyMaster cm,[LitigationApp].[dbo].[ITSectionCategory] ITSC
	WHERE 
	(@COMPANY_ID IS NULL OR ITRD.CompanyID = @COMPANY_ID) AND
	(@FYAYID IS NULL OR ITRD.FYAYID = @FYAYID) AND
	(@ITSectionID IS NULL OR ITRD.ITSectionID = @ITSectionID) AND
	(@ITReturnID IS NULL OR ITRD.Id = @ITReturnID) AND
	ITRD.ITSectionID = ITSM.Id and
	cm.id = itrd.CompanyID AND
	ITSC.ID = ITSM.SECTIONCATEGORYID
	order by [ITReturnFillingDate],[ITReturnDueDate]
END






GO


