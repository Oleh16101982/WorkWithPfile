USE [Terminal]
GO
/****** Object:  StoredProcedure [dbo].[P_Log_I]    Script Date: 07/17/2006 10:50:24 ******/
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
				@ErrorLine	,	@Text
			)	
-- COMMIT TRANSACTION	
