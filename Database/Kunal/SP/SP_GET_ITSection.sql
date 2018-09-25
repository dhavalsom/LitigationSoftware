USE [LitigationApp]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_ITSection]    Script Date: 9/26/2018 12:30:57 AM ******/
DROP PROCEDURE [dbo].[SP_GET_ITSection]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_ITSection]    Script Date: 9/26/2018 12:30:57 AM ******/
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
	  ,ITSM.SectionCategoryId
	FROM [dbo].[ITSectionMaster] ITSM
	WHERE 
	[SECTIONCATEGORYID] = @ITSectionCategory_ID AND
	(@ACTIVE IS NULL OR ITSM.[Active] = @ACTIVE) AND
	(@ITSection_ID IS NULL OR ITSM.Id = @ITSection_ID)
	ORDER BY ITSM.[Description]
END





GO


