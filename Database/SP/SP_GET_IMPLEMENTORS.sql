USE [LitigationApp]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_IMPLEMENTORS]    Script Date: 5/1/2019 8:56:09 AM ******/
DROP PROCEDURE [dbo].[SP_GET_IMPLEMENTORS]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_IMPLEMENTORS]    Script Date: 5/1/2019 8:56:09 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SP_GET_IMPLEMENTORS]
(	
	@ImplementorId bigint = NULL,
	@ACTIVE BIT = 1
)
AS

BEGIN

	SELECT [Id]
      ,[Description]
      ,[Active]
      ,[AddedBy]
      ,[AddedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
      ,[DeletedBy]
      ,[DeletedDate]
  FROM [dbo].[ImplementorMaster] IM
  WHERE 
	(@ACTIVE IS NULL OR IM.[Active] = @ACTIVE) AND
	(@ImplementorId IS NULL OR IM.Id = @ImplementorId)
	ORDER BY IM.[Description]
END







GO


