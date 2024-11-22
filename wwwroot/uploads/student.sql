create database group1;
use group1;
create table student (
studentid  int auto_increment not null,
name varchar(255) not null,
module varchar(50) not null,
primary key(studentid)
);

SELECT * from student;

insert into student (name, module)
values ("sindiswa" , "dbas"),
("johnson" , "prog");

select * 
from student;