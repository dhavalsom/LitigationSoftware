USE [LitigationApp]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_SUB_DOCUMENT_CATEGORY_MASTER]    Script Date: 2/24/2019 1:02:22 AM ******/
DROP PROCEDURE [dbo].[SP_GET_SUB_DOCUMENT_CATEGORY_MASTER]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_SUB_DOCUMENT_CATEGORY_MASTER]    Script Date: 2/24/2019 1:02:22 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SP_GET_SUB_DOCUMENT_CATEGORY_MASTER]
(
	@ACTIVE BIT = NULL,
	@DOCUMENT_CATEGORY_ID BIGINT =  NULL
)
AS

BEGIN

--EXEC [SP_GET_SUB_DOCUMENT_CATEGORY_MASTER]
--EXEC [SP_GET_SUB_DOCUMENT_CATEGORY_MASTER] 1, 1 

	SELECT SDC.[Id]
      ,SDC.[DocumentCategoryId]
	  ,SDC.[Description]
      ,DC.[Description] AS DocumentCategoryName
      ,SDC.[Active]
	FROM [SubDocumentCategoryMaster] SDC
	INNER JOIN [DocumentCategoryMaster] DC on DC.Id = SDC.DocumentCategoryId
	WHERE (@ACTIVE IS NULL OR SDC.[Active] = @ACTIVE)
	AND (@DOCUMENT_CATEGORY_ID IS NULL OR SDC.DocumentCategoryId = @DOCUMENT_CATEGORY_ID) 
	ORDER BY [Id]
END






GO


