use BaseDeEjemplo;

create table estudiantes(
	id int auto_increment,
    nombre varchar(2024) not null,
    apellido varchar(2024) not null,
    contacto varchar(2024) not null,
    primary key (id)
);