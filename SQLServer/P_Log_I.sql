-- =============================================
-- Create basic stored procedure template
-- =============================================
use P7_file
-- Drop stored procedure if it already exists
IF EXISTS (
  SELECT * 
    FROM INFORMATION_SCHEMA.ROUTINES 
   WHERE SPECIFIC_SCHEMA = 'dbo'
     AND SPECIFIC_NAME = 'P_Log_I' 
)
   DROP PROCEDURE dbo.P_Log_I
GO

CREATE PROCEDURE dbo.P_Log_I
	@ErrorNumber int, 
	@ErrorSeverity int,
	@ErrorState int,
	@ErrorProcedure varchar(126),
	@ErrorMessage varchar(4000),
	@ErrorLine int, 
	@Text varchar(500)
AS
SET NOCOUNT ON
-- execute dbo.sp_test
-- BEGIN TRANSACTION
-- INSERT INTO TEST (Text) VALUES ('LOG_I')
INSERT INTO dbo.Log
			(
				ErrorNumber	, ErrorSeverity	,	ErrorState	, ErrorProcedure ,	ErrorMessage	,
				ErrorLine	,	[Text]
			)
	VALUES (
				@ErrorNumber	 ,	@ErrorSeverity	,	@ErrorState	, @ErrorProcedure ,	@ErrorMessage	,
				@ErrorLine	,	@Text
			)	
-- COMMIT TRANSACTION	
GO

-- =============================================
-- Example to execute the stored procedure
-- =============================================
-- EXECUTE Schema_Name.Procedure_Name @0, @0
-- delete from dbo.[log]
-- EXECUTE dbo.P_Log_I
--									0, 0 , 0 ,
--									'default state', 'default Message', 
--									0, 'default text'

GO