update LitigationApp.dbo.CompanyCategoryMaster set CategoryDesc = 'Indian-Company/LLP/Partnership firm'
where CategoryDesc = 'Company/LLP/Partnership firm'

insert into LitigationApp.dbo.CompanyCategoryMaster
(CategoryDesc,IsDefault,Active,AddedBy,AddedDate)
values
('Foreign-Company/LLP/Partnership firm',1,1,1,GETUTCDATE())