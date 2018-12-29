USE [LitigationApp]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_COMPANY]    Script Date: 12/28/2018 5:19:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--exec dbo.sp_get_company null,1


ALTER PROCEDURE [dbo].[SP_GET_COMPANY]
(	
	@COMPANY_ID bigint = NULL,
	@ACTIVE BIT = 1
)
AS

BEGIN
		
	SELECT DISTINCT CM.ID,CM.CompanyName, CM.PANNumber,CM.IsDefault,CM.Active,CM.CategoryID
	FROM CompanyMaster CM
	WHERE (@ACTIVE IS NULL OR CM.[Active] = @ACTIVE) AND
	(@COMPANY_ID IS NULL OR CM.Id = @COMPANY_ID)

END




GO


