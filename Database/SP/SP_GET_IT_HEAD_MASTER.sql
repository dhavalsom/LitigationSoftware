USE [LitigationApp]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_IT_HEAD_MASTER]    Script Date: 2/24/2019 8:09:06 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO







ALTER PROCEDURE [dbo].[SP_GET_IT_HEAD_MASTER]
(	
	@IsTaxComputed BIT = NULL,
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
	  ,[IsROI]
	  ,[HasDate]
	  ,[IsSpecialIncomeEnabled]
      ,[Active]
	  ,[IsTaxComputed]
	FROM [ITHeadMaster]
	WHERE (@ACTIVE IS NULL OR [Active] = @ACTIVE)
	AND (@PROPERTY_NAME IS NULL OR [PropertyName] = @PROPERTY_NAME) 
	AND (@IsTaxComputed IS NULL OR IsTaxComputed = @IsTaxComputed)
	ORDER BY CAST([ExcelSrNo] AS BIGINT)
END







GO


