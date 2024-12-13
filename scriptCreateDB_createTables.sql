CREATE database myfinance;
GO;

USE myfinance;

drop table PLANOCONTA
drop table transacao

Create TABLE planoconta(
	id INT NOT NULL identity(1,1),
	nome VARCHAR(50)  not null,
	tipo char(1) not null,
	primary key(id)
);

GO;

CREATE TABLE transacao(
	 id int NOT NULL identity(1,1),
	 data DATETIME NOT NULL,
	 valor DECIMAL(9,2),
	 historico VARCHAR(100),
	 planocontaid INT NOT NULL,
	 primary key(id),
	 foreign key(planocontaid) references planoconta(id)
);

