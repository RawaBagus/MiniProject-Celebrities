#create database Celebrity;
create table Celebrities(
	Id int auto_increment primary key,
    Name varchar(255) not null,
    Date_Of_Birth datetime not null,
    IdTown int not null
	);
create table Towns(
	Id int auto_increment primary key,
    Name varchar(255) not null
    );
    
create table Movies(
	Id int auto_increment primary key,
    Name varchar(255) not null
	);

create table MoviesAndCelebrities(
	IdCelebrity int not null,
    IdMovie int not null
    );