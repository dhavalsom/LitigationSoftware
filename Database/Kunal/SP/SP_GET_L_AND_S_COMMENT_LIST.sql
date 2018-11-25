USE [LitigationApp]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_L_AND_S_COMMENT_LIST]    Script Date: 11/25/2018 11:39:52 AM ******/
DROP PROCEDURE [dbo].[SP_GET_L_AND_S_COMMENT_LIST]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_L_AND_S_COMMENT_LIST]    Script Date: 11/25/2018 11:39:52 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SP_GET_L_AND_S_COMMENT_LIST]
(	
	@COMPANY_ID BIGINT = NULL,
	@IT_SUB_HEAD_ID BIGINT = NULL
)
AS

BEGIN

--EXEC [SP_GET_L_AND_S_COMMENT_LIST] 1, 1

SELECT LSC.Id
	, LSC.CompanyId
	, COMP.CompanyName
	, LSC.ITSubHeadId
	, SH.Description AS ITSubHeadDescription
	, LSC.Comment
	, LSC.Active
	, LSC.AddedBy
	, LSC.AddedDate
	, LSC.ModifiedBy
	, LSC.ModifiedDate	
	FROM [LAndSComments] LSC
	INNER JOIN ITSubHeadMaster SH ON SH.Id = LSC.ITSubHeadId
	INNER JOIN CompanyMaster COMP ON COMP.Id = LSC.CompanyId
	WHERE LSC.Active = 1
	AND (@COMPANY_ID IS NULL OR LSC.CompanyId = @COMPANY_ID)
	AND (@IT_SUB_HEAD_ID IS NULL OR LSC.ITSubHeadId = @IT_SUB_HEAD_ID)
	ORDER BY  COMP.CompanyName, SH.Description, LSC.AddedDate DESC
END





GO


