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
--		and name not like '%_H'
		and name not like 'sys%'
--		and name<>'Log'
		and name<>'sp_caller'
--		and not exists(select 1 from sysobjects where name=so.name+'_H')
--		and name='Payments'

declare
	@objid	int,
	@flags	int,
	@orderby nvarchar(10),
	@flags2 int,
	@tablename nvarchar(517),
	@script_i	 varchar(8000),
	@script_d	 varchar(8000),
	@script_u	 varchar(8000),
    @Commonpart  varchar(8000),
	@DeclarePart  varchar(8000),
	@SelectPart  varchar(8000),
	@InsertPart  varchar(8000),
	@ValuePart   varchar(8000),
	@EndPart	varchar(8000),
	@col_name	nvarchar(128),
	@col_name_h	nvarchar(128),
	@col_id		int,
	@col_typename	nvarchar(128),
	@col_len		int,
    @col_prec         int,
    @col_scale        int,
	@col_identity     bit,
	@col_null		  bit

select @CommonPart=
'		DECLARE @id int; 
		DECLARE @rowcount int;
		DECLARE @error int;
		DECLARE @ProcId int
		DECLARE @ProcName varchar(126)
		DECLARE @UserErrNumber int
		DECLARE @UserErrSeverity int
		DECLARE @UserErrState int
		DECLARE @UserErrMsg varchar(500)
		DECLARE @UserErrText varchar(500)
		DECLARE @Mode char(1) 
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	IF OBJECT_ID(''tempdb..#sp_Caller'') IS NULL
	BEGIN
		SET @UserErrNumber = 50001
		SET @UserErrSeverity = 18
		SET @UserErrState = 1
		SET @UserErrMsg = ''User Error''
		SET @UserErrText = ''No temp table present''
		GOTO L_ERR
	END
	SELECT @ProcId = id from #sp_Caller
	SELECT @id = @@identity , @rowcount = @@rowcount , @error = @@error	
	IF (@rowcount = 0 ) or (@error != 0)
	BEGIN
		SET @UserErrNumber = 50002
		SET @UserErrSeverity = 18
		SET @UserErrState = 1
		SET @UserErrMsg = ''User Error''
		SET @UserErrText = ''Error define ProcID''
		GOTO L_ERR
	END
	SELECT @ProcName = OBJECT_NAME(@ProcID)
	IF @ProcName is NULL
	BEGIN
		SET @UserErrNumber = 50003
		SET @UserErrSeverity = 18
		SET @UserErrState = 1
		SET @UserErrMsg = ''User Error''
		SET @UserErrText = ''Error define ProcName''
		GOTO L_ERR
	END'+CHAR(13)

select @flags = 0, @orderby  = null, @flags2 = 0 

open tbl
fetch from tbl into @tablename

while @@fetch_status=0 begin
   select @script_i='CREATE TRIGGER [T_'+@tablename+'_I] 
    ON  [dbo].['+@tablename+'] 
    AFTER INSERT AS BEGIN '+CHAR(13)+ @CommonPart
	if (@tablename not like '%_H') and @tablename<>'Log'
		select @script_i=@script_i+ 
		'IF @ProcName != ''P_'+@tablename+'_I''
		BEGIN
			SET @UserErrNumber = 50004
			SET @UserErrSeverity = 18
			SET @UserErrState = 1
			SET @UserErrMsg = ''User Error''
			SET @UserErrText = ''Uncorrect Procedure for INSERT into '+@tablename+'''
			GOTO L_ERR
		END
		SET @Mode = ''I'''+CHAR(13)
	else
		if @tablename<>'Log'
		select @script_i=@script_i+
		'IF @ProcName not like ''P[_]'+substring(@tablename,1,len(@tablename)-2)+'[_][IUD]''
		BEGIN
			SET @UserErrNumber = 50004
			SET @UserErrSeverity = 18
			SET @UserErrState = 1
			SET @UserErrMsg = ''User Error''
			SET @UserErrText = ''Uncorrect Procedure for INSERT into '+@tablename+'''
			GOTO L_ERR
		END'
	
   select @script_u='CREATE TRIGGER [T_'+@tablename+'_U] 
    ON  [dbo].['+@tablename+'] 
    AFTER UPDATE AS BEGIN' +CHAR(13)+ @CommonPart
	if @tablename not like '%_H'
	select @script_u=@script_u+
	'IF @ProcName != ''P_'+@tablename+'_U''
	BEGIN
		SET @UserErrNumber = 50004
		SET @UserErrSeverity = 18
		SET @UserErrState = 1
		SET @UserErrMsg = ''User Error''
		SET @UserErrText = ''Uncorrect Procedure for UPDATE into '+@tablename+'''
		GOTO L_ERR
	END
	SET @Mode = ''U'''+CHAR(13)

   select @script_d='CREATE TRIGGER [T_'+@tablename+'_D] 
    ON  [dbo].['+@tablename+'] 
    AFTER DELETE AS BEGIN' +CHAR(13)+ @CommonPart+CHAR(13)
	if @tablename not like '%_H'
	select @script_d=@script_d+
	'IF @ProcName != ''P_'+@tablename+'_D''
	BEGIN
		SET @UserErrNumber = 50004
		SET @UserErrSeverity = 18
		SET @UserErrState = 1
		SET @UserErrMsg = ''User Error''
		SET @UserErrText = ''Uncorrect Procedure for DELETE into '+@tablename+'''
		GOTO L_ERR
	END
	SET @Mode = ''D'''+CHAR(13)

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
		
	select @DeclarePart=''
	select @SelectPart=CHAR(13)+'    select '+CHAR(13)
	select @ValuePart='VALUES ( @Mode,'+CHAR(13)
	select @InsertPart='if @@rowcount>0 INSERT INTO '+@tablename+'_H( Mode,'+CHAR(13)
	open colum
	fetch from colum into @col_name,@col_id,@col_typename,@col_len,@col_prec,@col_scale,@col_null,@col_identity
    while @@fetch_status=0 begin
		if @col_name in ('created','creator','changer','changed','stamp') goto L_next
		if @col_name='id' select @col_name_h=@tablename+'_id' else select @col_name_h=@col_name
		select @DeclarePart=@Declarepart+'    DECLARE @'+@col_name_h+' '+@col_typename
		if @col_len>0 and @col_typename in ('char','varchar','binary','varbinary','nchar','nvarchar','decimal','numeric') begin 
			select @DeclarePart=@DeclarePart+'('+cast(@col_len as varchar)
			if @col_typename in ('decimal','numeric') select @DeclarePart=@DeclarePart+','+cast(@col_scale as varchar)			
			select @DeclarePart=@DeclarePart+')'
		end
		select @DeclarePart=@declarePart+CHAR(13) 
		select @SelectPart='       '+@SelectPart+'@'+@col_name_h+'='+@col_name+','+CHAR(13)		
		select @InsertPart=@InsertPart+'         '+@col_name_h+','+CHAR(13)
		select @ValuePart=@ValuePart+'         @'+@col_name_h+','+CHAR(13)
			
        L_next:
		fetch from colum into @col_name,@col_id,@col_typename,@col_len,@col_prec,@col_scale,@col_null,@col_identity
	end
	close colum
	deallocate colum
	select @SelectPart=substring(@SelectPart,1,len(@SelectPart)-2)+CHAR(13)+' from '
	select @ValuePart=substring(@ValuePart,1,len(@ValuePart)-2)+')'
	select @InsertPart=substring(@InsertPart,1,len(@InsertPart)-2)+')'

	if (@tablename not in ('Log')) and (@tablename not like '%_H')
	select @script_i=@script_i+@DeclarePart+@SelectPart+' inserted '+CHAR(13)+
						@InsertPart+@ValuePart
	
	if ((@tablename like '%_H') or (@tablename in ('XXX'))) --“аблицы в которых запрет на изменение
		select @script_u=@script_u+'
					SET @UserErrNumber = 50007
					SET @UserErrSeverity = 18
					SET @UserErrState = 1
					SET @UserErrMsg = ''User Error''
					SET @UserErrText = ''Impossible update table '+@tablename+
		''' 
					GOTO L_ERR'
	else
	select @script_u=@script_u+@DeclarePart+@SelectPart+' inserted '+CHAR(13)+
						@InsertPart+@ValuePart
	if ((@tablename like '%_H') or (@tablename in ('Payments','Log'))) --“аблицы в которых запрет на удаление
	select @script_d=@script_d+'
		SET @UserErrNumber = 50005
		SET @UserErrSeverity = 18
		SET @UserErrState = 1
		SET @UserErrMsg = ''User Error''
		SET @UserErrText = ''Impossible delete this records in table '+@tablename+
		''' 
		GOTO L_ERR'
	else
	select @script_d=@script_d+@DeclarePart+@SelectPart+' deleted '+CHAR(13)+
						@InsertPart+@ValuePart

	select @script_i=@script_i+	'
	GOTO L_OK
		L_ERR:
		EXECUTE p_Log_I @UserErrNumber , @UserErrSeverity , @UserErrState , ''Trigger - T_'+@tablename+'_I'' , @UserErrMsg , 0 , @UserErrText
		RAISERROR(@UserErrNumber , @UserErrSeverity , @UserErrState , @UserErrText)
	END
	L_OK:'

	select @script_u=@script_u+'
	--	Update the special fields
	update	u
	set	u.Changed	= GetDate()	,
		u.Changer	= SUSER_SNAME()
	from	'+@tablename+' u INNER JOIN INSERTED i
	ON	( 1 = 1 )	-- Remove the extra "and" clause
	and	u.id	= i.id

	GOTO L_OK
		L_ERR:
		EXECUTE p_Log_I @UserErrNumber , @UserErrSeverity , @UserErrState , ''Trigger - T_'+@tablename+'_U'' , @UserErrMsg , 0 , @UserErrText
		RAISERROR(@UserErrNumber , @UserErrSeverity , @UserErrState , @UserErrText)
	END
	L_OK:'
	select @script_d=@script_d+'
	GOTO L_OK
		L_ERR:
		EXECUTE p_Log_I @UserErrNumber , @UserErrSeverity , @UserErrState , ''Trigger - T_'+@tablename+'_D'' , @UserErrMsg , 0 , @UserErrText
		RAISERROR(@UserErrNumber , @UserErrSeverity , @UserErrState , @UserErrText)
	END
	L_OK:'
	
	if (object_id('T_'+@tablename+'_I') is  NULL) begin
		select @script_i
		exec(@script_i)
	end
	if (object_id('T_'+@tablename+'_U') is  NULL) and (@tablename<>'Log') begin
		select @script_u
		exec(@script_u)
	end
	if object_id('T_'+@tablename+'_D') is  NULL begin
		select @script_d
		exec(@script_d)
	end

fetch from tbl into @tablename
end
close tbl
deallocate tbl
