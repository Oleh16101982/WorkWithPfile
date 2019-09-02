if exists (select 1 from sysobjects where name='V_Terminals' and type='V')
	drop view V_Terminals
go
create view 
		  V_Terminals
as
select
          TerminalUID as UID,
          value as [Name],
          isnull((select value from TerminalData where TerminalUID=t.TerminalUID and parameter='TerminalOwner'),' ') as owner,	
		  isnull((select value from TerminalData where TerminalUID=t.TerminalUID and parameter='TerminalZip'),' ') as Zip,
		  isnull((select value from TerminalData where TerminalUID=t.TerminalUID and parameter='TerminalCity'),' ') as city,
		  isnull((select value from TerminalData where TerminalUID=t.TerminalUID and parameter='TerminalAddress'),' ') as adress,
		  isnull((select value from TerminalData where TerminalUID=t.TerminalUID and parameter='TerminalGroup'),' ') as [Group],
		  isnull((select value from TerminalData where TerminalUID=t.TerminalUID and parameter='TerminalOwnerAddress'),' ') as OwnerAdress,
		  isnull((select value from TerminalData where TerminalUID=t.TerminalUID and parameter='TerminalOwnerCity'),' ') as OwnerCity,
		  isnull((select value from TerminalData where TerminalUID=t.TerminalUID and parameter='TerminalOwnerPhone'),' ') as OwnerPhone
from
         TerminalData t
where
         parameter='TerminalName'

--select * from TerminalData
--select * from V_Terminals