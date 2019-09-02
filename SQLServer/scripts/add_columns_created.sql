declare tbl cursor for
	select 
		id,
		name 
	from 
		sysobjects so
	where 
		type='U'
		and name not like '%_H'
		and name not like 'sys%'

declare
		@tablename nvarchar(517),
		@script    varchar(5000),
		@id			int

open tbl
fetch from tbl into @id, @tablename
while @@fetch_status=0 begin
	select @script=' alter table '+@tablename+' add created datetime not null default getdate() '
	if not exists (select 1 from syscolumns where id=@id and name='created') begin
		select @script	
    	exec(@script)
	end
	select @script=' alter table '+@tablename+' add creator varchar(50) not null default suser_sname() '
	if not exists (select 1 from syscolumns where id=@id and name='creator') begin
		select @script	
    	exec(@script)
	end
	select @script=' alter table '+@tablename+' add changed datetime not null default getdate() '
	if not exists (select 1 from syscolumns where id=@id and name='changed') begin
		select @script	
    	exec(@script)
	end
	select @script=' alter table '+@tablename+' add changer varchar(50) not null default suser_sname()'
	if not exists (select 1 from syscolumns where id=@id and name='changer') begin
		select @script	
    	exec(@script)
	end
	select @script=' alter table '+@tablename+' add stamp timestamp not null '
	if not exists (select 1 from syscolumns where id=@id and name='stamp') begin
		select @script	
    	exec(@script)
	end
	fetch from tbl into @id,@tablename
end

close tbl
deallocate tbl
	