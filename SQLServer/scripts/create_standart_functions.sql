GO
/****** Object:  UserDefinedFunction [dbo].[F_CheckTriggers]    Script Date: 07/18/2006 13:46:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[F_CheckTriggers]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[F_CheckTriggers]
GO

declare trg cursor for
	select 
		name 
	from 
		sysobjects so
	where 
		type='TR'
		and (name like 'T_%_I'
		or name like 'T_%_U'
		or name like 'T_%_D')

declare
		@triggername nvarchar(517),
		@script    varchar(max),
		@beginPart varchar(max),
		@endPart varchar(max)

	select @BeginPart='CREATE FUNCTION [dbo].[F_CheckTriggers] ()
RETURNS tinyint
AS
BEGIN
	-- Declare the return variable here
	DECLARE @retVal tinyint
	-- Add the T-SQL statements to compute the return value here
IF ( '
select @script=''

open trg
fetch from trg into @triggername
while @@fetch_status=0 begin
	select @script=@script+' (OBJECT_ID ('''+@triggername+''',''TR'') IS NULL )
or '
	fetch from trg into @triggername
end

select @EndPart=' (1=2))
	BEGIN
		SET @retVal = 1
		GOTO L_retVal
	END
IF EXISTS (SELECT * FROM sys.triggers WHERE is_disabled = 1)
	SELECT @retVal = 1
ELSE
	SELECT @retVal = 0


	-- Return the result of the function
L_RetVal:
	RETURN @retVal
END
'

select @Script=@BeginPart+@script+@EndPart

select @script
exec(@script)
close trg
deallocate trg


