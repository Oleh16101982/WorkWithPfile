USE [P7_file]
GO
/****** Object:  StoredProcedure [dbo].[P_V_Struct_OWNER_I]    Script Date: 12/06/2013 16:09:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- drop procedure P_V_Struct_OWNER_I

CREATE PROCEDURE [dbo].[P_V_Struct_OWNER_I]      	  @idDECLARATION					int,
      @OWNER_TYPE						char(1),
      @OWNER_OZN						varchar(2),
      @OWNER_POS						varchar(254),
      @OWNER_DATE						datetime,
      @OWNER_DORG						varchar(254),
--      @idDeclaration_OWNER_GOL_UCH		int,
      @GT_VIDSOTOK_GOL_UCH				decimal(5,2),
      @GT_GOLOSI_GOL_UCH				int,
--      @idDeclaration_OWNER_OPR_UCH		int,
      @UT_VIDSOTOK_OPR_UCH				decimal(5,2),
      @UT_NOMINAL_OPR_UCH				int,
      @UT_GOLOSI_OPR_UCH				int,
--      @idDeclaration_OWNER_OWNER_ADR	int,
      @ADR_COD_KR						varchar(3),
      @ADR_INDEX						varchar(6),
      @ADR_PUNKT						varchar(50),
      @ADR_UL							varchar(50),
      @ADR_BUD							varchar(10),
      @ADR_KORP							varchar(10),
      @ADR_OFF							varchar(10),      
--      @idDeclaration_OWNER_OWNER_NAZVA	int,
      @NT_COD							varchar(10),
      @NT_NM1							varchar(100),
      @NT_NM2							varchar(50),
      @NT_NM3							varchar(50),      
--      @idDeclaration_OWNER_OWNER_PASS	int,
      @PS_SR							varchar(2),
      @PS_NM							varchar(6),
      @PS_DT							datetime,
      @PS_ORG							varchar(254),
--      @idDeclaration_OWNER_PR_UCH		int,
      @UT_VIDSOTOK_PR_UCH				decimal(5,2),
      @UT_NOMINAL_PR_UCH				int,
      @UT_GOLOSI_PR_UCH					int,
--      @idDeclaration_OWNER_ZAG_UCH		int,
      @GT_VIDSOTOK_ZAG_UCH				decimal(5,2),
      @GT_GOLOSI_ZAG_UCH				int,     	  @Enabled							tinyint = 1,	@id int OUT,
	@error int OUT,
	@ErrorText varchar(200) OUT         as 	declare @idDeclaration_OWNER int;	DECLARE @rowcount int;
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
	 BEGIN TRY INSERT INTO StructDeclarationOWNER(
		idDECLARATION,					
		OWNER_TYPE,						
		OWNER_OZN,						
		OWNER_POS,						
		OWNER_DATE,						
		OWNER_DORG					) values (		@idDECLARATION,					
		@OWNER_TYPE,						
		@OWNER_OZN,						
		@OWNER_POS,						
		@OWNER_DATE,						
		@OWNER_DORG						
		)
	SELECT @id = @@identity , @rowcount = @@rowcount , @error = @@error		
	
	UPDATE StructDeclarationOWNER
		SET
		RowNum = (SELECT ISNULL(MAX(RowNum) + 1 , 1) from StructDeclarationOWNER where idDeclaration = @idDeclaration)
	where id = @id
	
	SET @idDeclaration_OWNER = @id
INSERT INTO StructOwnerGOL_UCH(		idDeclaration_OWNER,					
		GT_VIDSOTOK,
		GT_GOLOSI		
) values (		@idDeclaration_OWNER,					
		@GT_VIDSOTOK_GOL_UCH,
		@GT_GOLOSI_GOL_UCH
		)
	
INSERT INTO StructOwnerOPR_UCH(		idDeclaration_OWNER,					
		UT_VIDSOTOK,
		UT_NOMINAL,
		UT_GOLOSI		
) values (		@idDeclaration_OWNER,					
		@UT_VIDSOTOK_OPR_UCH,
		@UT_NOMINAL_OPR_UCH,
		@UT_GOLOSI_OPR_UCH
		)	
	
INSERT INTO StructOwnerOWNER_ADR(
		idDeclaration_OWNER,	
		ADR_COD_KR	,
		ADR_INDEX	,
		ADR_PUNKT	,
		ADR_UL		,
		ADR_BUD		,
		ADR_KORP	,	
		ADR_OFF
) values (
		@idDeclaration_OWNER,
		@ADR_COD_KR	,
		@ADR_INDEX	,
		@ADR_PUNKT	,
		@ADR_UL		,
		@ADR_BUD	,	
		@ADR_KORP	,	
		@ADR_OFF
		)		

INSERT INTO StructOwnerOWNER_NAZVA(
		idDeclaration_OWNER,	
		NT_COD,
		NT_NM1,
		NT_NM2,
		NT_NM3
) values (		
		@idDeclaration_OWNER,	
		@NT_COD,
		@NT_NM1,
		@NT_NM2,
		@NT_NM3
)	
	
INSERT INTO StructOwnerOWNER_PASS(
		idDeclaration_OWNER,	
		PS_SR,	
		PS_NM,	
		PS_DT,
		PS_ORG	
) values (
		@idDeclaration_OWNER,	
		@PS_SR,
		@PS_NM,	
		@PS_DT,	
		@PS_ORG	
		)		
	
INSERT INTO StructOwnerPR_UCH(		idDeclaration_OWNER,					
		UT_VIDSOTOK,
		UT_NOMINAL,
		UT_GOLOSI		
) values (		@idDeclaration_OWNER,					
		@UT_VIDSOTOK_PR_UCH,
		@UT_NOMINAL_PR_UCH,
		@UT_GOLOSI_PR_UCH
		)	


INSERT INTO StructOwnerZAG_UCH(		idDeclaration_OWNER,					
		GT_VIDSOTOK,
		GT_GOLOSI		
) values (		@idDeclaration_OWNER,					
		@GT_VIDSOTOK_ZAG_UCH,
		@GT_GOLOSI_ZAG_UCH
		)
	
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