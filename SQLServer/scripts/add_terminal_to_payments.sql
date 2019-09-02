
alter table 
	PAYMENTS
add
	TerminalId		int			 NOT NULL	Default (0)			with values,
	TerminalName	varchar(100) NOT NULL	Default ('Unknown') with values,
	TerminalOwner	varchar(100) NOT NULL	Default ('Unknown') with values,
	TerminalCity	varchar(100) NOT NULL	Default ('Unknown') with values