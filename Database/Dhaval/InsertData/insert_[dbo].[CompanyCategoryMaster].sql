/****** Script for SelectTopNRows command from SSMS  ******/
insert into [LitigationApp].[dbo].[CompanyCategoryMaster]
([CategoryDesc]
      ,[IsDefault]
      ,[Active]
      ,[AddedBy]
      ,[AddedDate]
 )
 values
 (
 'Individual',1,1,1,GETUTCDATE()
 ) 

insert into [LitigationApp].[dbo].[CompanyCategoryMaster]
([CategoryDesc]
      ,[IsDefault]
      ,[Active]
      ,[AddedBy]
      ,[AddedDate]
 )
 values
 (
 'Company/LLP/Partnership firm',1,1,1,GETUTCDATE()
 ) 