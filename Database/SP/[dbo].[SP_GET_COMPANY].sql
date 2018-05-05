USE [LitigationApp]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_COMPANY]    Script Date: 4/8/2018 5:34:57 PM ******/
DROP PROCEDURE [dbo].[SP_GET_COMPANY]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_COMPANY]    Script Date: 4/8/2018 5:34:57 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GET_COMPANY]
(	
	@COMPANY_ID bigint = NULL,
	@ACTIVE BIT = 1
)
AS

BEGIN
		
	SELECT DISTINCT CM.Id, CM.CompanyName, CM.PANNumber, CM.IsDefault, CM.Active
	FROM CompanyMaster CM
	WHERE (@ACTIVE IS NULL OR CM.[Active] = @ACTIVE) AND
	(@COMPANY_ID IS NULL OR CM.Id = @COMPANY_ID)

END

GO


