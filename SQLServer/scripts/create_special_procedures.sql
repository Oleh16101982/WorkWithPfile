IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[P_Log_I]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[P_Log_I]
go
CREATE PROCEDURE [dbo].[P_Log_I]
	@ErrorNumber int, 
	@ErrorSeverity int,
	@ErrorState int,
	@ErrorProcedure varchar(126),
	@ErrorMessage varchar(4000),
	@ErrorLine int, 
	@Text varchar(500)
AS
SET NOCOUNT ON

INSERT INTO dbo.Log
			(
				ErrorNumber	, ErrorSeverity	,	ErrorState	, ErrorProcedure ,	ErrorMessage	,
				ErrorLine	,	[Text]
			)
	VALUES (
				@ErrorNumber	 ,	@ErrorSeverity	,	@ErrorState	, @ErrorProcedure ,	@ErrorMessage	,
				@ErrorLine	,	@Text)
go

/****** Object:  StoredProcedure [dbo].[P_Users_I]    Script Date: 07/22/2006 11:59:37 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[P_Users_I]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[P_Users_I]
GO
/****** Object:  StoredProcedure [dbo].[P_Users_I]    Script Date: 07/22/2006 11:54:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[P_Users_I]           @useralias varchar(100),     @username	varchar(100),     @groupname varchar(100),     @last_date datetime='01.01.1900',     @suid		tinyint='0',     @enabled	tinyint='1',	 @password  varchar(100) as 	declare @id int;	DECLARE @rowcount int;
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
    DECLARE @db       varchar(125);
	CREATE TABLE #sp_Caller (id int)
	INSERT INTO #sp_Caller (id) VALUES (@@PROCID)
		IF dbo.F_CheckTriggers() = 1
				BEGIN
					SET @ErrorNumber = 50009
					SET @ErrorSeverity = 18
					SET @ErrorState = 1
					SET @ErrorMessage = 'User Error'
					SET @ErrorText = 'Error in F_CheckTriggers'
					set @ErrorProcedure = OBJECT_NAME(@@PROCID)
					EXECUTE p_Log_I @ErrorNumber , @ErrorSeverity , @ErrorState , @ErrorProcedure , @ErrorNumber , 0 , @ErrorText
					RAISERROR(@ErrorNumber , @ErrorSeverity , @ErrorState , @ErrorText)
					GOTO L_ERR
				ENDBEGIN TRY select @db=DB_NAME()if   not exists(select 1 from sysusers where name=@UserAlias) begin	exec sp_addlogin @loginame=@UserAlias,@passwd=@password,@defdb=@db       
	exec ('Create User '+@UserAlias)	EXEC sp_addrolemember @GroupName, @UserAlias	if @groupname='Admin' begin	  /*exec sys.sp_addsrvrolemember @loginame = @UserAlias, @rolename = 'securityadmin'	  exec sys.sp_addsrvrolemember @loginame = @UserAlias, @rolename = 'accessadmin'	  EXEC sp_addrolemember 'db_securityadmin', @UserAlias
	  EXEC sp_addrolemember 'db_accessadmin', @UserAlias*/
	  exec sys.sp_addsrvrolemember @loginame = @UserAlias, @rolename = 'sysadmin'
	endendelse begin	exec sp_addrolemember  @rolename =  @GroupName, 
     @membername =  @UserAlias	if @groupname='Admin' begin	   exec sys.sp_addsrvrolemember @loginame = @UserAlias, @rolename = 'sysadmin'	   /*EXEC sp_addrolemember 'db_sysadmin', @UserAlias*/
	endendselect 	@suid=uid from 	sysuserswhere	name=@UserAliasBEGIN TRANSACTION
INSERT INTO Users(       useralias,       username,       groupname,       last_date,       suid,       enabled) values (       @useralias,       @username,       @groupname,       @last_date,       @suid,       @enabled)
	SELECT @id = @@identity , @rowcount = @@rowcount , @error = @@error
	COMMIT TRANSACTION
	SET @ErrorText = 'Success exec Procedure - ' + OBJECT_NAME(@@PROCID) + '. RowCount - ' + cast(@rowcount as varchar(3))
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
	SELECT @ErrorText = 'Error in Procedure - '+OBJECT_NAME ( @@ProcId) +'. RowCount - ' + cast(@rowcount as varchar(3))
	if @@trancount > 0
		ROLLBACK TRANSACTION
	exec p_Log_I @ErrorNumber , @ErrorSeverity , @ErrorState , @ErrorProcedure , @ErrorMessage , @ErrorLine , @ErrorText
	exec sp_droplogin  @loginame		=	@UserAlias
	exec sp_dropuser	  @name_in_db	=	@UserAlias
	RAISERROR (@ErrorMessage , @ErrorSeverity , @ErrorState)
END CATCH
L_ERR:
-- select @id , @rowcount , @error
DROP TABLE #sp_Caller

GO

/****** Object:  StoredProcedure [dbo].[P_Users_U]    Script Date: 07/24/2006 15:44:07 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[P_Users_U]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[P_Users_U]
GO

USE [Terminal]
GO
/****** Object:  StoredProcedure [dbo].[P_Users_U]    Script Date: 07/24/2006 15:44:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[P_Users_U]      @id int,      @useralias varchar(100),     @username varchar(100),     @groupname varchar(100),     @last_date datetime = '01.01.1900',     @suid tinyint,     @enabled tinyint,	 @password  varchar(100) = NULL as 	DECLARE @rowcount int;
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
		IF dbo.F_CheckTriggers() = 1
				BEGIN
					SET @ErrorNumber = 50009
					SET @ErrorSeverity = 18
					SET @ErrorState = 1
					SET @ErrorMessage = 'User Error'
					SET @ErrorText = 'Error in F_CheckTriggers'
					set @ErrorProcedure = OBJECT_NAME(@@PROCID)
					EXECUTE p_Log_I @ErrorNumber , @ErrorSeverity , @ErrorState , @ErrorProcedure , @ErrorNumber , 0 , @ErrorText
					RAISERROR(@ErrorNumber , @ErrorSeverity , @ErrorState , @ErrorText)
					GOTO L_ERR
				ENDif @last_date='01.01.1900'   select @last_date=last_date from USERS where UserAlias=@UserAlias and GroupName=@GroupNameBEGIN TRANSACTION
	 BEGIN TRY UPDATE    UsersSET        username=@username,       enabled=@enabled,	   last_date=@last_date where id = @id
	SELECT @id = @@identity , @rowcount = @@rowcount , @error = @@error
	COMMIT TRANSACTION
    if (rtrim(@Password) is not null) and (rtrim(@Password)<>'')
	   EXEC sp_password NULL, @Password, @UserAlias
	SET @ErrorText = 'Success exec Procedure - ' + OBJECT_NAME(@@PROCID) + '. RowCount - ' + cast(@rowcount as varchar(3))
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
	SELECT @ErrorText = 'Error in Procedure - '+OBJECT_NAME ( @@ProcId) +'. RowCount - ' + cast(@rowcount as varchar(3))
	if @@trancount > 0
		ROLLBACK TRANSACTION
	exec p_Log_I @ErrorNumber , @ErrorSeverity , @ErrorState , @ErrorProcedure , @ErrorMessage , @ErrorLine , @ErrorText
	RAISERROR (@ErrorMessage , @ErrorSeverity , @ErrorState)
END CATCH
L_ERR:
-- select @id , @rowcount , @error
DROP TABLE #sp_Caller
GO

/****** Object:  StoredProcedure [dbo].[P_Users_D]   ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[P_Users_D]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[P_Users_D]
GO
USE [Terminal]
GO
/****** Object:  StoredProcedure [dbo].[P_Users_D]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[P_Users_D]  @id int as 	DECLARE @rowcount int;
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
		IF dbo.F_CheckTriggers() = 1
				BEGIN
					SET @ErrorNumber = 50009
					SET @ErrorSeverity = 18
					SET @ErrorState = 1
					SET @ErrorMessage = 'User Error'
					SET @ErrorText = 'Error in F_CheckTriggers'
					set @ErrorProcedure = OBJECT_NAME(@@PROCID)
					EXECUTE p_Log_I @ErrorNumber , @ErrorSeverity , @ErrorState , @ErrorProcedure , @ErrorNumber , 0 , @ErrorText
					RAISERROR(@ErrorNumber , @ErrorSeverity , @ErrorState , @ErrorText)
					GOTO L_ERR
				END	declare
			@UserAlias varchar(100)
	select
			@UserAlias=UserAlias
	from
		Users
	where
			id=@idBEGIN TRY 	exec sp_droplogin		@loginame	=	@UserAlias
	exec sp_dropuser		@name_in_db	=	@UserAlias
BEGIN TRANSACTION	DELETE FROM Users where id = @id
	SELECT @id = @@identity , @rowcount = @@rowcount , @error = @@error
	COMMIT TRANSACTION
	SET @ErrorText = 'Success exec Procedure - ' + OBJECT_NAME(@@PROCID) + '. RowCount - ' + cast(@rowcount as varchar(3))
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
	SELECT @ErrorText = 'Error in Procedure - '+OBJECT_NAME ( @@ProcId) +'. RowCount - ' + cast(@rowcount as varchar(3))
	if @@trancount > 0
		ROLLBACK TRANSACTION
	exec p_Log_I @ErrorNumber , @ErrorSeverity , @ErrorState , @ErrorProcedure , @ErrorMessage , @ErrorLine , @ErrorText
	RAISERROR (@ErrorMessage , @ErrorSeverity , @ErrorState)
END CATCH
L_ERR:
-- select @id , @rowcount , @error
DROP TABLE #sp_Caller