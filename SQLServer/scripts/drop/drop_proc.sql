declare prc cursor for
	select 
		name 
	from 
		sysobjects so
	where 
		type='P'
		and (name like '%_I'
		or name like '%_U'
		or name like '%_D')

declare
		@procname nvarchar(517),
		@script    varchar(5000)

open prc
fetch from prc into @procname
while @@fetch_status=0 begin
	select @script='drop procedure '+@procname
	exec(@script)
	fetch from prc into @procname
end

close prc
deallocate prc
	