select * from [dbo].[ITReturnDetailsExtension]
where itreturndetailsid = 10021

delete from [dbo].[ITReturnDetailsExtension] where id = 9

declare @EXTENSIONDETAILS_XML as xml

begin

set @EXTENSIONDETAILS_XML = '<ArrayOfITReturnDetailsExtension>
	<ITReturnDetailsExtension>
		<Id>10021</Id>
		<Active>false</Active>
		<ITReturnDetailsId>0</ITReturnDetailsId>
		<ITSubHeadId>4</ITSubHeadId>
		<ITSubHeadValue>123456.0</ITSubHeadValue>
	</ITReturnDetailsExtension>
	<ITReturnDetailsExtension>
		<Id>10021</Id>
		<Active>false</Active>
		<ITReturnDetailsId>0</ITReturnDetailsId>
		<ITSubHeadId>3</ITSubHeadId>
		<ITSubHeadValue>67875.00</ITSubHeadValue>
	</ITReturnDetailsExtension>
</ArrayOfITReturnDetailsExtension>';

with cte as
(
SELECT x.Rec.query('./Id').value('.', 'bigint') as ITReturnID,
				x.Rec.query('./ITSubHeadId').value('.', 'bigint') as ITSubHeadID,
				x.Rec.query('./ITSubHeadValue').value('.', 'decimal') as ITSubHeadValue
			FROM @EXTENSIONDETAILS_XML.nodes('/ArrayOfITReturnDetailsExtension/ITReturnDetailsExtension') as x(Rec)
)

merge [dbo].[ITReturnDetailsExtension] as itrde
using cte as cte1
on (cte1.ITReturnID = itrde.itreturndetailsid and cte1.ITSubHeadID = itrde.ITSubHeadID)
when matched then
update set itrde.ITSubHeadValue = cte1.ITSubHeadValue
when not matched then
INSERT ( 
			   [ITReturnDetailsId]
			  ,[ITSubHeadId]
			  ,[ITSubHeadValue]
			  ,[Active]
			  ,[AddedBy]
			  ,[AddedDate]
			)
			values
			(	cte1.ITReturnID,
			    cte1.ITSubHeadID,
				cte1.ITSubHeadValue,
				1,
				1,
				GETUTCDATE());

end;
				
			