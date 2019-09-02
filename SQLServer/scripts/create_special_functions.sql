GO
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[F_FormatDateDDMMYYYY]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
CREATE FUNCTION [dbo].[F_FormatDateDDMMYYYY] (@date datetime,@delimiter char(1))
RETURNS varchar(50) AS  
BEGIN 

declare @sValue varchar(50)

set	@sValue=substring(''0''+cast(datepart(day,@date) as varchar),len(cast(datepart(day,@date) as varchar)),2)+@delimiter+
		  substring(''0''+cast(datepart(month,@date) as varchar),len(cast(datepart(month,@date) as varchar)),2)+@delimiter
		+cast(datepart(year,@date) as varchar)	

RETURN @sValue
END

' 
END

GO
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[F_FormatDateS]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
CREATE FUNCTION [dbo].[F_FormatDateS] (@date datetime,@language char(2))
RETURNS varchar(50) AS  
BEGIN 

declare
	@sValue varchar(50),
	@month varchar(20),
	@year_s varchar(2)
set @month=''???????????''
set @year_s=''�.''

if lower(@language)=''ua''  begin
	set @month=
	case datepart(month,@date)
		when 1 then ''�_���''
		when 2 then ''������''
		when 3 then ''�������''
		when 4 then ''��_���''
		when 5 then ''������''
		when 6 then ''������''
		when 7 then ''�����''
		when 8 then ''������''
		when 9 then ''�������''
		when 10 then ''������''
		when 11 then ''���������''
		when 12 then ''������''
	end
	set @year_s=''�.''
end
	
if lower(@language)=''ru'' begin
	set @month=
	case datepart(month,@date)
		when 1 then ''������''
		when 2 then ''�������''
		when 3 then ''�����''
		when 4 then ''������''
		when 5 then ''���''
		when 6 then ''����''
		when 7 then ''����''
		when 8 then ''�������''
		when 9 then ''��������''
		when 10 then ''�������''
		when 11 then ''������''
		when 12 then ''�������''
	end
	set @year_s=''�.''
end


set	@sValue=''"''+substring(''0''+cast(datepart(day,@date) as varchar),len(cast(datepart(day,@date) as varchar)),2)+''" ''+
		@month+'' ''+
		cast(datepart(year,@date) as varchar) +@year_s

RETURN @sValue
END


' 
END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[F_FormatDateYYYYMMDD]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
CREATE FUNCTION [dbo].[F_FormatDateYYYYMMDD] (@date datetime,@delimiter char(1))
RETURNS varchar(50) AS  
BEGIN 

declare @sValue varchar(50)

set	@sValue=cast(datepart(year,@date) as varchar)+@delimiter+
		substring(''0''+cast(datepart(month,@date) as varchar),len(cast(datepart(month,@date) as varchar)),2)+@delimiter+
		substring(''0''+cast(datepart(day,@date) as varchar),len(cast(datepart(day,@date) as varchar)),2)

RETURN @sValue
END

' 
END

GO
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[F_FormatTimeHHMMSS]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
CREATE FUNCTION [dbo].[F_FormatTimeHHMMSS] (@time datetime,@delimiter char(1))
RETURNS varchar(50) AS  
BEGIN 

declare @sValue varchar(50)

set	@sValue=substring(''0''+cast(datepart(hour,@time) as varchar),len(cast(datepart(hour,@time) as varchar)),2)+@delimiter+
		  substring(''0''+cast(datepart(minute,@time) as varchar),len(cast(datepart(minute,@time) as varchar)),2)+@delimiter
		+substring(''0''+cast(datepart(second,@time) as varchar),len(cast(datepart(second,@time) as varchar)),2)

RETURN @sValue
END

' 
END

GO
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[F_InsSpaces]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
CREATE FUNCTION [dbo].[F_InsSpaces] (@count int)
RETURNS varchar(5000) AS  
BEGIN 

declare @sValue varchar(5000)
declare @i		int

set	@sValue=''''
select @i=0
while @i<@count 
   select @sValue=@sValue+'' '',@i=@i+1

RETURN @sValue
END

' 
END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[F_AlignNumeric]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'

CREATE FUNCTION [dbo].[F_AlignNumeric] (@n numeric(19,2),@len int,@alignment tinyint)
RETURNS varchar(5000) AS  
BEGIN 

declare @sValue varchar(5000)
declare @i		int

select @sValue=cast(cast(@n as decimal(19,2)) as varchar)

if len(@sValue)>@len begin
  if @alignment in (1,0) select @sValue=substring(@sValue,1,@len)
  else select @sValue=substring(@sValue,len(@sValue)-@len+1,@len)
end

while len(@sValue+''1'')<@len+1 begin
    if @alignment=0 select @sValue='' ''+@sValue+'' ''
	if @alignment=1 select @sValue=@sValue+'' ''
	if @alignment=2 select @sValue='' ''+@sValue
end


select @sValue=substring(@sValue,1,@len)

RETURN @sValue
END




' 
END

