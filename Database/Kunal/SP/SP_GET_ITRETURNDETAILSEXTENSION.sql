USE [LitigationApp]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_ITRETURNDETAILSEXTENSION]    Script Date: 11/16/2018 7:59:18 PM ******/
DROP PROCEDURE [dbo].[SP_GET_ITRETURNDETAILSEXTENSION]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_ITRETURNDETAILSEXTENSION]    Script Date: 11/16/2018 7:59:18 PM ******/
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
--EXEC [SP_GET_ITRETURNDETAILSEXTENSION] 1, 20009 
--EXEC [SP_GET_ITRETURNDETAILSEXTENSION] 1, 30009 
	IF @ITRETURNDETAILS_ID IS NULL
	BEGIN

	SELECT ITRDE.[Id]
      ,ITRDE.ITReturnDetailsId
	  ,SH.ITHeadId
	  ,ITRDE.ITSubHeadId
      ,ITRDE.ITSubHeadValue
	  ,SH.IsAllowance
      ,ITRDE.Active
	FROM [ITReturnDetailsExtension] ITRDE
	LEFT OUTER JOIN ITSubHeadMaster SH ON SH.Id = ITRDE.ITSubHeadId
	WHERE (@ACTIVE IS NULL OR ITRDE.[Active] = @ACTIVE)
	ORDER BY ITRDE.[Id]

	END
	ELSE
	BEGIN
		SELECT ISNULL(ITRDE.[Id],0) [Id]
		  ,@ITRETURNDETAILS_ID AS ITReturnDetailsId
		  ,SH.ITHeadId
		  ,SH.Id AS ITSubHeadId
		  ,ISNULL(ITRDE.ITSubHeadValue,0) AS ITSubHeadValue
		  ,SH.IsAllowance
		  ,ISNULL(ITRDE.Active, CONVERT(BIT, 1)) AS Active
		FROM ITSubHeadMaster SH
		LEFT OUTER JOIN [ITReturnDetailsExtension] ITRDE ON ITRDE.ITSubHeadId = SH.Id AND ITRDE.ITReturnDetailsId = @ITRETURNDETAILS_ID
		ORDER BY ITRDE.[Id]
	END
END








GO


