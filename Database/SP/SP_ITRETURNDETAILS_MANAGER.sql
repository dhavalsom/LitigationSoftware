USE [LitigationApp]
GO

/****** Object:  StoredProcedure [dbo].[SP_ITRETURNDETAILS_MANAGER]    Script Date: 2/16/2019 11:03:12 PM ******/
DROP PROCEDURE [dbo].[SP_ITRETURNDETAILS_MANAGER]
GO

/****** Object:  StoredProcedure [dbo].[SP_ITRETURNDETAILS_MANAGER]    Script Date: 2/16/2019 11:03:12 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[SP_ITRETURNDETAILS_MANAGER]
(
	@ITRETURNDETAILS_XML AS XML,
	@EXTENSIONDETAILS_XML AS XML,
	@OPERATION AS NVARCHAR(25) = NULL,
	@USER_ID BIGINT = 1 --SET THE DEFAULT VALUE TO 1 IF NOT PASSED,
)
AS

BEGIN



/*BLOCK TO READ THE VARIABLES*/

DECLARE @Id AS BIGINT, @CompanyID AS BIGINT, @FYAYID AS BIGINT,@ITSectionID AS BIGINT,
        @ITReturnFillingDate AS DATETIME,@Active AS BIT,@ReturnMessage as NVARCHAR(MAX), @Result as BIT,
        @ITReturnDueDate AS DATETIME, @HousePropIncome AS DECIMAL (18, 2), @IncomefromCapGainsLTCG AS DECIMAL (18, 2),
        @IncomefromCapGainsSTCG AS DECIMAL (18, 2), @IncomefromBusinessProf AS DECIMAL(18,2), @IncomefromSpeculativeBusiness AS DECIMAL (18, 2),
        @Broughtforwardlosses AS BIT, @IncomeFromOtherSources AS DECIMAL (18, 2), @DeductChapterVIA AS DECIMAL (18, 2),
        @ProfitUS115JB AS DECIMAL (18, 2), @AdvanceTax1installment AS DECIMAL (18, 2), @AdvanceTax2installment AS DECIMAL (18, 2),
        @AdvanceTax3installment AS DECIMAL (18, 2), @AdvanceTax4installment AS DECIMAL (18, 2), @TDS AS DECIMAL (18, 2),
        @TCSPaidbyCompany AS DECIMAL (18, 2), @SelfassessmentTax AS DECIMAL (18, 2), @MATCredit AS DECIMAL (18, 2),
		@InterestUS234A AS DECIMAL (18, 2), @InterestUS234B AS DECIMAL (18, 2), @InterestUS234C AS DECIMAL (18, 2),
        @InterestUS244A AS DECIMAL (18, 2), @RefundReceived AS DECIMAL (18, 2), @RevisedReturnFile AS BIT,
		@ExtId AS BIGINT, @ITSubHeadId AS DECIMAL (18, 2), @ITSubHeadValue AS DECIMAL (18, 2),@TDS26AS AS DECIMAL (18, 2),@TDSasperBooks AS DECIMAL (18, 2),
		@IncomefromSalary AS DECIMAL (18, 2), @TaxCollectedAtSource  AS DECIMAL (18, 2), @ForeignTaxCredit  AS DECIMAL (18, 2),
		@InterestUS234D  AS DECIMAL (18, 2), @InterestUS220  AS DECIMAL (18, 2), @RefundAdjusted  AS DECIMAL (18, 2),
		@RegularAssessment  AS DECIMAL (18, 2), @RefundAlreadyReceived AS DECIMAL (18, 2), @SelfAssessmentTaxDate  AS DATETIME, @AdvanceTax1installmentDate  AS DATETIME,
		@AdvanceTax2installmentDate AS DATETIME, @AdvanceTax3installmentDate AS DATETIME, @AdvanceTax4installmentDate  AS DATETIME,
		@RefundAdjustedDate AS DATETIME, @RegularAssessmentDate AS DATETIME, @RefundAlreadyReceivedDate AS DATETIME,
		@RITotalIncome AS DECIMAL (18, 2), @RISurcharge AS DECIMAL (18, 2), @RIEducationCess AS DECIMAL (18, 2),
		@MATTotalIncome AS DECIMAL (18, 2), @MATSurcharge AS DECIMAL (18, 2), @MATEducationCess AS DECIMAL (18, 2)


SELECT	 @Id = ITReturnDetailsList.Columns.value('Id[1]', 'BIGINT')
	   , @CompanyID = ITReturnDetailsList.Columns.value('CompanyID[1]', 'BIGINT')
	   , @FYAYID = ITReturnDetailsList.Columns.value('FYAYID[1]', 'BIGINT')
	   , @ITSectionID = ITReturnDetailsList.Columns.value('ITSectionID[1]', 'BIGINT')   
	   , @ITReturnFillingDate = ITReturnDetailsList.Columns.value('ITReturnFillingDate[1]', 'DATETIME')
	   , @ITReturnDueDate = ITReturnDetailsList.Columns.value('ITReturnDueDate[1]', 'DATETIME')
	   , @HousePropIncome = ITReturnDetailsList.Columns.value('HousePropIncome[1]', 'DECIMAL (18, 2)')
	   , @IncomefromCapGainsLTCG = ITReturnDetailsList.Columns.value('IncomefromCapGainsLTCG[1]', 'DECIMAL (18, 2)')
	   , @IncomefromCapGainsSTCG = ITReturnDetailsList.Columns.value('IncomefromCapGainsSTCG[1]', 'DECIMAL (18, 2)')
	   , @IncomefromBusinessProf = ITReturnDetailsList.Columns.value('IncomefromBusinessProf[1]', 'DECIMAL (18, 2)')
	   , @IncomefromSpeculativeBusiness = ITReturnDetailsList.Columns.value('IncomefromSpeculativeBusiness[1]', 'DECIMAL (18, 2)')
	   , @Broughtforwardlosses = ITReturnDetailsList.Columns.value('Broughtforwardlosses[1]', 'BIT')
	   , @IncomeFromOtherSources = ITReturnDetailsList.Columns.value('IncomeFromOtherSources[1]', 'DECIMAL (18, 2)')
	   , @DeductChapterVIA = ITReturnDetailsList.Columns.value('DeductChapterVIA[1]', 'DECIMAL (18, 2)')
	   , @ProfitUS115JB = ITReturnDetailsList.Columns.value('ProfitUS115JB[1]', 'DECIMAL (18, 2)')
	   , @AdvanceTax1installment = ITReturnDetailsList.Columns.value('AdvanceTax1installment[1]', 'DECIMAL (18, 2)')
	   , @AdvanceTax2installment = ITReturnDetailsList.Columns.value('AdvanceTax2installment[1]', 'DECIMAL (18, 2)')
	   , @AdvanceTax3installment = ITReturnDetailsList.Columns.value('AdvanceTax3installment[1]', 'DECIMAL (18, 2)')
	   , @AdvanceTax4installment = ITReturnDetailsList.Columns.value('AdvanceTax4installment[1]', 'DECIMAL (18, 2)')
	   , @TDS = ITReturnDetailsList.Columns.value('TDS[1]', 'DECIMAL (18, 2)')
	   , @TCSPaidbyCompany = ITReturnDetailsList.Columns.value('TCSPaidbyCompany[1]', 'DECIMAL (18, 2)')
	   , @SelfassessmentTax = ITReturnDetailsList.Columns.value('SelfAssessmentTax[1]', 'DECIMAL (18, 2)')
	   , @MATCredit = ITReturnDetailsList.Columns.value('MATCredit[1]', 'DECIMAL (18, 2)')
	   , @InterestUS234A = ITReturnDetailsList.Columns.value('InterestUS234A[1]', 'DECIMAL (18, 2)')
	   , @InterestUS234B = ITReturnDetailsList.Columns.value('InterestUS234B[1]', 'DECIMAL (18, 2)')
	   , @InterestUS234C = ITReturnDetailsList.Columns.value('InterestUS234C[1]', 'DECIMAL (18, 2)')
	   , @InterestUS244A = ITReturnDetailsList.Columns.value('InterestUS244A[1]', 'DECIMAL (18, 2)')
	   , @RefundReceived = ITReturnDetailsList.Columns.value('RefundReceived[1]', 'DECIMAL (18, 2)')
	   , @RevisedReturnFile = ITReturnDetailsList.Columns.value('RevisedReturnFile[1]', 'BIT')
	   , @Active = ITReturnDetailsList.Columns.value('Active[1]', 'BIT')
	   , @TDS26AS = ITReturnDetailsList.Columns.value('TDS26AS[1]', 'DECIMAL (18, 2)')
	   , @TDSasperBooks = ITReturnDetailsList.Columns.value('TDSasperBooks[1]', 'DECIMAL (18, 2)')
	   , @IncomefromSalary = ITReturnDetailsList.Columns.value('IncomefromSalary[1]', 'DECIMAL (18, 2)')
	   , @TaxCollectedAtSource = ITReturnDetailsList.Columns.value('TaxCollectedAtSource[1]', 'DECIMAL (18, 2)')
	   , @ForeignTaxCredit = ITReturnDetailsList.Columns.value('ForeignTaxCredit[1]', 'DECIMAL (18, 2)')
	   , @InterestUS234D = ITReturnDetailsList.Columns.value('InterestUS234D[1]', 'DECIMAL (18, 2)')
	   , @InterestUS220 = ITReturnDetailsList.Columns.value('InterestUS220[1]', 'DECIMAL (18, 2)')
	   , @RefundAdjusted = ITReturnDetailsList.Columns.value('RefundAdjusted[1]', 'DECIMAL (18, 2)')
	   , @RegularAssessment = ITReturnDetailsList.Columns.value('RegularAssessment[1]', 'DECIMAL (18, 2)')
	   , @RefundAlreadyReceived = ITReturnDetailsList.Columns.value('RefundAlreadyReceived[1]', 'DECIMAL (18, 2)')
	   , @RITotalIncome = ITReturnDetailsList.Columns.value('RITotalIncome[1]', 'DECIMAL (18, 2)')
	   , @RISurcharge = ITReturnDetailsList.Columns.value('RISurcharge[1]', 'DECIMAL (18, 2)')
	   , @RIEducationCess = ITReturnDetailsList.Columns.value('RIEducationCess[1]', 'DECIMAL (18, 2)')
	   , @MATTotalIncome = ITReturnDetailsList.Columns.value('MATTotalIncome[1]', 'DECIMAL (18, 2)')
	   , @MATSurcharge = ITReturnDetailsList.Columns.value('MATSurcharge[1]', 'DECIMAL (18, 2)')
	   , @MATEducationCess = ITReturnDetailsList.Columns.value('MATEducationCess[1]', 'DECIMAL (18, 2)')
	   , @SelfAssessmentTaxDate = ITReturnDetailsList.Columns.value('SelfAssessmentTaxDate[1]', 'DATETIME')
	   , @AdvanceTax1installmentDate = ITReturnDetailsList.Columns.value('AdvanceTax1installmentDate[1]', 'DATETIME')
	   , @AdvanceTax2installmentDate = ITReturnDetailsList.Columns.value('AdvanceTax2installmentDate[1]', 'DATETIME')
	   , @AdvanceTax3installmentDate = ITReturnDetailsList.Columns.value('AdvanceTax3installmentDate[1]', 'DATETIME')
	   , @AdvanceTax4installmentDate = ITReturnDetailsList.Columns.value('AdvanceTax4installmentDate[1]', 'DATETIME')
	   , @RefundAdjustedDate = ITReturnDetailsList.Columns.value('RefundAdjustedDate[1]', 'DATETIME')
	   , @RegularAssessmentDate = ITReturnDetailsList.Columns.value('RegularAssessmentDate[1]', 'DATETIME')
	   , @RefundAlreadyReceivedDate = ITReturnDetailsList.Columns.value('RefundAlreadyReceivedDate[1]', 'DATETIME')
FROM   @ITRETURNDETAILS_XML.nodes('ITReturnDetails') AS ITReturnDetailsList(Columns)

SELECT 
    @ExtId = x.Rec.query('./ExtID').value('.', 'bigint'),
    @ITSubHeadId = x.Rec.query('./ITSubHeadId').value('.', 'bigint'),
    @ITSubHeadValue = x.Rec.query('./ITSubHeadValue').value('.', 'DECIMAL (18, 2)')
FROM @EXTENSIONDETAILS_XML.nodes('/ArrayOfITReturnDetailsExtension/ITReturnDetailsExtension') as x(Rec)


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
      ,[AddedDate]
	  ,[TDS26AS] 
      ,[TDSasperBooks]
	  ,[IncomefromSalary]	  
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
	  ,[RefundAlreadyReceivedDate])
     VALUES
	(@CompanyID
     ,@FYAYID 
     ,@ITSectionID 
     ,@ITReturnFillingDate 
     ,@ITReturnDueDate 
     ,@HousePropIncome 
     ,@IncomefromCapGainsLTCG 
     ,@IncomefromCapGainsSTCG 
     ,@IncomefromBusinessProf 
     ,@IncomefromSpeculativeBusiness 
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
	,GETUTCDATE()
	,@TDS26AS 
    ,@TDSasperBooks
	,@IncomefromSalary	
	,@TaxCollectedAtSource
	,@ForeignTaxCredit
	,@InterestUS234D
	,@InterestUS220
	,@RefundAdjusted
	,@RegularAssessment
	,@RefundAlreadyReceived
	,@RITotalIncome
	,@RISurcharge
	,@RIEducationCess
	,@MATTotalIncome
	,@MATSurcharge
	,@MATEducationCess
	,@SelfAssessmentTaxDate
	,@AdvanceTax1installmentDate
	,@AdvanceTax2installmentDate
	,@AdvanceTax3installmentDate
	,@AdvanceTax4installmentDate
	,@RefundAdjustedDate
	,@RegularAssessmentDate
	,@RefundAlreadyReceivedDate)

	set @Id = @@IDENTITY
	
	IF @ITSubHeadId IS NOT NULL AND @ITSubHeadValue IS NOT NULL
		BEGIN
			INSERT INTO [dbo].[ITReturnDetailsExtension]
			( 
			   [ITReturnDetailsId]
			  ,[ITSubHeadId]
			  ,[ITSubHeadValue]
			  ,[ITSubHeadDate]
			  ,[Active]
			  ,[AddedBy]
			  ,[AddedDate]
			)
			SELECT 
				@Id,
				x.Rec.query('./ITSubHeadId').value('.', 'bigint'),
				x.Rec.query('./ITSubHeadValue').value('.', 'DECIMAL (18, 2)'),
				x.Rec.query('./ITSubHeadDate').value('.', 'DATETIME'),
				1,
				@USER_ID,
				GETUTCDATE()
			FROM @EXTENSIONDETAILS_XML.nodes('/ArrayOfITReturnDetailsExtension/ITReturnDetailsExtension') as x(Rec)
		END

	SET @Result = 1;
	SET @ReturnMessage = 'Record created successfully.'

	END

	ELSE
	BEGIN

		IF @OPERATION IS NULL
		BEGIN

		UPDATE [dbo].[ITReturnDetails]
		   SET 
		   [CompanyID] = @CompanyID
		  ,[FYAYID] = @FYAYID
		  ,[ITSectionID] = @ITSectionID
		  ,[ITReturnFillingDate] = @ITReturnFillingDate
		  ,[ITReturnDueDate] = @ITReturnDueDate
		  ,[HousePropIncome] = @HousePropIncome
		  ,[IncomefromCapGainsLTCG] = @IncomefromCapGainsLTCG
		  ,[IncomefromCapGainsSTCG] = @IncomefromCapGainsSTCG
		  ,[IncomefromBusinessProf] = @IncomefromBusinessProf
		  ,[IncomefromSpeculativeBusiness] = @IncomefromSpeculativeBusiness
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
		  ,[TDS26AS] = @TDS26AS 
          ,[TDSasperBooks] = @TDSasperBooks
		  ,[IncomefromSalary] = @IncomefromSalary
		  ,[TaxCollectedAtSource]= @TaxCollectedAtSource
		  ,[ForeignTaxCredit] = @ForeignTaxCredit
		  ,[InterestUS234D] = @InterestUS234D
		  ,[InterestUS220] = @InterestUS220
		  ,[RefundAdjusted] = @RefundAdjusted
		  ,[RegularAssessment] = @RegularAssessment
		  ,[RefundAlreadyReceived] = @RefundAlreadyReceived
		  ,[SelfAssessmentTaxDate] = @SelfAssessmentTaxDate		  
		  ,[RITotalIncome] = @RITotalIncome
		  ,[RISurcharge] = @RISurcharge
		  ,[RIEducationCess] = @RIEducationCess
		  ,[MATTotalIncome] = @MATTotalIncome
		  ,[MATSurcharge] = @MATSurcharge
		  ,[MATEducationCess] = @MATEducationCess
		  ,[AdvanceTax1installmentDate] = @AdvanceTax1installmentDate
		  ,[AdvanceTax2installmentDate] = @AdvanceTax2installmentDate
		  ,[AdvanceTax3installmentDate] = @AdvanceTax3installmentDate
		  ,[AdvanceTax4installmentDate] = @AdvanceTax4installmentDate
		  ,[RefundAdjustedDate] = @RefundAdjustedDate
		  ,[RegularAssessmentDate] = @RegularAssessmentDate
		  ,[RefundAlreadyReceivedDate] = @RefundAlreadyReceivedDate
		WHERE Id = @Id
		
		END

		IF @OPERATION = 'EXTENSION' AND @ITSubHeadId IS NOT NULL AND @ITSubHeadValue IS NOT NULL
		BEGIN

		with cte as
		(
			SELECT x.Rec.query('./Id').value('.', 'bigint') as Id,
				x.Rec.query('./ITReturnDetailsId').value('.', 'bigint') as ITReturnDetailsId,
				x.Rec.query('./ITSubHeadId').value('.', 'bigint') as ITSubHeadId,
				x.Rec.query('./ITSubHeadValue').value('.', 'DECIMAL (18, 2)') as ITSubHeadValue,
				x.Rec.query('./ITSubHeadDate').value('.', 'DATETIME') as ITSubHeadDate
			FROM @EXTENSIONDETAILS_XML.nodes('/ArrayOfITReturnDetailsExtension/ITReturnDetailsExtension') as x(Rec)
		)

		merge [dbo].[ITReturnDetailsExtension] as itrde
		using cte as cte1
		on (cte1.Id = itrde.Id)
		when matched then
		update set 
			itrde.ITSubHeadValue = cte1.ITSubHeadValue,
			itrde.ITSubHeadId = cte1.ITSubHeadId,
			itrde.ModifiedBy = @USER_ID,
			[ModifiedDate] = GETUTCDATE()
		when not matched then
		INSERT ( 
					   [ITReturnDetailsId]
					  ,[ITSubHeadId]
					  ,[ITSubHeadValue]
					  ,[ITSubHeadDate]
					  ,[Active]
					  ,[AddedBy]
					  ,[AddedDate]
					)
					values
					(	cte1.ITReturnDetailsId,
						cte1.ITSubHeadId,
						cte1.ITSubHeadValue,
						cte1.ITSubHeadDate,
						1,
						@USER_ID,
						GETUTCDATE());
		
		END

		SET @Result = 1;
		SET @ReturnMessage = 'Record updated successfully.'

	END


SELECT @Id as ITReturDetailsId, @Result AS Result, @ReturnMessage AS ReturnMessage

END











GO


