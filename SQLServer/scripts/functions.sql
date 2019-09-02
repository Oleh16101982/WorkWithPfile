GO
/****** Object:  UserDefinedFunction [dbo].[F_CheckTriggers]    Script Date: 07/18/2006 13:46:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[F_CheckTriggers]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[F_CheckTriggers]
GO
/****** Object:  UserDefinedFunction [dbo].[F_CheckTriggers]    Script Date: 07/18/2006 13:34:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[F_CheckTriggers] ()
RETURNS tinyint
AS
BEGIN
	-- Declare the return variable here
	DECLARE @retVal tinyint
	-- Add the T-SQL statements to compute the return value here
IF (
(OBJECT_ID ('T_RefOperationType_D','TR') IS NULL )
or
(OBJECT_ID ('T_RefCurrency_I','TR') IS NULL )
or
(OBJECT_ID ('T_RefCurrency_U','TR') IS NULL )
or
(OBJECT_ID ('T_RefCurrency_D','TR') IS NULL )
or
(OBJECT_ID ('T_RefObjects_I','TR') IS NULL )
or
(OBJECT_ID ('T_RefObjects_U','TR') IS NULL )
or
(OBJECT_ID ('T_RefObjects_D','TR') IS NULL )
or
(OBJECT_ID ('T_RefDecodeBill_I','TR') IS NULL )
or
(OBJECT_ID ('T_RefDecodeBill_U','TR') IS NULL )
or
(OBJECT_ID ('T_RefDecodeBill_D','TR') IS NULL )
or
(OBJECT_ID ('T_RefDenomination_I','TR') IS NULL )
or
(OBJECT_ID ('T_RefDenomination_U','TR') IS NULL )
or
(OBJECT_ID ('T_RefDenomination_D','TR') IS NULL )
or
(OBJECT_ID ('T_ParkomatOption_I','TR') IS NULL )
or
(OBJECT_ID ('T_ParkomatOption_U','TR') IS NULL )
or
(OBJECT_ID ('T_ParkomatOption_D','TR') IS NULL )
or
(OBJECT_ID ('T_Log_I','TR') IS NULL )
or
(OBJECT_ID ('T_Log_D','TR') IS NULL )
or
(OBJECT_ID ('T_RefParkomats_I','TR') IS NULL )
or
(OBJECT_ID ('T_RefParkomats_U','TR') IS NULL )
or
(OBJECT_ID ('T_RefParkomats_D','TR') IS NULL )
or
(OBJECT_ID ('T_RefTimesWork_I','TR') IS NULL )
or
(OBJECT_ID ('T_RefTimesWork_U','TR') IS NULL )
or
(OBJECT_ID ('T_RefTimesWork_D','TR') IS NULL )
or
(OBJECT_ID ('T_RefParkomatClasses_H_I','TR') IS NULL )
or
(OBJECT_ID ('T_RefParkomatClasses_H_U','TR') IS NULL )
or
(OBJECT_ID ('T_RefParkomatClasses_H_D','TR') IS NULL )
or
(OBJECT_ID ('T_Payments_I','TR') IS NULL )
or
(OBJECT_ID ('T_Payments_U','TR') IS NULL )
or
(OBJECT_ID ('T_Payments_D','TR') IS NULL )
or
(OBJECT_ID ('T_RefPrices_H_I','TR') IS NULL )
or
(OBJECT_ID ('T_RefPrices_H_U','TR') IS NULL )
or
(OBJECT_ID ('T_RefPrices_H_D','TR') IS NULL )
or
(OBJECT_ID ('T_RefTimesPrices_H_I','TR') IS NULL )
or
(OBJECT_ID ('T_RefTimesPrices_H_U','TR') IS NULL )
or
(OBJECT_ID ('T_RefTimesPrices_H_D','TR') IS NULL )
or
(OBJECT_ID ('T_ServerResult_H_I','TR') IS NULL )
or
(OBJECT_ID ('T_ServerResult_H_U','TR') IS NULL )
or
(OBJECT_ID ('T_ServerResult_H_D','TR') IS NULL )
or
(OBJECT_ID ('T_RefOperationType_H_I','TR') IS NULL )
or
(OBJECT_ID ('T_RefOperationType_H_U','TR') IS NULL )
or
(OBJECT_ID ('T_RefOperationType_H_D','TR') IS NULL )
or
(OBJECT_ID ('T_RefCurrency_H_I','TR') IS NULL )
or
(OBJECT_ID ('T_RefCurrency_H_U','TR') IS NULL )
or
(OBJECT_ID ('T_RefCurrency_H_D','TR') IS NULL )
or
(OBJECT_ID ('T_ServerSend_I','TR') IS NULL )
or
(OBJECT_ID ('T_ServerSend_U','TR') IS NULL )
or
(OBJECT_ID ('T_ServerSend_D','TR') IS NULL )
or
(OBJECT_ID ('T_RefObjects_H_I','TR') IS NULL )
or
(OBJECT_ID ('T_RefObjects_H_U','TR') IS NULL )
or
(OBJECT_ID ('T_RefObjects_H_D','TR') IS NULL )
or
(OBJECT_ID ('T_RefDecodeBill_H_I','TR') IS NULL )
or
(OBJECT_ID ('T_RefDecodeBill_H_U','TR') IS NULL )
or
(OBJECT_ID ('T_RefDecodeBill_H_D','TR') IS NULL )
or
(OBJECT_ID ('T_RefDenomination_H_I','TR') IS NULL )
or
(OBJECT_ID ('T_RefDenomination_H_U','TR') IS NULL )
or
(OBJECT_ID ('T_RefDenomination_H_D','TR') IS NULL )
or
(OBJECT_ID ('T_ParkomatOption_H_I','TR') IS NULL )
or
(OBJECT_ID ('T_ParkomatOption_H_U','TR') IS NULL )
or
(OBJECT_ID ('T_ParkomatOption_H_D','TR') IS NULL )
or
(OBJECT_ID ('T_BillEjected_I','TR') IS NULL )
or
(OBJECT_ID ('T_BillEjected_U','TR') IS NULL )
or
(OBJECT_ID ('T_BillEjected_D','TR') IS NULL )
or
(OBJECT_ID ('T_RefParkomats_H_I','TR') IS NULL )
or
(OBJECT_ID ('T_RefParkomats_H_U','TR') IS NULL )
or
(OBJECT_ID ('T_RefParkomats_H_D','TR') IS NULL )
or
(OBJECT_ID ('T_RefTimesWork_H_I','TR') IS NULL )
or
(OBJECT_ID ('T_RefTimesWork_H_U','TR') IS NULL )
or
(OBJECT_ID ('T_RefTimesWork_H_D','TR') IS NULL )
or
(OBJECT_ID ('T_Payments_H_I','TR') IS NULL )
or
(OBJECT_ID ('T_Payments_H_U','TR') IS NULL )
or
(OBJECT_ID ('T_Payments_H_D','TR') IS NULL )
or
(OBJECT_ID ('T_ServerSend_H_I','TR') IS NULL )
or
(OBJECT_ID ('T_ServerSend_H_U','TR') IS NULL )
or
(OBJECT_ID ('T_ServerSend_H_D','TR') IS NULL )
or
(OBJECT_ID ('T_BillEjected_H_I','TR') IS NULL )
or
(OBJECT_ID ('T_BillEjected_H_U','TR') IS NULL )
or
(OBJECT_ID ('T_BillEjected_H_D','TR') IS NULL )
or
(OBJECT_ID ('T_BillAccepted_I','TR') IS NULL )
or
(OBJECT_ID ('T_BillAccepted_U','TR') IS NULL )
or
(OBJECT_ID ('T_BillAccepted_D','TR') IS NULL )
or
(OBJECT_ID ('T_BillAccepted_H_I','TR') IS NULL )
or
(OBJECT_ID ('T_BillAccepted_H_U','TR') IS NULL )
or
(OBJECT_ID ('T_BillAccepted_H_D','TR') IS NULL )
or
(OBJECT_ID ('T_Sessions_H_I','TR') IS NULL )
or
(OBJECT_ID ('T_Sessions_H_U','TR') IS NULL )
or
(OBJECT_ID ('T_Sessions_H_D','TR') IS NULL )
or
(OBJECT_ID ('T_Sessions_I','TR') IS NULL )
or
(OBJECT_ID ('T_Sessions_U','TR') IS NULL )
or
(OBJECT_ID ('T_Sessions_D','TR') IS NULL )
or
(OBJECT_ID ('T_RefParkomatClasses_I','TR') IS NULL )
or
(OBJECT_ID ('T_RefParkomatClasses_U','TR') IS NULL )
or
(OBJECT_ID ('T_RefParkomatClasses_D','TR') IS NULL )
or
(OBJECT_ID ('T_RefPrices_I','TR') IS NULL )
or
(OBJECT_ID ('T_RefPrices_U','TR') IS NULL )
or
(OBJECT_ID ('T_RefPrices_D','TR') IS NULL )
or
(OBJECT_ID ('T_RefTimesPrices_I','TR') IS NULL )
or
(OBJECT_ID ('T_RefTimesPrices_U','TR') IS NULL )
or
(OBJECT_ID ('T_RefTimesPrices_D','TR') IS NULL )
or
(OBJECT_ID ('T_ServerResult_I','TR') IS NULL )
or
(OBJECT_ID ('T_ServerResult_U','TR') IS NULL )
or
(OBJECT_ID ('T_ServerResult_D','TR') IS NULL )
or
(OBJECT_ID ('T_RefOperationType_I','TR') IS NULL )
or
(OBJECT_ID ('T_RefOperationType_U','TR') IS NULL )
	)
	BEGIN
		SET @retVal = 1
		GOTO L_retVal
	END
IF EXISTS (SELECT * FROM sys.triggers WHERE is_disabled = 0)
	SELECT @retVal = 0
ELSE
	SELECT @retVal = 1


	-- Return the result of the function
L_RetVal:
	RETURN @retVal
END


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
set @year_s=''г.''

if lower(@language)=''ua''  begin
	set @month=
	case datepart(month,@date)
		when 1 then ''с_чн€''
		when 2 then ''лютого''
		when 3 then ''березн€''
		when 4 then ''кв_тн€''
		when 5 then ''травн€''
		when 6 then ''червн€''
		when 7 then ''липн€''
		when 8 then ''серпн€''
		when 9 then ''вересн€''
		when 10 then ''жовтн€''
		when 11 then ''листопада''
		when 12 then ''грудн€''
	end
	set @year_s=''р.''
end
	
if lower(@language)=''ru'' begin
	set @month=
	case datepart(month,@date)
		when 1 then ''€нвар€''
		when 2 then ''феврал€''
		when 3 then ''марта''
		when 4 then ''апрел€''
		when 5 then ''ма€''
		when 6 then ''июн€''
		when 7 then ''июл€''
		when 8 then ''августа''
		when 9 then ''сент€бр€''
		when 10 then ''окт€бр€''
		when 11 then ''но€бр€''
		when 12 then ''декабр€''
	end
	set @year_s=''г.''
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

