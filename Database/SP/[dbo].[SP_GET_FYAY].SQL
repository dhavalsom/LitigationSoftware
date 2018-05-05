USE [LitigationApp]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_FYAY]    Script Date: 5/5/2018 5:34:57 PM ******/
DROP PROCEDURE [dbo].[SP_GET_FYAY]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_FYAY]    Script Date: 5/5/2018 5:34:57 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GET_FYAY]
(	
	@FYAY_ID bigint = NULL,
	@ACTIVE BIT = 1
)
AS

BEGIN
		
	SELECT DISTINCT FAM.Id, FAM.[FinancialYear]
      ,FAM.[AssessmentYear]
      ,FAM.[IsDefault]
      ,FAM.[Active]
	FROM FYAYMaster FAM
	WHERE (@ACTIVE IS NULL OR FAM.[Active] = @ACTIVE) AND
	(@FYAY_ID IS NULL OR FAM.Id = @FYAY_ID)

END

GO


