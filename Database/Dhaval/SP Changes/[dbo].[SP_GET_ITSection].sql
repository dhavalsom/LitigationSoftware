USE [LitigationApp]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_ITSection]    Script Date: 8/27/2018 2:28:02 AM ******/
DROP PROCEDURE [dbo].[SP_GET_ITSection]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_ITSection]    Script Date: 8/27/2018 2:28:02 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[SP_GET_ITSection]
(	
	@ITSectionCategory_ID bigint,
	@ITSection_ID bigint = NULL,
	@ACTIVE BIT = 1
)
AS

BEGIN
		
	SELECT DISTINCT ITSM.Id
	  ,ITSM.[Description]
      ,ITSM.[IsDefault]
	  ,ISNULL(ITSM.[IsReturn],0) AS IsReturn
      ,ITSM.[Active]
	FROM [dbo].[ITSectionMaster] ITSM
	WHERE 
	[SECTIONCATEGORYID] = @ITSectionCategory_ID AND
	(@ACTIVE IS NULL OR ITSM.[Active] = @ACTIVE) AND
	(@ITSection_ID IS NULL OR ITSM.Id = @ITSection_ID)
	ORDER BY ITSM.[Description]
END



GO


