USE [LitigationApp]
GO

INSERT INTO [dbo].[ITHeadMaster]
           ([Description]
           ,[PropertyName]
           ,[CanAddSubHead]
           ,[Active]
           ,[AddedBy]
           ,[AddedDate]
           
           ,[CanAddDocuments]
           ,[IsROI]
           ,[HasDate]
           ,[IsSpecialIncomeEnabled])
     VALUES
           ( 'Refund Already Received','RefundAlreadyReceived',0,1,1,getdate(),0,0,1,0)