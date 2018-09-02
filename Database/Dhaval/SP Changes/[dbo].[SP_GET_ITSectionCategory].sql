USE [LitigationApp]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_ITSectionCategory]    Script Date: 8/26/2018 11:51:59 PM ******/
DROP PROCEDURE [dbo].[SP_GET_ITSectionCategory]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_ITSectionCategory]    Script Date: 8/26/2018 11:51:59 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[SP_GET_ITSectionCategory]
(	
	@ITSectionCategory_ID bigint = NULL,
	@ACTIVE BIT = 1
)
AS

BEGIN
		
	SELECT DISTINCT ITSC.Id
	  ,ITSC.[CategoryDesc]
      ,ITSC.[IsDefault]
      ,ITSC.[Active]
	FROM [dbo].[ITSectionCategory] ITSC
	WHERE (@ACTIVE IS NULL OR ITSC.[Active] = @ACTIVE) AND
	(@ITSectionCategory_ID IS NULL OR ITSC.Id = @ITSectionCategory_ID)
	ORDER BY ITSC.[CategoryDesc]
END



GO


