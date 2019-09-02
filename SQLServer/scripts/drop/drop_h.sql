declare tbl cursor for
	select 
		name 

	from 
		sysobjects so
	where 
		type='U'
		and name like '%_H'

declare
		@tablename nvarchar(517),
		@script    varchar(5000)

open tbl
fetch from tbl into @tablename
while @@fetch_status=0 begin
	select @script='drop table '+@tablename
	exec(@script)
	fetch from tbl into @tablename
end

close tbl
deallocate tbl
	
	