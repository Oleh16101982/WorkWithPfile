
IF OBJECT_ID('tempdb..##table') IS NOT NULL drop table ##table

create table ##table (
	col_name	nvarchar(128),
	col_id		int,
	col_typename	nvarchar(128),
	col_len		int,
    col_prec         int                          NULL,
    col_scale        int                          NULL,
	col_null		bit,
	col_identity     bit,
	col_def_id      int)

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
--		and not exists(select 1 from sysobjects where name=so.name+'_H')
--		and name='Users'

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
	@ParamPart  varchar(8000),
	@FieldPart  varchar(8000),
	@ValuePart   varchar(8000),
	@EndPart	varchar(8000),
	@CheckPart  varchar(8000), 
	@DeletePart  varchar(8000), 
	@UpdatePart  varchar(8000), 
	@InsertPart  varchar(8000), 
	@ActionPart  varchar(8000), 
	@SetPart  varchar(8000), 
	@col_name	nvarchar(128),
	@col_id		int,
	@col_typename	nvarchar(128),
	@col_len		int,
    @col_prec         int,
    @col_scale        int,
	@col_identity     bit,
	@col_null		  bit,
	@col_defname      nvarchar(257),
	@col_def_id      int

select @CommonPart=
'	DECLARE @rowcount int;
	DECLARE @error int;
	DECLARE @Creator varchar(50);
	DECLARE @Created datetime;
	DECLARE @Changer varchar(50);
	DECLARE @Changed datetime;
	DECLARE @ErrorNumber int;
	DECLARE	@ErrorSeverity int;
	DECLARE	@ErrorState int;
	DECLARE @ErrorProcedure varchar(126);
	DECLARE	@ErrorMessage varchar(4000);
	DECLARE	@ErrorLine int;
	DECLARE @ErrorText varchar(500);
	CREATE TABLE #sp_Caller (id int)
	INSERT INTO #sp_Caller (id) VALUES (@@PROCID)
	'+CHAR(13)

select @flags = 0, @orderby  = null, @flags2 = 0 

open tbl
fetch from tbl into @tablename

while @@fetch_status=0 begin
   select @script_i='CREATE PROCEDURE [P_'+@tablename+'_I] '
	
   select @script_u='CREATE PROCEDURE [P_'+@tablename+'_U] '

   select @script_d='CREATE PROCEDURE [P_'+@tablename+'_D] '
    

	select @objid = object_id(@tablename)
    delete ##table
	insert ##table(col_name,col_id,col_typename,col_len,col_prec,col_scale,col_null,col_identity,col_def_id)
	select 
		c.name, 
		c.colid, 
		st.name as dtype,
        case when bt.name in (N'nchar', N'nvarchar') then c.length/2 else c.length end as leng,
		ColumnProperty(@objid, c.name, N'Precision') as pres,
		ColumnProperty(@objid, c.name, N'Scale') as scale,
		convert(bit, ColumnProperty(@objid, c.name, N'AllowsNull')) as col_nul,
		case when (@flags & 0x40000000 = 0) then convert(bit, ColumnProperty(@objid, c.name, N'IsIdentity')) else 0 end,
		case when (@flags & 0x0200 = 0 and c.cdefault is not null and (OBJECTPROPERTY(c.cdefault, N'IsDefaultCnst') <> 0))
					then t.id else null end
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
			col_identity,
			col_def_id
		from
			##table
		
	select @ParamPart=''
	select @FieldPart=''
	select @ValuePart=''
	select @SetPart=''
	open colum
	fetch from colum into @col_name,@col_id,@col_typename,@col_len,@col_prec,@col_scale,@col_null,@col_identity,@col_def_id
    while @@fetch_status=0 begin
		if @col_name in ('created','creator','changer','changed','stamp','id') goto L_next
		select @ParamPart=@Parampart+'     @'+@col_name+' '+@col_typename
		if @col_len>0 and @col_typename in ('char','varchar','binary','varbinary','nchar','nvarchar','decimal','numeric') begin 
			select @ParamPart=@ParamPart+'('+cast(@col_len as varchar)
			if @col_typename in ('decimal','numeric') select @ParamPart=@ParamPart+','+cast(@col_scale as varchar)			
			select @ParamPart=@ParamPart+')'
		end
		if @col_def_id is not null begin
			select @col_defname=text from syscomments where id=@col_def_id
			while substring(@col_defname,1,1)='(' 
				select @col_defname=substring(@col_defname,2,len(@col_defname)-2)
			select @ParamPart=@ParamPart+' = '+@col_defname
		end
		select @ParamPart=@ParamPart+','+CHAR(13) 
		select @FieldPart=@FieldPart+'       '+@col_name+','+CHAR(13)		
		select @ValuePart=@ValuePart+'       '+'@'+@col_name+','+CHAR(13)
		select @SetPart=@SetPart+'       '+@col_name+'='+'@'+@col_name+','+CHAR(13)
			
        L_next:
		fetch from colum into @col_name,@col_id,@col_typename,@col_len,@col_prec,@col_scale,@col_null,@col_identity,@col_def_id
	end
	close colum
	deallocate colum
	if len(@FieldPart)>2
	  select @FieldPart=substring(@FieldPart,1,len(@FieldPart)-2)+CHAR(13)
	if len(@ValuePart)>2
	  select @ValuePart=substring(@ValuePart,1,len(@ValuePart)-2)
	if len(@ParamPart)>2
	  select @ParamPart=substring(@ParamPart,1,len(@ParamPart)-2)+CHAR(13)
    if len(@SetPart)>2
	  select @SetPart=substring(@SetPart,1,len(@SetPart)-2)+CHAR(13)
	select @CheckPart='	IF dbo.F_CheckTriggers() = 1
				BEGIN
					SET @ErrorNumber = 50009
					SET @ErrorSeverity = 18
					SET @ErrorState = 1
					SET @ErrorMessage = ''User Error''
					SET @ErrorText = ''Error in F_CheckTriggers''
					set @ErrorProcedure = OBJECT_NAME(@@PROCID)
					EXECUTE p_Log_I @ErrorNumber , @ErrorSeverity , @ErrorState , @ErrorProcedure , @ErrorNumber , 0 , @ErrorText
					RAISERROR(@ErrorNumber , @ErrorSeverity , @ErrorState , @ErrorText)
					GOTO L_ERR
				END'

	select @DeletePart='DELETE FROM '+@tablename+' where id = @id'
	select @InsertPart='INSERT INTO '+@tablename+'('+CHAR(13)+@FieldPart+') values ('+CHAR(13)+@ValuePart+')'
	select @UpdatePart='UPDATE '+CHAR(13)+'   '+@tablename+CHAR(13)+'SET '+CHAR(13)+@SetPart+' where id = @id'

	select @EndPart='
	SELECT @id = @@identity , @rowcount = @@rowcount , @error = @@error
	COMMIT TRANSACTION
	SET @ErrorText = ''Success exec Procedure - '' + OBJECT_NAME(@@PROCID) + ''. RowCount - '' + cast(@rowcount as varchar(3))
	exec p_Log_I 0 , 0 , 0 , 0 , 0 , 0 , @ErrorText	
END TRY
BEGIN CATCH
	SELECT @id = @@identity , @rowcount = @@rowcount , @error = @@error
    SELECT 
         @ErrorNumber = ERROR_NUMBER(),
         @ErrorSeverity = ERROR_SEVERITY(),
         @ErrorState = ERROR_STATE(),
         @ErrorProcedure = ERROR_PROCEDURE(),
         @ErrorLine = ERROR_LINE(),
         @ErrorMessage = ERROR_MESSAGE()
	SELECT @ErrorText = ''Error in Procedure - ''+OBJECT_NAME ( @@ProcId) +''. RowCount - '' + cast(@rowcount as varchar(3))
	if @@trancount > 0
		ROLLBACK TRANSACTION
	exec p_Log_I @ErrorNumber , @ErrorSeverity , @ErrorState , @ErrorProcedure , @ErrorMessage , @ErrorLine , @ErrorText
	RAISERROR (@ErrorMessage , @ErrorSeverity , @ErrorState)
END CATCH
L_ERR:
-- select @id , @rowcount , @error
DROP TABLE #sp_Caller'
	

	select @script_i=@script_i+@ParamPart+' as '+CHAR(13)+'declare @id int'++CHAR(13)+@CommonPart+@checkPart+CHAR(13)+
	'BEGIN TRANSACTION
	 BEGIN TRY '+CHAR(13)+@INSERTPart +@EndPart
	
	select @script_u=@script_u+CHAR(13)+'     @id int, '+CHAR(13)+@ParamPart+' as '+CHAR(13)+@CommonPart+@checkPart+CHAR(13)+
	'BEGIN TRANSACTION
	 BEGIN TRY '+CHAR(13)+@UPDATEPart +@EndPart

	select @script_d=@script_d+' @id int as '+CHAR(13)+@CommonPart+@checkPart+CHAR(13)+
	'BEGIN TRANSACTION
	 BEGIN TRY '+CHAR(13)+@DeletePart +@EndPart

		
	if object_id('P_'+@tablename+'_I') is  NULL begin
		select @script_i
		exec(@script_i)
	end
	if (object_id('P_'+@tablename+'_U') is  NULL) and (@tablename<>'Log') begin
		select @script_u
		exec(@script_u)
	end
	if (object_id('P_'+@tablename+'_D') is  NULL) and (@tablename<>'Log') begin
		select @script_d
		exec(@script_d)
	end

fetch from tbl into @tablename
end
close tbl
deallocate tbl
drop table ##table
GO

