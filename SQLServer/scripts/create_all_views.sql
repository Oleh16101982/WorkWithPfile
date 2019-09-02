declare tables cursor for
	select name from 
			sysobjects 
	where 
			type='U' 
			and name not like '%_H'
			and name not like 'sys%'

declare @name varchar(255)
declare @script varchar(5000)

open tables
fetch from tables into @name
while @@fetch_status=0 begin
  select @script='create view V_'+@name+' as select * from '+@name
  if exists (select 1 from syscolumns where id=OBJECT_ID(@name) and name='enabled')
	select @script=@script+' where enabled=1'
  if not exists(select 1 from sysobjects where name='V_'+@name) begin
	 select @script	
	 exec (@script)
  end
  fetch from tables into @name
end

close tables
deallocate tables


