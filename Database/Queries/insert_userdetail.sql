insert into [LitigationApp].[dbo].[UserDetail]
([FirstName]
      ,[LastName]
      ,[EmailAddress]
      ,[PhoneNumber]
      ,[Gender]
      ,[DOB]
      ,[Password]
      ,[Active]
      ,[AddedBy]
      ,[AddedDate]
)  values
(
'Administrator','Administrator','dhavalsom@gmail.com',+919920898802,1,08/28/1982,'admin',1,1,getdate()
)