use banco;
go

/*
select * from cliente
select * from Cuenta
select * from TipoCuenta
select * from TipoTransaccion
select * from Transaccion
*/

--PRIMER PROCEDIMIENTO ALMACENADO
alter proc SelectClient
	@ClientID int = null
as
	if @ClientID is null or (select id from cliente where id = @ClientID) is null
	begin
		select * from cliente
	end
	else begin
		select * from cliente where id = @ClientID
	end
go

exec SelectClient
exec SelectClient @ClientID = 50
go

--SEGUNDO PROCEDIMIENTO ALMACENADO
alter proc insertCliente 
	@nombre varchar(200),
	@numero varchar(40),
	@correo varchar(50) = null
as
	if (select correo from cliente where correo = @correo) is null
	begin
		insert into cliente (nombre, numeroTelefono, correo)
		values (@nombre, @numero, @correo);
	end
	else begin
		raiserror('Ya existe un cliente creado con ese correo', 1, 1)
		return 1
	end
go

exec insertCliente @nombre = '6hrhyerh', @numero = '24759256729', @correo = '6hrhyerh@gmail.com'
exec insertCliente @nombre = '6hrhyerh', @numero = '24759256729', @correo = 'whfoihowi@gmail.com'
go

--TERCER PROCEDIMIENTO ALMACENADO
alter proc crearTransaccion
	@cuentaID int, 
	@tipo int,
	@cantidad decimal(10,2),
	@cuentaext int = null
as
	if (select id from Cuenta where id = @cuentaID) is null
	begin
		raiserror('Cuenta no existente', 2, 1)
		return 1
	end

	if (select id from TipoTransaccion where id = @tipo) is null
	begin
		raiserror('Tipo no existente', 2, 1)
		return 1
	end

	declare @saldoActual decimal(10,2), @saldoNuevo decimal (10,2)
	set @saldoActual = (select saldo from Cuenta where id = @cuentaID)

	if @tipo = 3 or @tipo = 4
	begin
		set @saldoNuevo = @saldoActual - @cantidad
	end
	else begin
		set @saldoNuevo = @saldoActual + @cantidad
	end

	if @tipo = 1 or @tipo = 3
	begin
		set @cuentaext = null
	end

	begin transaction
	update Cuenta set saldo = @saldoNuevo where id = @cuentaID
	insert into Transaccion (cuentaID, tipoTransaccion, cantidad, cuentaExterna)
	values (@cuentaID, @tipo, @cantidad, @cuentaext);

	if @saldoNuevo >= 0 
	begin
		commit transaction
	end 
	else begin 
		rollback transaction
		raiserror('No cuentas con los suficientes fondos para realizar esta acci�n', 2, 1)
		return 1
	end
go

exec crearTransaccion @cuentaID = 9, @tipo = 1, @cantidad = 100000, @cuentaext = null
exec crearTransaccion @cuentaID = 9, @tipo = 2, @cantidad = 10000000, @cuentaext = null
go 

--HACIENDO UN BACKUP
backup database banco
to disk = 'banco.bak' with format;
go

--restore database banco from disk = 'banco.bak'; go
