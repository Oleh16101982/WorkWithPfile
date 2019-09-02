if not exists(select 1 from sysusers where name='Admin')
	exec sp_addgroup 'Admin'

if not exists(select 1 from sysusers where name='Buch')
	exec sp_addgroup 'Buch'

if not exists(select 1 from sysusers where name='Manager')
	exec sp_addgroup 'Manager'
GO

