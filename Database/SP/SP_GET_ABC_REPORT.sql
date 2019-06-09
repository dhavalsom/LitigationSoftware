USE [LitigationApp]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_ABC_REPORT]    Script Date: 20-05-2019 21:47:58 ******/
DROP PROCEDURE [dbo].[SP_GET_ABC_REPORT]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_ABC_REPORT]    Script Date: 20-05-2019 21:47:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GET_ABC_REPORT]
(	
	@COMPANY_ID BIGINT,
	@IS_ALLOWANCE BIT = NULL
)
AS

BEGIN
	--EXEC [SP_GET_ABC_REPORT] 1
	SELECT ITSM.Id ITSubHeadId, 
	   ITSM.[Description] AS [ITSubHead], 
	   ITSM.ITHeadId, 
	   ITHM.[Description] AS [ITHead], 
	   ITSM.SubHeadType,
	   ITSM.IsAllowance,
	   SUM(ISNULL(ITDE.ITSubHeadValue, 0)) AS Total,
	   CM.CompanyName,
	   ITR.CompanyId 
	FROM ITSubHeadMaster ITSM
	INNER JOIN ITHeadMaster ITHM ON ITHM.Id = ITSM.ITHeadId
	LEFT OUTER JOIN ITReturnDetailsExtension ITDE ON ITDE.ITSubHeadId = ITSM.Id
	INNER JOIN ITReturnDetails ITR ON ITR.Id = ITDE.ITReturnDetailsId
	INNER JOIN CompanyMaster CM ON CM.Id = ITR.CompanyId
	GROUP BY ITSM.Id, ITSM.ITHeadId, ITHM.[Description], ITSM.[Description], ITSM.SubHeadType, ITSM.IsAllowance,
	CM.CompanyName, ITR.CompanyId
	HAVING (@COMPANY_ID IS NULL OR ITR.CompanyId = @COMPANY_ID)
	AND (@IS_ALLOWANCE IS NULL OR ITSM.IsAllowance = @IS_ALLOWANCE)

END





GO


