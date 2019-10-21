create table RolABC
(
	idrol integer IDENTITY(1,1) primary key,
	bono float default 0.0,
	desc_rol varchar(20)
);

create table TipoABC
(
	idtipo integer IDENTITY(1,1) primary key,
	desc_tipo varchar(20)
);

create table EmpleadoABC
(
	idNumEmpleado integer primary key not null,
	nombre varchar(20),
	primerap varchar(20),
	segundoap varchar(20),
	direccion varchar(50),
	curp varchar(18) not null,
	fechanac datetime,
	idrol int not null,
	idtipo int not null,
	img_usuario image,
	CONSTRAINT FK_EmpleadosABC_RolABC FOREIGN KEY (idrol)     
    REFERENCES RolABC (idrol),
    CONSTRAINT FK_EmpleadosABC_TipoABC FOREIGN KEY (idtipo)     
    REFERENCES TipoABC (idtipo),
);

create table SalarioABC
(	
	idSalario int IDENTITY(1,1) primary key,
	idNumEmpleado integer,
	salario_mensual float default 0.00,
	CONSTRAINT FK_SalarioABC_EmpleadoABC FOREIGN KEY (idNumEmpleado)     
    REFERENCES EmpleadoABC (idNumEmpleado)
);

create table MovimientosABC
(
	idmovimiento int IDENTITY(1,1) primary key,
	idnumempleado integer not null,
	cant_entregas integer default 0,
	idrol int not null,
	idtipo int not null,
	fecha_movimiento datetime,
	CONSTRAINT FK_MovimientosABC_EmpleadoABC FOREIGN KEY (idnumempleado)     
    REFERENCES EmpleadoABC (idnumempleado)
);

create table HorariosABC
(
	idhorario int IDENTITY(1,1) primary key,
	idnumempleado integer not null,
	idrol int not null,
	idtipo int not null,
	fecha_movimiento datetime,
	CONSTRAINT FK_HorariosABC_EmpleadoABC FOREIGN KEY (idnumempleado)     
    REFERENCES EmpleadoABC (idnumempleado)
);

insert into RolABC (desc_rol,bono) values ('Chofer', 10.00);
insert into RolABC (desc_rol,bono) values ('Cargador',5.00);
insert into RolABC (desc_rol) values ('Auxiliar');

insert into TipoABC (desc_tipo) values ('Interno');
insert into TipoABC (desc_tipo) values ('Externo');

select * from RolABC
select * from TipoABC
select * from EmpleadoABC
select * from MovimientosABC
select * from SalarioABC
select * from HorariosABC

/*
update SalarioABC set salario_mensual = 1500 where idNumEmpleado = 97899459

insert into HorariosABC(idnumempleado, idrol, idtipo, fecha_movimiento) values (97899459, 2,1,GETDATE())
insert into SalarioABC (idNumEmpleado) values (97848972);

drop table TipoABC;
drop table RolABC;
drop table EmpleadoABC;
drop table SalarioABC;
drop table MovimientosABC;
*/
