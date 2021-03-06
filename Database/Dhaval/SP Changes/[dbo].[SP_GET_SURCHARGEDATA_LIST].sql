USE [LitigationApp]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_SURCHARGEDATA_LIST]    Script Date: 12/29/2018 1:15:24 PM ******/
DROP PROCEDURE [dbo].[SP_GET_SURCHARGEDATA_LIST]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_SURCHARGEDATA_LIST]    Script Date: 12/29/2018 1:15:24 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






CREATE PROCEDURE [dbo].[SP_GET_SURCHARGEDATA_LIST]
(	
	@FYAY_ID bigint = NULL,
	@SurchargeData_ID bigint = NULL,
	@entitycategorytypeid bigint = NULL,
	@ACTIVE BIT = 1
)
AS

BEGIN
		
	SELECT DISTINCT SD.Id
	  ,SD.[FYAYID]
	  ,FAM.[FinancialYear]
      ,FAM.[AssessmentYear]
      ,SD.[surchargefromthreshold]
	  ,SD.[surchargetothreshold]
	  ,SD.[surchargerate] 
	  ,SD.[entitycategorytypeid]
      ,SD.[Active]
	FROM [dbo].[SurchargeData] SD,[dbo].[FYAYMaster] FAM
	WHERE 
	(@ACTIVE IS NULL OR SD.[Active] = @ACTIVE) AND
	(@SurchargeData_ID IS NULL OR SD.Id = @SurchargeData_ID) AND
	(@FYAY_ID IS NULL OR SD.[FYAYID] = @FYAY_ID) AND
	(@entitycategorytypeid IS NULL OR SD.[entitycategorytypeid] = @entitycategorytypeid) AND
	FAM.ID = SD.FYAYID
	ORDER BY SD.[FYAYID]
END






GO


