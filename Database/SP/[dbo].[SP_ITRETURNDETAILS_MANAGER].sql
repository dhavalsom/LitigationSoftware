USE [LitigationApp]
GO

/****** Object:  StoredProcedure [dbo].[SP_ITRETURNDETAILS_MANAGER]    Script Date: 7/15/2018 3:01:57 PM ******/
DROP PROCEDURE [dbo].[SP_ITRETURNDETAILS_MANAGER]
GO

/****** Object:  StoredProcedure [dbo].[SP_ITRETURNDETAILS_MANAGER]    Script Date: 7/15/2018 3:01:57 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






CREATE PROCEDURE [dbo].[SP_ITRETURNDETAILS_MANAGER]
(
	@ITRETURNDETAILS_XML AS XML,
	@EXTENSIONDETAILS_XML AS XML,
	@USER_ID BIGINT = 1 --SET THE DEFAULT VALUE TO 1 IF NOT PASSED,
)
AS

BEGIN


/*BLOCK TO READ THE VARIABLES*/

DECLARE @Id AS BIGINT, @CompanyID AS BIGINT, @FYAYID AS BIGINT,@ITSectionID AS BIGINT,
        @ITReturnFillingDate AS DATETIME,@Active AS BIT,@ReturnMessage as NVARCHAR(MAX), @Result as BIT,
        @ITReturnDueDate AS DATETIME, @HousePropIncome AS DECIMAL (18, 2), @IncomefromCapGainsNonSTT AS DECIMAL (18, 2),
        @IncomefromCapGainsSTT AS DECIMAL (18, 2), @IncomefromBusinessProf AS BIT, @UnabsorbedDepreciation AS DECIMAL (18, 2),
        @Broughtforwardlosses AS DECIMAL (18, 2), @IncomeFromOtherSources AS DECIMAL (18, 2), @DeductChapterVIA AS DECIMAL (18, 2),
        @ProfitUS115JB AS DECIMAL (18, 2), @AdvanceTax1installment AS DECIMAL (18, 2), @AdvanceTax2installment AS DECIMAL (18, 2),
        @AdvanceTax3installment AS DECIMAL (18, 2), @AdvanceTax4installment AS DECIMAL (18, 2), @TDS AS DECIMAL (18, 2),
        @TCSPaidbyCompany AS DECIMAL (18, 2), @SelfassessmentTax AS DECIMAL (18, 2), @MATCredit AS DECIMAL (18, 2),
		@InterestUS234A AS DECIMAL (18, 2), @InterestUS234B AS DECIMAL (18, 2), @InterestUS234C AS DECIMAL (18, 2),
        @InterestUS244A AS DECIMAL (18, 2), @RefundReceived AS DECIMAL (18, 2), @RevisedReturnFile AS BIT,
		@ExtId AS BIGINT, @ITSubHeadId AS DECIMAL (18, 2), @ITSubHeadValue AS DECIMAL (18, 2),@IdentityVal AS BIGINT


SELECT	 @Id = ITReturnDetailsList.Columns.value('Id[1]', 'BIGINT')
	   , @CompanyID = ITReturnDetailsList.Columns.value('CompanyID[1]', 'BIGINT')
	   , @FYAYID = ITReturnDetailsList.Columns.value('FYAYID[1]', 'BIGINT')
	   , @ITSectionID = ITReturnDetailsList.Columns.value('ITSectionID[1]', 'BIGINT')   
	   , @ITReturnFillingDate = ITReturnDetailsList.Columns.value('ITReturnFillingDate[1]', 'DATETIME')
	   , @ITReturnDueDate = ITReturnDetailsList.Columns.value('ITReturnDueDate[1]', 'DATETIME')
	   , @HousePropIncome = ITReturnDetailsList.Columns.value('HousePropIncome[1]', 'DECIMAL (18, 2)')
	   , @IncomefromCapGainsNonSTT = ITReturnDetailsList.Columns.value('IncomefromCapGainsNonSTT[1]', 'DECIMAL (18, 2)')
	   , @IncomefromCapGainsSTT = ITReturnDetailsList.Columns.value('IncomefromCapGainsSTT[1]', 'DECIMAL (18, 2)')
	   , @IncomefromBusinessProf = ITReturnDetailsList.Columns.value('IncomefromBusinessProf[1]', 'BIT')
	   , @UnabsorbedDepreciation = ITReturnDetailsList.Columns.value('UnabsorbedDepreciation[1]', 'DECIMAL (18, 2)')
	   , @Broughtforwardlosses = ITReturnDetailsList.Columns.value('Broughtforwardlosses[1]', 'DECIMAL (18, 2)')
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
			SELECT 
				@IdentityVal,
				x.Rec.query('./ITSubHeadId').value('.', 'bigint'),
				x.Rec.query('./ITSubHeadValue').value('.', 'DECIMAL (18, 2)'),
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
		


		IF @ITSubHeadId IS NOT NULL AND @ITSubHeadValue IS NOT NULL
		BEGIN

		with cte as
		(
		SELECT x.Rec.query('./Id').value('.', 'bigint') as ITReturnID,
						x.Rec.query('./ITSubHeadId').value('.', 'bigint') as ITSubHeadID,
						x.Rec.query('./ITSubHeadValue').value('.', 'DECIMAL (18, 2)') as ITSubHeadValue
					FROM @EXTENSIONDETAILS_XML.nodes('/ArrayOfITReturnDetailsExtension/ITReturnDetailsExtension') as x(Rec)
		)

		merge [dbo].[ITReturnDetailsExtension] as itrde
		using cte as cte1
		on (cte1.ITReturnID = itrde.itreturndetailsid and cte1.ITSubHeadID = itrde.ITSubHeadID)
		when matched then
		update set itrde.ITSubHeadValue = cte1.ITSubHeadValue,itrde.ModifiedBy = @USER_ID,[ModifiedDate] = GETUTCDATE()
		when not matched then
		INSERT ( 
					   [ITReturnDetailsId]
					  ,[ITSubHeadId]
					  ,[ITSubHeadValue]
					  ,[Active]
					  ,[AddedBy]
					  ,[AddedDate]
					)
					values
					(	cte1.ITReturnID,
						cte1.ITSubHeadID,
						cte1.ITSubHeadValue,
						1,
						@USER_ID,
						GETUTCDATE());


		--declare @DocHandle int

		--EXEC sp_xml_preparedocument @DocHandle OUTPUT, @EXTENSIONDETAILS_XML 

		--UPDATE [dbo].[ITReturnDetailsExtension]
		--   SET [ITSubHeadValue] = XM.ITSubHeadValue
		--   ,[ModifiedBy] = @USER_ID
		--  ,[ModifiedDate] = GETUTCDATE()
		--   from OPENXML(@DocHandle,'/ArrayOfITReturnDetailsExtension/ITReturnDetailsExtension',2)
		--   WITH (ITSubHeadId bigINT,[ITSubHeadValue] DECIMAL (18, 2)) AS XM
		--   INNER JOIN [dbo].[ITReturnDetailsExtension] ITRDE ON ITRDE.ITSubHeadId = XM.ITSubHeadId
		--   AND ITRDE.ITReturnDetailsId = @Id
		
		--EXEC sp_xml_removedocument @DocHandle
		
		END

		

		SET @Result = 1;
		SET @ReturnMessage = 'Record updated successfully.'

	END


SELECT @Result AS Result, @ReturnMessage AS ReturnMessage

END




GO


