USE [P7_file]
GO
/****** Object:  StoredProcedure [dbo].[P_V_Struct_OWNER_D]    Script Date: 12/06/2013 16:17:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- drop procedure P_V_Struct_OWNER_D

CREATE PROCEDURE [dbo].[P_V_Struct_OWNER_D]      	  @idOWNER					int,
	@id int OUT,
	@error int OUT,
	@ErrorText varchar(200) OUT         as 	DECLARE @RowNum int;	DECLARE @idDECLARATION int;			DECLARE @rowcount int;
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
	 BEGIN TRY 	 SELECT @RowNum = RowNum, @idDECLARATION = idDECLARATION  from StructDeclarationOWNER where id = @idOWNER	 	 DELETE FROM StructOwnerGOL_UCH where idDeclaration_OWNER = @idOWNERDELETE FROM StructOwnerOPR_UCH where idDeclaration_OWNER = @idOWNERDELETE FROM StructOwnerOWNER_ADR where idDeclaration_OWNER = @idOWNER
DELETE FROM StructOwnerOWNER_NAZVA where idDeclaration_OWNER = @idOWNER
DELETE FROM StructOwnerOWNER_PASS where idDeclaration_OWNER = @idOWNER
DELETE FROM StructOwnerPR_UCH where idDeclaration_OWNER = @idOWNERDELETE FROM StructOwnerZAG_UCH where idDeclaration_OWNER = @idOWNERDELETE FROM StructDeclarationOWNER where id = @idOWNER

	UPDATE StructDeclarationOWNER 
		SET
		RowNum = (select RowNum - 1 from StructDeclarationOWNER where idDECLARATION = @idDECLARATION and RowNum > @RowNum)



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