create database studentz;
use studentz;

create table student(
studentid  int auto_increment not null,
name varchar(25) not null,
surname varchar(50) not null,
age varchar(50) not null,
primary key(studentid),
foreign key (moduleid) references Author (moduleid)
);

SELECT * from student;

create table module(
moduleid int auto_increment,
module_name varchar(30) not null,
lecturer_name varchar(30) not null,
venue varchar(20) not null,
primary key(moduleid)
);

SELECT * from module;

insert into student (name,surname, age) values
('Sindiswa', 'Madliwa', 20),
('Peace', 'Phiri', 20),
('Sinazo', 'Mgidi', 20),
('Fina', 'Masango', 20),
('Fortune', 'Mlilo', 24);

SELECT * from student;

insert into module (module_name,lecturer_name, venue) values
('Programming', 'Dr Rodney', 'PC Lab 10'),                                                           
('Database', 'Dr Rodney', 'PC Lab 9'),
('System Analysis', 'Mrs Nortjie', 'Lecture Theatre 4'),
('Web development', 'Mr familua', 'PC Lab 9');

SELECT * from module;

Create INDEX module_name
on module (module_name);


