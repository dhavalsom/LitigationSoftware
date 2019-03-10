USE [LitigationApp]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_RefundDetails]    Script Date: 3/10/2019 11:09:09 PM ******/
DROP PROCEDURE [dbo].[SP_GET_RefundDetails]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_RefundDetails]    Script Date: 3/10/2019 11:09:09 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SP_GET_RefundDetails]
(	
	@ITReturnDetailsID bigint = NULL,
	@ITHeadMasterID bigint = NULL,
	@FYAYID bigint = NULL,
	@ACTIVE BIT = 1
)
AS

BEGIN
		
	SELECT 
	  RD.id
	  ,RD.ITReturnDetailsID as ITReturnDetailsId
	  ,RD.ITHeadMasterID
      ,RD.[FYAYID]
      ,RD.[RefAmount]
	  ,RD.[RefDate]
      ,RD.[Active]
  FROM [LitigationApp].[dbo].[ITReturnDetails] ITRD,[LitigationApp].[dbo].[RefundDetails] RD
	WHERE 
	(@ITReturnDetailsID IS NULL OR ITRD.ID = @ITReturnDetailsID) AND
	(@FYAYID IS NULL OR RD.FYAYID = @FYAYID) AND
	(@ITHeadMasterID IS NULL OR RD.ITHeadMasterID = @ITHeadMasterID) AND
	(@ACTIVE IS NULL OR RD.Active = @ACTIVE) AND
	ITRD.ID = RD.ITReturnDetailsID 
	order by [ITHeadMasterID],[RefDate],[RefAmount]
END








GO


