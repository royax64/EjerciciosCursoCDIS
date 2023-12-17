--create database banco;

use banco;

/*
create table cliente(
    id int primary key identity(1,1), 
    nombre varchar(200) not null,
    numeroTelefono varchar(40) not null,
    correo varchar(50),
    saldo decimal(10, 2)
)

alter table cliente drop column saldo
alter table cliente add fechaRegistro datetime default getdate()
alter table cliente alter column fechaRegistro datetime not null


insert into cliente (nombre, numeroTelefono, correo) values
('Roy', '2845432325', 'roy@gmail.com');

insert into cliente (nombre, numeroTelefono, correo) values
('Roy malo', '6666666666', 'roymalo@gmail.com');

insert into cliente (nombre, numeroTelefono, correo) 
values ('Conroy Stollenhof', '546-880-0673', 'cstollenhof0@qq.com');

insert into cliente (nombre, numeroTelefono, correo) 
values ('Lolly Willcocks', '463-838-9868', 'lwillcocks1@goo.ne.jp');

insert into cliente (nombre, numeroTelefono, correo) 
values ('Phyllys Adcocks', '754-727-2569', 'padcocks2@bloglovin.com');

insert into cliente (nombre, numeroTelefono, correo) 
values ('Doralia Frid', '297-393-1476', 'dfrid3@comcast.net');

insert into cliente (nombre, numeroTelefono, correo) 
values ('Dyanna Baylis', '533-828-5190', 'dbaylis4@stumbleupon.com');

insert into cliente (nombre, numeroTelefono, correo) 
values ('Zara Greenhall', '626-412-3236', 'zgreenhall5@bluehost.com');

insert into cliente (nombre, numeroTelefono, correo) 
values ('Ambrose Lindner', '304-253-3039', 'alindner6@jalbum.net');

insert into cliente (nombre, numeroTelefono, correo) 
values ('Moria Horsey', '618-173-8192', 'mhorsey7@virginia.edu');

insert into cliente (nombre, numeroTelefono, correo) 
values ('Umeko Fend', '481-965-1011', 'ufend8@hugedomains.com');

insert into cliente (nombre, numeroTelefono, correo) 
values ('Zahara Scarr', '284-998-2993', 'zscarr9@blogtalkradio.com');


update cliente set correo = 'roy@outlook.com' where id = 1

delete from cliente where id in (11,12,13,14)

create table TipoCuenta(
	id int primary key identity(1,1),
	nombre varchar(100) not null,
	fechaRegistro datetime not null default getdate()
);

create table TipoTransaccion(
	id int primary key identity(1,1),
	nombre varchar(100) not null,
	fechaRegistro datetime not null default getdate()
);

create table Cuenta(
	id int primary key identity(1,1),
	tipoCuenta int not null foreign key references TipoCuenta(id),
	idCliente int not null foreign key references cliente(id),
	saldo decimal(10,2) not null,
	fechaRegistro datetime not null default getdate()
);

create table Transaccion(
	id int primary key identity(1,1),
	cuentaID int not null foreign key references Cuenta(id),
	tipoTransaccion int not null foreign key references TipoTransaccion(id),
	cantidad decimal(10,2) not null,
	cuentaExterna int null,
	fechaRegistro datetime not null default getdate()
);

insert into TipoCuenta (nombre) values ('Personal'), ('Nomina'), ('Ahorro')
insert into TipoTransaccion (nombre) values ('Depósito en efectivo'), ('Depósito via transferencia'), ('Retiro en efectivo'), ('Retiro via transferencia')

insert into Cuenta(tipoCuenta, idCliente, saldo) values
(1, 1, 5000),
(2, 1, 10000),
(3, 2, 100),
(2, 5, 6000);

insert into Transaccion(cuentaID, tipoTransaccion, cantidad, cuentaExterna) values
(9, 1, 36356, 123123123),
(10, 4, 654, null),
(12, 3, 234, 456456456),
(11, 2, 56, null);

select * from cliente
select * from Cuenta
select * from TipoCuenta
select * from TipoTransaccion
select * from Transaccion


select c.id, cl.nombre as cliente , c.saldo, c.fechaRegistro, tc.nombre as tipoCuenta 
from Cuenta c
join Cliente cl on c.idCliente = cl.id
join TipoCuenta tc on c.tipoCuenta = tc.id

select t.id, cl.nombre as cliente, tt.nombre as tipoTransaccion, t.cantidad, t.cuentaExterna
from Transaccion t
join TipoTransaccion tt on t.tipoTransaccion = tt.id
join Cuenta c on t.cuentaID = c.id
join cliente cl on c.idCliente = cl.id

create proc SelectAccount as
	select * from cliente
go

alter proc SelectAccount 
	@usrID int = null
as
	if @usrID is null 
	begin 
		select * from cliente
	end
	else begin 
		select * from cliente where id = @usrID
	end
go

exec SelectAccount @usrID = 1

create proc insertCliente 
	@nombre varchar(200),
	@numero varchar(40),
	@correo varchar(50) = null
as
	insert into cliente (nombre, numeroTelefono, correo)
	values (@nombre, @numero, @correo)
go

exec insertCliente 'Antonio', '8184859932', 'antonio@gmail.com'


create trigger afterClienteCreado on cliente after insert
as
		declare @newID int
		set @newID = (select id from inserted)
		insert into Cuenta (tipoCuenta, idCliente, saldo) 
		values (1, @newID, 0);
go


exec insertCliente 'Juan', '01234567', 'juan@hotmail.com'


alter table Cuenta alter column id int null

create trigger beforeDeleteClient on cliente instead of delete
as
	declare @delID int
	set @delID = (select id from deleted)
	update Cuenta set clienteID = null where clienteID = @delID
	delete from cliente where id = @delID --nótese que es explícito en los instead/before
go

create proc crearTransaccion
	@cuentaID int, 
	@tipo int,
	@cantidad decimal(10,2),
	@cuentaext int = null
as
	declare @saldoActual decimal(10,2), @saldoNuevo decimal (10,2)
	begin transaction
	set @saldoActual = (select saldo from Cuenta where id = @cuentaID)

	if @tipo = 2 or @tipo = 4
		set @saldoNuevo = @saldoActual - @cantidad
	else
		set @saldoNuevo = @saldoActual + @cantidad

	update Cuenta set saldo = @saldoNuevo where id = @cuentaID
	insert into Transaccion (cuentaID, tipoTransaccion, cantidad, cuentaExterna)
	values (@cuentaID, @tipo, @cantidad, @cuentaext);

	if @saldoNuevo >= 0
		commit transaction
	else
		rollback transaction
go

exec crearTransaccion 9, 1, 100, null
*/
