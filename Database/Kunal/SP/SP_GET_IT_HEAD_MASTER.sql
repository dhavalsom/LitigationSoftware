USE [LitigationApp]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_IT_HEAD_MASTER]    Script Date: 9/9/2018 10:54:28 AM ******/
DROP PROCEDURE [dbo].[SP_GET_IT_HEAD_MASTER]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_IT_HEAD_MASTER]    Script Date: 9/9/2018 10:54:28 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[SP_GET_IT_HEAD_MASTER]
(
	@ACTIVE BIT = NULL,
	@PROPERTY_NAME NVARCHAR(100) =  NULL
)
AS

BEGIN

--EXEC [SP_GET_IT_HEAD_MASTER]
--EXEC [SP_GET_IT_HEAD_MASTER] 1,'HousePropIncome' 
	SELECT [Id]
      ,[ExcelSrNo]
      ,[Description]
      ,[PropertyName]
	  ,[CanAddSubHead]
	  ,[CanAddDocuments]
      ,[Active]
	FROM [ITHeadMaster]
	WHERE (@ACTIVE IS NULL OR [Active] = @ACTIVE)
	AND (@PROPERTY_NAME IS NULL OR [PropertyName] = @PROPERTY_NAME) 
	ORDER BY [Id]
END



GO


