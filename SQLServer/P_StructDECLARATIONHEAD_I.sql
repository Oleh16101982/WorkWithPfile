USE [P7_file]
GO
/****** Object:  StoredProcedure [dbo].[P_StructDeclarationHEAD_I]    Script Date: 11/13/2013 13:56:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- drop procedure [dbo].[P_StructDeclarationHEAD_I]

CREATE PROCEDURE [dbo].[P_StructDeclarationHEAD_I]      @idDECLARATION int,     @FNAME varchar(50),     @IDBANK char(3),     @CDTASK char(3) = '002',     @CDSUB char(5) = '00001',     @CDFORM char(8) = 'NBU_01SV',     @EI char(1),     @KU char(2) = '16',     @Enabled tinyint = 1,	@id int OUT,
	@error int OUT,
	@ErrorText varchar(200) OUT       as -- declare @id int	DECLARE @rowcount int;
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
	 BEGIN TRY INSERT INTO StructDeclarationHEAD(       idDECLARATION,       FNAME,       IDBANK,       CDTASK,       CDSUB,       CDFORM,       EI,       KU,       Enabled) values (       @idDECLARATION,       @FNAME,       @IDBANK,       @CDTASK,       @CDSUB,       @CDFORM,       @EI,       @KU,       @Enabled)
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