create table trade
(
	Id int auto_increment
		primary key,
	BOARDID varchar(5) null,
	TradeDate varchar(10) null,
	SHORTNAME varchar(189) null,
	SECIDstr varchar(36) null,
	SecId int not null,
	OPEN varchar(20) null,
	LOW varchar(20) null,
	HIGH varchar(20) null,
	WAPRICE varchar(20) null,
	CLOSE varchar(20) null,
	URL text null
);

