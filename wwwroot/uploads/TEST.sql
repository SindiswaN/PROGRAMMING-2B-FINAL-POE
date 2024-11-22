create database Test;
use Test;

create table customer (
customerid  int auto_increment not null,
customerFullName varchar(50) not null,
customerEmail varchar(60) not null,
primary key(customerid)
);

SELECT * from customer;

create table orders (
orderid  int auto_increment not null,
orderNumber varchar(255) not null,
date DATE NOT NULL,
primary key(orderid),
foreign key (customerid) references customer (customerid)
);

SELECT * from orders;

insert into customer (customerFullName, customerEmail)
values ("Debbie Duncan" , "dduncan@yahoo.com");

select * 
from customer;

insert into orders (orderNumber,  date)
values ("020149" , "2024-02-14" );

select * 
from orders;

update orders
set date = "2024-2-13"
where orderid = 1;

select * 
from orders;

delete
from orders
where orderid = 1;

select * 
from orders;
