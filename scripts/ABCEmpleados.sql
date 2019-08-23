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
 CONSTRAINT FK_EmpleadosABC_SalarioABC FOREIGN KEY (idNumEmpleado)     
    REFERENCES SalarioABC (idnumempleado)
create table RolABC
(
	idrol integer IDENTITY(1,1) primary key,
	desc_rol varchar(20)
);

create table TipoABC
(
	idtipo integer IDENTITY(1,1) primary key,
	desc_tipo varchar(20)
);

create table SalarioABC
(
	idNumEmpleado integer not null,
	salario_mensual float default 0.0,
	CONSTRAINT FK_SalarioABC_EmpleadoABC FOREIGN KEY (idnumempleado)     
    REFERENCES EmpleadoABC (idnumempleado)  
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


insert into RolABC (desc_rol) values ('Chofer');
insert into RolABC (desc_rol) values ('Cargador');
insert into RolABC (desc_rol) values ('Auxiliar');

insert into TipoABC (desc_tipo) values ('Interno');
insert into TipoABC (desc_tipo) values ('Externo');

select * from RolABC
select * from TipoABC
select * from EmpleadoABC

"Violation of PRIMARY KEY constraint 'PK__Empleado__1E8475F75717BEEC'. Cannot insert duplicate key in object 'dbo.EmpleadoABC'.\r\nThe statement has been terminated."

drop table TipoABC;
drop table RolABC;
drop table EmpleadosABC;
drop table SalarioABC;
drop table MovimientosABC;

select * from CatPeriodos

select * from Cl