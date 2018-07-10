USE [LitigationApp]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_ITRETURNDETAILSEXTENSION]    Script Date: 7/11/2018 4:38:11 AM ******/
DROP PROCEDURE [dbo].[SP_GET_ITRETURNDETAILSEXTENSION]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_ITRETURNDETAILSEXTENSION]    Script Date: 7/11/2018 4:38:11 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[SP_GET_ITRETURNDETAILSEXTENSION]
(
	@ACTIVE BIT = NULL,
	@ITRETURNDETAILS_ID BIGINT =  NULL
)
AS

BEGIN

--EXEC [SP_GET_ITRETURNDETAILSEXTENSION]
--EXEC [SP_GET_ITRETURNDETAILSEXTENSION] 1, 1 

	SELECT [Id]
      ,ITReturnDetailsId
	  ,ITSubHeadId
      ,ITSubHeadValue
      ,Active
	FROM [ITReturnDetailsExtension] ITRDE
	WHERE (@ACTIVE IS NULL OR ITRDE.[Active] = @ACTIVE)
	AND (@ITRETURNDETAILS_ID IS NULL OR ITRDE.ITReturnDetailsId = @ITRETURNDETAILS_ID) 
	ORDER BY [Id]
END






GO

