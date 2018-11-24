USE [LitigationApp]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_SURCHARGEDATA_LIST]    Script Date: 8/27/2018 2:28:02 AM ******/
DROP PROCEDURE [dbo].[SP_GET_SURCHARGEDATA_LIST]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_SURCHARGEDATA_LIST]    Script Date: 8/27/2018 2:28:02 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[SP_GET_SURCHARGEDATA_LIST]
(	
	@SurchargeData_ID bigint = NULL,
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
	FAM.ID = SD.FYAYID
	ORDER BY FAM.[FinancialYear]
END



GO


