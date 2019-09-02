IF OBJECT_ID('tempdb..##table') IS NOT NULL drop table ##table

create table ##table (
	col_name	nvarchar(128),
	col_id		int,
	col_typename	nvarchar(128),
	col_len		int,
    col_prec         int                          NULL,
    col_scale        int                          NULL,
	col_null		bit,
	col_identity     bit)

declare tbl cursor for
	select 
		name 
	from 
		sysobjects so
	where 
		type='U'
		and name not like '%_H'
		and name not like 'sys%'
		and name<>'Log'
		and name<>'sp_caller'
		and not exists(select 1 from sysobjects where name=so.name+'_H')

declare
	@objid	int,
	@flags	int,
	@orderby nvarchar(10),
	@flags2 int,
	@tablename nvarchar(517),
	@script	 varchar(5000),
	@script_index	 varchar(5000),
	@col_name	nvarchar(128),
	@col_id		int,
	@col_typename	nvarchar(128),
	@col_len		int,
    @col_prec         int,
    @col_scale        int,
	@col_identity     bit,
	@col_null		  bit

select @flags = 0, @orderby  = null, @flags2 = 0 

open tbl
fetch from tbl into @tablename

while @@fetch_status=0 begin
	select @objid = object_id(@tablename)
    delete ##table
	insert ##table(col_name,col_id,col_typename,col_len,col_prec,col_scale,col_null,col_identity)
	select 
		c.name, 
		c.colid, 
		st.name as dtype,
        case when bt.name in (N'nchar', N'nvarchar') then c.length/2 else c.length end as leng,
		ColumnProperty(@objid, c.name, N'Precision') as pres,
		ColumnProperty(@objid, c.name, N'Scale') as scale,
		convert(bit, ColumnProperty(@objid, c.name, N'AllowsNull')) as col_nul,
		case when (@flags & 0x40000000 = 0) then convert(bit, ColumnProperty(@objid, c.name, N'IsIdentity')) else 0 end
	from 
		dbo.syscolumns c
				-- NonDRI Default and Rule filters
			left outer join (dbo.sysobjects d join sys.all_objects sysod on d.id = sysod.object_id)  on d.id = c.cdefault
			left outer join (dbo.sysobjects r join sys.all_objects sysor on r.id = sysor.object_id)  on r.id = c.domain
				-- Fully derived data type name
			join dbo.systypes st on st.xusertype = c.xusertype
				-- Physical base data type name
			join dbo.systypes bt on bt.xusertype = c.xtype
				-- DRIDefault text, if it's only one row.
			left outer join dbo.syscomments t on t.id = c.cdefault and t.colid = 1
					and not exists (select * from dbo.syscomments where id = c.cdefault and colid = 2)    
	where 
		c.id = @objid
	order by 
		c.colid
	select * from ##table
	declare colum cursor for
		select 
			col_name,
			col_id,
			col_typename,
			col_len,
			col_prec,
			col_scale,
			col_null,
			col_identity
		from
			##table
		
	select @script=' create table '+@tablename+'_H ( id int IDENTITY,'+CHAR(13)+'[Mode] [char](1),'+CHAR(13)
	select @script_index=''
    open colum
	fetch from colum into @col_name,@col_id,@col_typename,@col_len,@col_prec,@col_scale,@col_null,@col_identity
    while @@fetch_status=0 begin
		if @col_name='id' select @col_name=@tablename+'_id'
		select @script=@script+@col_name+' '+@col_typename
		if @col_len>0 and @col_typename in ('char','varchar','binary','varbinary','nchar','nvarchar','decimal','numeric') begin 
			select @script=@script+'('+cast(@col_len as varchar)
			if @col_typename in ('decimal','numeric') select @script=@script+','+cast(@col_scale as varchar)			
			select @script=@script+')'
		end
		if @col_null=1 select @script=@script+' NULL ' else
			select @script=@script+' NOT NULL '		
		if @col_identity=1 select @script_index='create index IND_'+@tablename+'_H on ' +@tablename+'_H ('+@col_name+')'
		if @col_name in ('created','changed') select @script=@script+' DEFAULT getdate()'
		if @col_name in ('creator','changer') select @script=@script+' DEFAULT suser_sname()'
		select @script=@script+','+CHAR(13)
		fetch from colum into @col_name,@col_id,@col_typename,@col_len,@col_prec,@col_scale,@col_null,@col_identity
	end
	select @script=@script+')'
	select @script
	select @script_index
	exec(@script)
	exec(@script_index)
	close colum
	deallocate colum
	fetch from tbl into @tablename
end
close tbl
deallocate tbl
