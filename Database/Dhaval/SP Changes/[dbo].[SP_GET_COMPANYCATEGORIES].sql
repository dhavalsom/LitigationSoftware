USE [LitigationApp]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_COMPANY]    Script Date: 4/8/2018 5:34:57 PM ******/
DROP PROCEDURE [dbo].[SP_GET_COMPANYCATEGORIES]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_COMPANYCATEGORIES]    Script Date: 4/8/2018 5:34:57 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GET_COMPANYCATEGORIES]
(	
	@ACTIVE BIT = 1
)
AS

BEGIN
		
	SELECT DISTINCT CCM.Id, CCM.CategoryDesc, CCM.IsDefault, CCM.Active
	FROM [CompanyCategoryMaster] CCM
	WHERE (@ACTIVE IS NULL OR CCM.[Active] = @ACTIVE) 

END

GO


