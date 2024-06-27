
use FishingStore
go 

create table Roles(
	Id int primary key identity(1,1),
	Name nvarchar(50) not null)

create table Users(
	Id int primary key identity(1,1),
	FullName nvarchar(max) not null,
	Email nvarchar(60) not null,
	Password nvarchar(20) not null,
	RoleId int references Roles(Id) not null
	)

create table Categories(
	Id int primary key identity(1,1),
	Name nvarchar(50) not null)

create table Items(
	Id int primary key identity(1,1),
	Name nvarchar(100) not null,
	Description nvarchar(1000) not null,
	Price money not null,
	Discount int,
	TitleImagePath varchar(max) not null,
	CategoryId int references Categories(Id) not null
	)

	insert into Roles values ('Admin'),
	('User');
	insert into Users values ('Admin','admin@gmail.com','Admin',1);