declare trg cursor for
	select 
		name 
	from 
		sysobjects so
	where 
		type='TR'
		and (name like '%_I'
		or name like '%_U'
		or name like '%_D')

declare
		@triggername nvarchar(517),
		@script    varchar(5000)

open trg
fetch from trg into @triggername
while @@fetch_status=0 begin
	select @script='drop trigger '+@triggername
	exec(@script)
	fetch from trg into @triggername
end

close trg
deallocate trg
	