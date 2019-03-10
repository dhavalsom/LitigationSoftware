USE [LitigationApp]
GO

/****** Object:  StoredProcedure [dbo].[SP_REFUNDDETAILS_MANAGER]    Script Date: 3/10/2019 11:18:23 PM ******/
DROP PROCEDURE [dbo].[SP_REFUNDDETAILS_MANAGER]
GO

/****** Object:  StoredProcedure [dbo].[SP_REFUNDDETAILS_MANAGER]    Script Date: 3/10/2019 11:18:23 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SP_REFUNDDETAILS_MANAGER]
(
	@REFUNDDETAILS_XML AS XML,
	@USER_ID BIGINT = 1 --SET THE DEFAULT VALUE TO 1 IF NOT PASSED,
)
AS

BEGIN


/*BLOCK TO READ THE VARIABLES*/

DECLARE @Id AS BIGINT, @ITReturnDetailsID AS BIGINT, @FYAYID AS BIGINT,@ITHeadMasterID AS BIGINT,
        @RefDate AS DATETIME,@Active AS BIT,@ReturnMessage as NVARCHAR(MAX), @Result as BIT,
        @RefAmount AS DECIMAL (18, 2)
        

SELECT	 @Id = RefundDetailsList.Columns.value('Id[1]', 'BIGINT')
	   , @ITReturnDetailsID = RefundDetailsList.Columns.value('ITReturnDetailsID[1]', 'BIGINT')
	   , @FYAYID = RefundDetailsList.Columns.value('FYAYID[1]', 'BIGINT')
	   , @ITHeadMasterID = RefundDetailsList.Columns.value('ITHeadMasterID[1]', 'BIGINT')   
	   , @RefDate = RefundDetailsList.Columns.value('RefDate[1]', 'DATETIME')
	   , @RefAmount = RefundDetailsList.Columns.value('RefAmount[1]', 'DECIMAL (18, 2)')
	   , @Active = RefundDetailsList.Columns.value('Active[1]', 'BIT')
FROM   @REFUNDDETAILS_XML.nodes('RefundDetails') AS RefundDetailsList(Columns)


/*BLOCK TO READ THE VARIABLES ENDS HERE*/


	IF @Id IS NULL OR @Id = 0 
	BEGIN

	INSERT INTO [dbo].[RefundDetails]
      (ITReturnDetailsID
      ,FYAYID
      ,ITHeadMasterID
      ,RefDate
      ,RefAmount
      ,[Active]
      ,[AddedBy]
      ,[AddedDate])
     VALUES
	(@ITReturnDetailsID
     ,@FYAYID 
     ,@ITHeadMasterID
     ,@RefDate
     ,@RefAmount 
     ,@Active
	 ,@USER_ID
	 ,GETUTCDATE())

	set @Id = @@IDENTITY

	SET @Result = 1;
	SET @ReturnMessage = 'Record created successfully.'

	END

	ELSE
	BEGIN
		UPDATE [dbo].[RefundDetails]
		   SET 
		   ITReturnDetailsID = @ITReturnDetailsID
		  ,[FYAYID] = @FYAYID
		  ,ITHeadMasterID = @ITHeadMasterID
		  ,RefDate = @RefDate
		  ,RefAmount = @RefAmount
		  ,Active = @Active
		  ,[ModifiedBy] = @USER_ID
		  ,[ModifiedDate] = GETUTCDATE()
		WHERE Id = @Id
		
		SET @Result = 1;
		SET @ReturnMessage = 'Record updated successfully.'

	END


SELECT @Id as RefundDetailsId, @Result AS Result, @ReturnMessage AS ReturnMessage

END


GO


