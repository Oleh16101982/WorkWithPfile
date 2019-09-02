declare views cursor for
	select 
		name 

	from 
		sysobjects so
	where 
		type='V'
		and name like 'V_%'
		and exists(select 1 from sysobjects where type='U' and substring(so.name,3,len(so.name)-2)=name and name not like 'sys%')

declare
		@viewname nvarchar(517),
		@script    varchar(5000)

open views
fetch from views into @viewname
while @@fetch_status=0 begin
	select @script='drop view '+@viewname
	exec(@script)
	fetch from views into @viewname
end

close views
deallocate views
	
	