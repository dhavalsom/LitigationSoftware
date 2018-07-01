USE [LitigationApp]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_IT_SUB_HEAD_MASTER]    Script Date: 7/1/2018 8:22:54 PM ******/
DROP PROCEDURE [dbo].[SP_GET_IT_SUB_HEAD_MASTER]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_IT_SUB_HEAD_MASTER]    Script Date: 7/1/2018 8:22:54 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GET_IT_SUB_HEAD_MASTER]
(
	@ACTIVE BIT = NULL,
	@IT_HEAD_ID BIGINT =  NULL
)
AS

BEGIN

--EXEC [SP_GET_IT_SUB_HEAD_MASTER]
--EXEC [SP_GET_IT_SUB_HEAD_MASTER] 1, 1 

	SELECT SH.[Id]
      ,SH.[ITHeadId]
	  ,SH.[Description]
      ,H.[Description] AS ITHeadName
      ,SH.[IsAllowance]
      ,SH.[Active]
	FROM [ITSubHeadMaster] SH
	INNER JOIN [ITHeadMaster] H on H.Id = SH.ITHeadId
	WHERE (@ACTIVE IS NULL OR SH.[Active] = @ACTIVE)
	AND (@IT_HEAD_ID IS NULL OR SH.[ITHeadId] = @IT_HEAD_ID) 
	ORDER BY [Id]
END




GO


