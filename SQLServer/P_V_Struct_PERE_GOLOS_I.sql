USE [P7_file]
GO
/****** Object:  StoredProcedure [dbo].[P_V_Struct_PERE_GOLOS_I]    Script Date: 12/06/2013 16:24:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- drop procedure [dbo].[P_V_Struct_PERE_GOLOS_I]

CREATE PROCEDURE [dbo].[P_V_Struct_PERE_GOLOS_I]      	@idDeclaration			int,    @GL_NOMER				varchar(20),
    @GL_DT					datetime,
    @GL_PRICH				varchar(254),
    @NT_COD_FROM_GL_OSOBA	varchar(10),
    @NT_NM1_FROM_GL_OSOBA	varchar(100),
    @NT_NM2_FROM_GL_OSOBA	varchar(50),
    @NT_NM3_FROM_GL_OSOBA	varchar(50),
    @NT_COD_TO_GL_OSOBA		varchar(10),
    @NT_NM1_TO_GL_OSOBA		varchar(100),
    @NT_NM2_TO_GL_OSOBA		varchar(50),
    @NT_NM3_TO_GL_OSOBA		varchar(50),
    @GT_VIDSOTOK			decimal (5, 2),
    @GT_GOLOS				int,
	@Enabled				tinyint = 1,	@id int OUT,
	@error int OUT,
	@ErrorText varchar(200) OUT       as -- declare @id int	DECLARE @idFROM_GL_OSOBA int;	DECLARE @idTO_GL_OSOBA int;	DECLARE @idGL_NABUT int;		DECLARE @rowcount int;
--	DECLARE @error int;
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
--	DECLARE @ErrorText varchar(500);
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
				ENDBEGIN TRANSACTION
	 BEGIN TRY 	 		INSERT INTO StructDeclarationPERE_GOLOS(			idDeclaration			,			GL_NOMER				,
			GL_DT					,
			GL_PRICH				,
			Enabled			) values (			@idDeclaration		,			@GL_NOMER			,
			@GL_DT				,
			@GL_PRICH			,
			@Enabled			)		SELECT @id = @@identity , @rowcount = @@rowcount , @error = @@error	  	UPDATE StructDeclarationPERE_GOLOS
		SET
		RowNum = (SELECT ISNULL(MAX(RowNum) + 1 , 1) from StructDeclarationPERE_GOLOS where idDeclaration = @idDeclaration)
	where id = @id
   		INSERT INTO StructPere_golosFROM_GL_OSOBA(			idDeclarationPERE_GOLOS,			NT_COD,			NT_NM1,
			NT_NM2,	
			NT_NM3,			Enabled			) values (			@id,			@NT_COD_FROM_GL_OSOBA,
			@NT_NM1_FROM_GL_OSOBA,
			@NT_NM2_FROM_GL_OSOBA,
			@NT_NM3_FROM_GL_OSOBA,
			@Enabled)
		SELECT @idFROM_GL_OSOBA = @@identity -- , @rowcount = @@rowcount , @error = @@error
		INSERT INTO StructPere_golosTO_GL_OSOBA(			idDeclarationPERE_GOLOS,			NT_COD,			NT_NM1,
			NT_NM2,	
			NT_NM3,			Enabled			) values (			@id,			@NT_COD_TO_GL_OSOBA,
			@NT_NM1_TO_GL_OSOBA,
			@NT_NM2_TO_GL_OSOBA,
			@NT_NM3_TO_GL_OSOBA,
			@Enabled)
		SELECT @idTO_GL_OSOBA = @@identity -- , @rowcount = @@rowcount , @error = @@error
				INSERT INTO StructPere_golosGL_NABUT(			idDeclarationPERE_GOLOS,			GT_VIDSOTOK,
			GT_GOLOS,	
			Enabled			) values (			@id,			@GT_VIDSOTOK,
			@GT_GOLOS,	
			@Enabled)
	SELECT @idGL_NABUT = @@identity -- , @rowcount = @@rowcount , @error = @@error
	
	
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