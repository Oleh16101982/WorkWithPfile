--drop table users

if not exists(select 1 from sysobjects where name='Users' and type='U')
create table
	Users(
	id	int IDENTITY PRIMARY KEY,
	useralias		varchar(100)	NOT NULL,
	username		varchar(100)	NOT NULL,
	groupname		varchar(100)	NOT NULL,
    last_date	datetime			NULL,
	suid		tinyint			NOT NULL default(0),
	enabled		tinyint			NOT NULL default(1)
	)

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND name = N'IX_Users_Alias_Group')
CREATE UNIQUE NONCLUSTERED INDEX [IX_Users_Alias_Group] ON [dbo].[Users] 
(
	[useralias] ASC,
	[groupname] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO
	
	