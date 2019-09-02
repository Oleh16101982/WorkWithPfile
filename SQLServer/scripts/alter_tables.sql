
alter table 
	PAYMENTS
add
	TerminalId		int			 NOT NULL	Default (0)			with values,
	TerminalName	varchar(100) NOT NULL	Default ('Unknown') with values,
	TerminalOwner	varchar(100) NOT NULL	Default ('Unknown') with values,
	TerminalCity	varchar(100) NOT NULL	Default ('Unknown') with values

alter table 
	REfCurrency
add
	currencycode		varchar(5)	NOT NULL	Default ('0')			with values,
	decimal_extension	int			NOT NULL	Default ('2')			with values,
	currency_name1		varchar(50)	NOT NULL	Default (' ')			with values,  
	currency_name2      varchar(50)	NOT NULL	Default (' ')			with values,  
	currency_name3		varchar(50)	NOT NULL	Default (' ')			with values,     
	decimal_name1		varchar(50)	NOT NULL	Default (' ')			with values,      
	decimal_name2		varchar(50)	NOT NULL	Default (' ')			with values,     
	decimal_name3		varchar(50)	NOT NULL	Default (' ')			with values,  
	gender				varchar(1)	NOT NULL	Default ('M')			with values


