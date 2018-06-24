USE [LitigationApp]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_ITSection]    Script Date: 6/24/2018 10:39:00 PM ******/
DROP PROCEDURE [dbo].[SP_GET_ITSection]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_ITSection]    Script Date: 6/24/2018 10:39:00 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SP_GET_ITSection]
(	
	@ITSection_ID bigint = NULL,
	@ACTIVE BIT = 1
)
AS

BEGIN
		
	SELECT DISTINCT ITSM.Id
	  ,ITSM.[Description]
      ,ITSM.[IsDefault]
      ,ITSM.[Active]
	FROM [dbo].[ITSectionMaster] ITSM
	WHERE (@ACTIVE IS NULL OR ITSM.[Active] = @ACTIVE) AND
	(@ITSection_ID IS NULL OR ITSM.Id = @ITSection_ID)
	ORDER BY ITSM.[Description]
END


GO


