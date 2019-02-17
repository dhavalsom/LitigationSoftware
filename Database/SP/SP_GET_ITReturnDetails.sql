USE [LitigationApp]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_ITReturnDetails]    Script Date: 2/16/2019 11:10:29 PM ******/
DROP PROCEDURE [dbo].[SP_GET_ITReturnDetails]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_ITReturnDetails]    Script Date: 2/16/2019 11:10:29 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






--exec dbo.[SP_GET_ITReturnDetails] 3,1,4


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
	
	DECLARE @GET_DEFAULT_DATA AS BIT = 0;
	DECLARE @GET_COMPUTATION_DATA AS BIT = 0;

	/*Check for the @ITReturnID if the  @ITReturnID is NOT passed 
	and (@COMPANY_ID and @FYAYID) are passed*/
	IF @ITReturnID IS NULL AND @COMPANY_ID IS NOT NULL AND @FYAYID IS NOT NULL
	BEGIN
		IF @ITSectionID IS NOT NULL
		BEGIN
			SELECT @ITReturnID = ITRD.Id
		FROM ITReturnDetails ITRD
		  INNER JOIN ITSectionMaster ITSM ON ITRD.ITSectionID = ITSM.Id
		  INNER JOIN CompanyMaster cm ON cm.id = itrd.CompanyID
		  INNER JOIN ITSectionCategory ITSC ON ITSC.ID = ITSM.SECTIONCATEGORYID  
			WHERE 
			(@COMPANY_ID IS NULL OR ITRD.CompanyID = @COMPANY_ID) AND
			(@FYAYID IS NULL OR ITRD.FYAYID = @FYAYID) AND
			(@ITSectionID IS NULL OR ITRD.ITSectionID = @ITSectionID)

			IF @ITReturnID IS NULL
		BEGIN
			SELECT TOP 1 @ITReturnID = ITRD.Id FROM ITReturnDetails ITRD
			INNER JOIN ITSectionMaster ITSM ON ITRD.ITSectionID = ITSM.Id
			INNER JOIN ITSectionCategory ITSC ON ITSC.ID = ITSM.SECTIONCATEGORYID
				WHERE ITRD.CompanyID = @COMPANY_ID
				AND ITRD.FYAYID = @FYAYID
				AND ITSC.Id = (SELECT Id FROM ITSectionCategory WHERE [CategoryDesc] = 'ROI')
				ORDER BY ITRD.[ITReturnFillingDate] DESC
			SET @GET_DEFAULT_DATA =  1;
		END
		END
		ELSE 
		BEGIN
			SET @GET_COMPUTATION_DATA =  1;
		END
	END


	PRINT '@GET_DEFAULT_DATA' 
	PRINT @GET_DEFAULT_DATA
	PRINT '@@GET_COMPUTATION_DATA' 
	PRINT @GET_COMPUTATION_DATA
	PRINT @ITReturnID
	SELECT 
	  CASE @GET_DEFAULT_DATA WHEN 0 THEN itrd.id  ELSE 0 END as ITReturnDetailsId,
      [CompanyID]
	  ,CompanyName
      ,[FYAYID]
      ,CASE @GET_DEFAULT_DATA WHEN 0 THEN [ITSectionID] ELSE @ITSectionID END as [ITSectionID]
	  ,CASE @GET_DEFAULT_DATA WHEN 0 THEN ITSM.[Description] 
		ELSE (SELECT ITSM_INNER.[Description] FROM ITSectionMaster ITSM_INNER WHERE ITSM_INNER.Id = @ITSectionID) 
		END AS [Description]
	  ,CASE @GET_DEFAULT_DATA WHEN 0 THEN ITSM.[SECTIONCATEGORYID] ELSE 
		(SELECT SECTIONCATEGORYID FROM ITSectionMaster WHERE Id = @ITSectionID)
		END as [SECTIONCATEGORYID]

	  ,CASE @GET_DEFAULT_DATA WHEN 0 THEN ITSC.[CategoryDesc] ELSE 
		(SELECT ITSC_INNER.[CategoryDesc] 
			FROM ITSectionCategory ITSC_INNER 
			INNER JOIN ITSectionMaster ITSM_INNER ON ITSM_INNER.SECTIONCATEGORYID = ITSC_INNER.Id
			WHERE ITSM_INNER.Id = @ITSectionID)
		END as [CategoryDesc]
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
	  ,[TaxCollectedAtSource]
	  ,[ForeignTaxCredit]
	  ,[InterestUS234D]
	  ,[InterestUS220]
	  ,[RefundAdjusted]
	  ,[RegularAssessment]
	  ,[RefundAlreadyReceived]
	  ,[RITotalIncome]
	  ,[RISurcharge]
	  ,[RIEducationCess]
	  ,[MATTotalIncome]
	  ,[MATSurcharge]
	  ,[MATEducationCess]
	  ,[SelfAssessmentTaxDate]
	  ,[AdvanceTax1installmentDate]
	  ,[AdvanceTax2installmentDate]
	  ,[AdvanceTax3installmentDate]
	  ,[AdvanceTax4installmentDate]
	  ,[RefundAdjustedDate]
	  ,[RegularAssessmentDate]
	  ,[RefundAlreadyReceivedDate]
		FROM ITReturnDetails ITRD
  INNER JOIN ITSectionMaster ITSM ON ITRD.ITSectionID = ITSM.Id
  INNER JOIN CompanyMaster cm ON cm.id = itrd.CompanyID
  INNER JOIN ITSectionCategory ITSC ON ITSC.ID = ITSM.SECTIONCATEGORYID  
  WHERE (@GET_COMPUTATION_DATA = 1 OR ITRD.Id = @ITReturnID) AND
  (@COMPANY_ID IS NULL OR ITRD.CompanyID = @COMPANY_ID) AND
  (@FYAYID IS NULL OR ITRD.FYAYID = @FYAYID)
  ORDER BY [ITReturnFillingDate],[ITReturnDueDate]
END





GO


