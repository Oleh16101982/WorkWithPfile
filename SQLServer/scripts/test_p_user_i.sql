DECLARE @RC int
DECLARE @useralias varchar(100)
DECLARE @username varchar(100)
DECLARE @groupname varchar(100)
DECLARE @last_date datetime
DECLARE @suid tinyint
DECLARE @enabled tinyint
DECLARE @password varchar(100)

-- TODO: Set parameter values here.

select @useralias='LAG',
	@username='Любомирский Александр',
	@groupname='admin',
	@password='12345'

EXECUTE @RC = [Terminal].[dbo].[P_Users_I] 
   @useralias=@useralias
  ,@username=@username
  ,@groupname=@groupname
  ,@password=@password


select @RC


select suser_name()
exec sp_addlogin @loginame='LLL1',@passwd='1234',@defdb='Terminal'       
	exec ('Create User LLL1')	EXEC sp_addrolemember 'admin','LLL1'

exec sys.sp_addsrvrolemember @loginame = 'LLL1', @rolename = 'securityadmin'	  EXEC sp_addrolemember 'db_securityadmin', 'LLL1'
	  EXEC sp_addrolemember 'db_accessadmin', 'LLL1'

EXEC sp_addrolemember 'admin', 'LAG'

