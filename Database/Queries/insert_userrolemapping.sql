/****** Script for SelectTopNRows command from SSMS  ******/
insert into [LitigationApp].[dbo].[UserRoleMapping]
([UserId]
      ,[RoleId]
      ,[IsDefault]
      ,[Active]
      ,[AddedBy]
      ,[AddedDate]
 )
 values
 (1,1,1,1,1,getdate() )