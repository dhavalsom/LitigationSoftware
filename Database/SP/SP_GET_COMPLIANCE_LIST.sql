USE [LitigationApp]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_COMPLIANCE_LIST]    Script Date: 7/4/2018 8:42:05 PM ******/
DROP PROCEDURE [dbo].[SP_GET_COMPLIANCE_LIST]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_COMPLIANCE_LIST]    Script Date: 7/4/2018 8:42:05 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[SP_GET_COMPLIANCE_LIST]
(	
	@COMPLIANCE_ID bigint = NULL,
	@ACTIVE BIT = 1
)
AS

--exec [SP_GET_COMPLIANCE_LIST] 
--exec [SP_GET_COMPLIANCE_LIST] 1
BEGIN
		
	SELECT DISTINCT CM.Id
	  ,CM.[Description]
      ,CM.[SrNo]
      ,CM.[Active]
	FROM [dbo].[ComplianceMaster] CM
	WHERE (@ACTIVE IS NULL OR CM.[Active] = @ACTIVE) AND
	(@COMPLIANCE_ID IS NULL OR CM.Id = @COMPLIANCE_ID)
	ORDER BY CM.[SrNo]
END



GO


