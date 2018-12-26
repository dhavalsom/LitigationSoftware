USE [LitigationApp]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_STANDARDDATA_LIST]    Script Date: 12/19/2018 11:35:01 PM ******/
DROP PROCEDURE [dbo].[SP_GET_STANDARDDATA_LIST]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_STANDARDDATA_LIST]    Script Date: 12/19/2018 11:35:01 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [dbo].[SP_GET_STANDARDDATA_LIST]
(	
	@FYAY_ID bigint = NULL,
	@StandardData_ID bigint = NULL,
	@ACTIVE BIT = 1
)
AS

BEGIN
		
	SELECT DISTINCT SD.Id
	  ,SD.[FYAYID]
	  ,FAM.[FinancialYear]
      ,FAM.[AssessmentYear]
      ,SD.[BasicTaxRate]
	  ,SD.[MATRate]
	  ,SD.[EducationCess]
      ,SD.[Active]
	FROM [dbo].[StandardData] SD,[dbo].[FYAYMaster] FAM
	WHERE 
	(@ACTIVE IS NULL OR SD.[Active] = @ACTIVE) AND
	(@StandardData_ID IS NULL OR SD.Id = @StandardData_ID) AND
	(@FYAY_ID IS NULL OR SD.[FYAYID] = @FYAY_ID) AND
	FAM.ID = SD.FYAYID
	ORDER BY SD.[FYAYID]
END





GO


