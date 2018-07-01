USE [LitigationApp]
GO

/****** Object:  StoredProcedure [dbo].[SP_ITRETURNDETAILS_MANAGER]    Script Date: 7/1/2018 11:06:22 PM ******/
DROP PROCEDURE [dbo].[SP_ITRETURNDETAILS_MANAGER]
GO

/****** Object:  StoredProcedure [dbo].[SP_ITRETURNDETAILS_MANAGER]    Script Date: 7/1/2018 11:06:22 PM ******/
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
        @ITReturnDueDate AS DATETIME, @HousePropIncome AS DECIMAL, @IncomefromCapGainsNonSTT AS DECIMAL,
        @IncomefromCapGainsSTT AS DECIMAL, @IncomefromBusinessProf AS BIT, @UnabsorbedDepreciation AS DECIMAL,
        @Broughtforwardlosses AS DECIMAL, @IncomeFromOtherSources AS DECIMAL, @DeductChapterVIA AS DECIMAL,
        @ProfitUS115JB AS DECIMAL, @AdvanceTax1installment AS DECIMAL, @AdvanceTax2installment AS DECIMAL,
        @AdvanceTax3installment AS DECIMAL, @AdvanceTax4installment AS DECIMAL, @TDS AS DECIMAL,
        @TCSPaidbyCompany AS DECIMAL, @SelfassessmentTax AS DECIMAL, @MATCredit AS DECIMAL,
		@InterestUS234A AS DECIMAL, @InterestUS234B AS DECIMAL, @InterestUS234C AS DECIMAL,
        @InterestUS244A AS DECIMAL, @RefundReceived AS DECIMAL, @RevisedReturnFile AS BIT,
		@ExtId AS BIGINT, @ITSubHeadId AS DECIMAL, @ITSubHeadValue AS DECIMAL,@IdentityVal AS BIGINT


SELECT	 @Id = ITReturnDetailsList.Columns.value('Id[1]', 'BIGINT')
	   , @CompanyID = ITReturnDetailsList.Columns.value('CompanyID[1]', 'BIGINT')
	   , @FYAYID = ITReturnDetailsList.Columns.value('FYAYID[1]', 'BIGINT')
	   , @ITSectionID = ITReturnDetailsList.Columns.value('ITSectionID[1]', 'BIGINT')   
	   , @ITReturnFillingDate = ITReturnDetailsList.Columns.value('ITReturnFillingDate[1]', 'DATETIME')
	   , @ITReturnDueDate = ITReturnDetailsList.Columns.value('ITReturnDueDate[1]', 'DATETIME')
	   , @HousePropIncome = ITReturnDetailsList.Columns.value('HousePropIncome[1]', 'DECIMAL')
	   , @IncomefromCapGainsNonSTT = ITReturnDetailsList.Columns.value('IncomefromCapGainsNonSTT[1]', 'DECIMAL')
	   , @IncomefromCapGainsSTT = ITReturnDetailsList.Columns.value('IncomefromCapGainsSTT[1]', 'DECIMAL')
	   , @IncomefromBusinessProf = ITReturnDetailsList.Columns.value('IncomefromBusinessProf[1]', 'BIT')
	   , @UnabsorbedDepreciation = ITReturnDetailsList.Columns.value('UnabsorbedDepreciation[1]', 'DECIMAL')
	   , @Broughtforwardlosses = ITReturnDetailsList.Columns.value('Broughtforwardlosses[1]', 'DECIMAL')
	   , @IncomeFromOtherSources = ITReturnDetailsList.Columns.value('IncomeFromOtherSources[1]', 'DECIMAL')
	   , @DeductChapterVIA = ITReturnDetailsList.Columns.value('DeductChapterVIA[1]', 'DECIMAL')
	   , @ProfitUS115JB = ITReturnDetailsList.Columns.value('ProfitUS115JB[1]', 'DECIMAL')
	   , @AdvanceTax1installment = ITReturnDetailsList.Columns.value('AdvanceTax1installment[1]', 'DECIMAL')
	   , @AdvanceTax2installment = ITReturnDetailsList.Columns.value('AdvanceTax2installment[1]', 'DECIMAL')
	   , @AdvanceTax3installment = ITReturnDetailsList.Columns.value('AdvanceTax3installment[1]', 'DECIMAL')
	   , @AdvanceTax4installment = ITReturnDetailsList.Columns.value('AdvanceTax4installment[1]', 'DECIMAL')
	   , @TDS = ITReturnDetailsList.Columns.value('TDS[1]', 'DECIMAL')
	   , @TCSPaidbyCompany = ITReturnDetailsList.Columns.value('TCSPaidbyCompany[1]', 'DECIMAL')
	   , @SelfassessmentTax = ITReturnDetailsList.Columns.value('SelfassessmentTax[1]', 'DECIMAL')
	   , @MATCredit = ITReturnDetailsList.Columns.value('MATCredit[1]', 'DECIMAL')
	   , @InterestUS234A = ITReturnDetailsList.Columns.value('InterestUS234A[1]', 'DECIMAL')
	   , @InterestUS234B = ITReturnDetailsList.Columns.value('InterestUS234B[1]', 'DECIMAL')
	   , @InterestUS234C = ITReturnDetailsList.Columns.value('InterestUS234C[1]', 'DECIMAL')
	   , @InterestUS244A = ITReturnDetailsList.Columns.value('InterestUS244A[1]', 'DECIMAL')
	   , @RefundReceived = ITReturnDetailsList.Columns.value('RefundReceived[1]', 'DECIMAL')
	   , @RevisedReturnFile = ITReturnDetailsList.Columns.value('RevisedReturnFile[1]', 'BIT')
	   , @Active = ITReturnDetailsList.Columns.value('Active[1]', 'BIT')
FROM   @ITRETURNDETAILS_XML.nodes('ITReturnComplexAPIModel/ITReturnDetailsObject') AS ITReturnDetailsList(Columns)


SELECT	 @ExtId = ITReturnDetailsList.Columns.value('Id[1]', 'BIGINT')
	   , @ITSubHeadId = ITReturnDetailsList.Columns.value('ITSubHeadId[1]', 'DECIMAL')
	   , @ITSubHeadValue = ITReturnDetailsList.Columns.value('ITSubHeadValue[1]', 'DECIMAL')
	   , @Active = ITReturnDetailsList.Columns.value('Active[1]', 'BIT')
FROM   @ITRETURNDETAILS_XML.nodes('ITReturnComplexAPIModel/ExtensionList') AS ITReturnDetailsList(Columns)


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

	set @IdentityVal = @@IDENTITY
	
	IF @ITSubHeadId IS NOT NULL AND @ITSubHeadValue IS NOT NULL
		BEGIN
			INSERT INTO [dbo].[ITReturnDetailsExtension]
			( 
			   [ITReturnDetailsId]
			  ,[ITSubHeadId]
			  ,[ITSubHeadValue]
			  ,[Active]
			  ,[AddedBy]
			  ,[AddedDate]
			)
			VALUES
			(
			  @IdentityVal
			 ,@ITSubHeadId
			 ,@ITSubHeadValue
			 ,@Active
			 ,@USER_ID
			 ,GETUTCDATE()
			)
		END

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


		IF @ITSubHeadId IS NOT NULL AND @ITSubHeadValue IS NOT NULL
		BEGIN
		UPDATE [dbo].[ITReturnDetailsExtension]
		   SET [ITSubHeadValue] = @ITSubHeadValue
		   WHERE Id = @Id AND ITSubHeadId = @ITSubHeadId
		END


	END


SELECT @Result AS Result, @ReturnMessage AS ReturnMessage
END
















GO


