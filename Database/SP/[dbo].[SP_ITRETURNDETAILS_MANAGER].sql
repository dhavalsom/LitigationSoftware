USE [LitigationApp]
GO

/****** Object:  StoredProcedure [dbo].[SP_ITRETURNDETAILS_MANAGER]    Script Date: 6/18/2018 11:49:17 AM ******/
DROP PROCEDURE [dbo].[SP_ITRETURNDETAILS_MANAGER]
GO

/****** Object:  StoredProcedure [dbo].[SP_ITRETURNDETAILS_MANAGER]    Script Date: 6/18/2018 11:49:17 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO







CREATE PROCEDURE [dbo].[SP_ITRETURNDETAILS_MANAGER]
(
	@ITRETURNDETAILS_XML AS XML,
	@USER_ID BIGINT = 1 --SET THE DEFAULT VALUE TO 1 IF NOT PASSED,
)
AS

BEGIN


/*BLOCK TO READ THE VARIABLES*/

DECLARE @Id AS BIGINT, @CompanyID AS BIGINT, @FYAYID AS BIGINT,@ITSectionID AS BIGINT,
        @ITReturnFillingDate AS DATETIME,@Active AS BIT,@ReturnMessage as NVARCHAR(MAX), @Result as BIT,
        @ITReturnDueDate AS DATETIME, @HousePropIncome AS BIGINT, @IncomefromCapGainsNonSTT AS BIGINT,
        @IncomefromCapGainsSTT AS BIGINT, @IncomefromBusinessProf AS BIT, @UnabsorbedDepreciation AS BIGINT,
        @Broughtforwardlosses AS BIGINT, @IncomeFromOtherSources AS BIGINT, @DeductChapterVIA AS BIGINT,
        @ProfitUS115JB AS BIGINT, @AdvanceTax1installment AS BIGINT, @AdvanceTax2installment AS BIGINT,
        @AdvanceTax3installment AS BIGINT, @AdvanceTax4installment AS BIGINT, @TDS AS BIGINT,
        @TCSPaidbyCompany AS BIGINT, @SelfassessmentTax AS BIGINT, @MATCredit AS BIGINT,
		@InterestUS234A AS BIGINT, @InterestUS234B AS BIGINT, @InterestUS234C AS BIGINT,
        @InterestUS244A AS BIGINT, @RefundReceived AS BIGINT, @RevisedReturnFile AS BIT


SELECT	 @Id = ITReturnDetailsList.Columns.value('Id[1]', 'BIGINT')
	   , @CompanyID = ITReturnDetailsList.Columns.value('CompanyID[1]', 'BIGINT')
	   , @FYAYID = ITReturnDetailsList.Columns.value('FYAYID[1]', 'BIGINT')
	   , @ITSectionID = ITReturnDetailsList.Columns.value('ITSectionID[1]', 'BIGINT')   
	   , @ITReturnFillingDate = ITReturnDetailsList.Columns.value('ITReturnFillingDate[1]', 'DATETIME')
	   , @ITReturnDueDate = ITReturnDetailsList.Columns.value('ITReturnDueDate[1]', 'DATETIME')
	   , @HousePropIncome = ITReturnDetailsList.Columns.value('HousePropIncome[1]', 'BIGINT')
	   , @IncomefromCapGainsNonSTT = ITReturnDetailsList.Columns.value('IncomefromCapGainsNonSTT[1]', 'BIGINT')
	   , @IncomefromCapGainsSTT = ITReturnDetailsList.Columns.value('IncomefromCapGainsSTT[1]', 'BIGINT')
	   , @IncomefromBusinessProf = ITReturnDetailsList.Columns.value('IncomefromBusinessProf[1]', 'BIT')
	   , @UnabsorbedDepreciation = ITReturnDetailsList.Columns.value('UnabsorbedDepreciation[1]', 'BIGINT')
	   , @Broughtforwardlosses = ITReturnDetailsList.Columns.value('Broughtforwardlosses[1]', 'BIGINT')
	   , @IncomeFromOtherSources = ITReturnDetailsList.Columns.value('IncomeFromOtherSources[1]', 'BIGINT')
	   , @DeductChapterVIA = ITReturnDetailsList.Columns.value('DeductChapterVIA[1]', 'BIGINT')
	   , @ProfitUS115JB = ITReturnDetailsList.Columns.value('ProfitUS115JB[1]', 'BIGINT')
	   , @AdvanceTax1installment = ITReturnDetailsList.Columns.value('AdvanceTax1installment[1]', 'BIGINT')
	   , @AdvanceTax2installment = ITReturnDetailsList.Columns.value('AdvanceTax2installment[1]', 'BIGINT')
	   , @AdvanceTax3installment = ITReturnDetailsList.Columns.value('AdvanceTax3installment[1]', 'BIGINT')
	   , @AdvanceTax4installment = ITReturnDetailsList.Columns.value('AdvanceTax4installment[1]', 'BIGINT')
	   , @TDS = ITReturnDetailsList.Columns.value('TDS[1]', 'BIGINT')
	   , @TCSPaidbyCompany = ITReturnDetailsList.Columns.value('TCSPaidbyCompany[1]', 'BIGINT')
	   , @SelfassessmentTax = ITReturnDetailsList.Columns.value('SelfassessmentTax[1]', 'BIGINT')
	   , @MATCredit = ITReturnDetailsList.Columns.value('MATCredit[1]', 'BIGINT')
	   , @InterestUS234A = ITReturnDetailsList.Columns.value('InterestUS234A[1]', 'BIGINT')
	   , @InterestUS234B = ITReturnDetailsList.Columns.value('InterestUS234B[1]', 'BIGINT')
	   , @InterestUS234C = ITReturnDetailsList.Columns.value('InterestUS234C[1]', 'BIGINT')
	   , @InterestUS244A = ITReturnDetailsList.Columns.value('InterestUS244A[1]', 'BIGINT')
	   , @RefundReceived = ITReturnDetailsList.Columns.value('RefundReceived[1]', 'BIGINT')
	   , @RevisedReturnFile = ITReturnDetailsList.Columns.value('RevisedReturnFile[1]', 'BIT')
	   , @Active = ITReturnDetailsList.Columns.value('Active[1]', 'BIT')
FROM   @ITRETURNDETAILS_XML.nodes('ITReturnDetails') AS ITReturnDetailsList(Columns)

/*BLOCK TO READ THE VARIABLES ENDS HERE*/


	IF @Id IS NULL OR @Id = 0 
	BEGIN

	INSERT INTO [dbo].[ITReturnDetails]
      ([CompanyID]
      ,[FYAYID]
      ,[ITSectionID]
      ,[ITReturnFillingDate]
      ,[ITReturnDueDate]
      ,[HousePropIncome]
      ,[IncomefromCapGainsNonSTT]
      ,[IncomefromCapGainsSTT]
      ,[IncomefromBusinessProf]
      ,[UnabsorbedDepreciation]
      ,[Broughtforwardlosses]
      ,[IncomeFromOtherSources]
      ,[DeductChapterVIA]
      ,[ProfitUS115JB]
      ,[AdvanceTax1installment]
      ,[AdvanceTax2installment]
      ,[AdvanceTax3installment]
      ,[AdvanceTax4installment]
      ,[TDS]
      ,[TCSPaidbyCompany]
      ,[SelfassessmentTax]
      ,[MATCredit]
      ,[InterestUS234A]
      ,[InterestUS234B]
      ,[InterestUS234C]
      ,[InterestUS244A]
      ,[RefundReceived]
      ,[RevisedReturnFile]
      ,[IsDefault]
      ,[Active]
      ,[AddedBy]
      ,[AddedDate])
     VALUES
	(@CompanyID
     ,@FYAYID 
     ,@ITSectionID 
     ,@ITReturnFillingDate 
     ,@ITReturnDueDate 
     ,@HousePropIncome 
     ,@IncomefromCapGainsNonSTT 
     ,@IncomefromCapGainsSTT 
     ,@IncomefromBusinessProf 
     ,@UnabsorbedDepreciation 
     ,@Broughtforwardlosses 
     ,@IncomeFromOtherSources 
     ,@DeductChapterVIA 
     ,@ProfitUS115JB 
     ,@AdvanceTax1installment 
     ,@AdvanceTax2installment 
     ,@AdvanceTax3installment 
     ,@AdvanceTax4installment 
     ,@TDS 
     ,@TCSPaidbyCompany 
     ,@SelfassessmentTax 
		,@MATCredit
		,@InterestUS234A
		,@InterestUS234B
		,@InterestUS234C
		,@InterestUS244A
		,@RefundReceived
		,@RevisedReturnFile
		,0
		,@Active
		,@USER_ID
		,GETUTCDATE())

	SET @Result = 1;
	SET @ReturnMessage = 'Record created successfully.'

	END

	ELSE
	BEGIN

		UPDATE [dbo].[ITReturnDetails]
		   SET 
		   [CompanyID] = @CompanyID
      ,[FYAYID] = @FYAYID
      ,[ITSectionID] = @ITSectionID
      ,[ITReturnFillingDate] = @ITReturnFillingDate
      ,[ITReturnDueDate] = @ITReturnDueDate
      ,[HousePropIncome] = @HousePropIncome
      ,[IncomefromCapGainsNonSTT] = @IncomefromCapGainsNonSTT
      ,[IncomefromCapGainsSTT] = @IncomefromCapGainsSTT
      ,[IncomefromBusinessProf] = @IncomefromBusinessProf
      ,[UnabsorbedDepreciation] = @UnabsorbedDepreciation
      ,[Broughtforwardlosses] = @Broughtforwardlosses
      ,[IncomeFromOtherSources] = @IncomeFromOtherSources
      ,[DeductChapterVIA] = @DeductChapterVIA
      ,[ProfitUS115JB] = @ProfitUS115JB
      ,[AdvanceTax1installment] = @AdvanceTax1installment
      ,[AdvanceTax2installment] = @AdvanceTax2installment
      ,[AdvanceTax3installment] = @AdvanceTax3installment
      ,[AdvanceTax4installment] = @AdvanceTax4installment
      ,[TDS] = @TDS
      ,[TCSPaidbyCompany] = @TCSPaidbyCompany
      ,[SelfassessmentTax] = @SelfassessmentTax
      ,[MATCredit] = @MATCredit
      ,[InterestUS234A] = @InterestUS234A
      ,[InterestUS234B] = @InterestUS234B
      ,[InterestUS234C] = @InterestUS234C
      ,[InterestUS244A] = @InterestUS244A
      ,[RefundReceived] = @RefundReceived
      ,[RevisedReturnFile] = @RevisedReturnFile
		   ,[ModifiedBy] = @USER_ID
			  ,[ModifiedDate] = GETUTCDATE()
		WHERE Id = @Id
		SET @Result = 1;
		SET @ReturnMessage = 'Record updated successfully.'
	END


SELECT @Result AS Result, @ReturnMessage AS ReturnMessage
END















GO


