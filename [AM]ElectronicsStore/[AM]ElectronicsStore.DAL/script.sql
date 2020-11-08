create database ElectronicsStoreDB;
GO

use ElectronicsDB;
go
create table Roles
(
	Id int not null identity constraint PK_Roles primary key,
	Name nvarchar(1000) not null
)

insert into Roles values ('Admin');
insert into Roles values ('User');

create table Users
(
	Id int not null identity constraint PK_Users primary key,
	RoleId int not null foreign key references Roles(Id),
	Login nvarchar(100) not null, 
	Password nvarchar(1000) not null,
	Mail nvarchar(1000) not null,
	[DateRegistration] datetime not null
)

create table Categories
(
	Id int not null identity constraint PK_Categories primary key,
	Name nvarchar(100) not null
)

insert into Categories values ('Телефоны');
insert into Categories values ('Планшеты');
insert into Categories values ('Ноутбуки');
insert into Categories values ('Телевизоры');

create table Items
(
	Id int not null identity constraint PK_Items primary key,
	CategoryId int foreign key references Categories(Id), 
	Name nvarchar(100) not null,
	[Description] nvarchar(1000) not null,
	Price money not null,
	[Count] int not null,
	ImagePath nvarchar(1000)
)

/*login=admin, password=admin*/
insert into Users values(1,'admin','d61d004c03457bac7b90c1e8d4f51113be162346b27af5307caffe21ef88597ff15ab1569e07155302ff7b0af29f7f0431531004568da3849a5708176815a70f','admin@admin.ru','2020/10/10')

go
create procedure addUser
(
	@RoleId int = 2,
	@Login nvarchar(100),
	@Mail nvarchar(200),
	@Password nvarchar(1000),	
	@DateRegistration dateTime
)
as
begin
	insert into [Users]
		([RoleId], [Login], [Password], Mail, DateRegistration)
	values
		(@RoleId, @Login, @Password, @Mail, @DateRegistration)

	select cast (SCOPE_IDENTITY() as int) as Id;
end

go
create procedure getUsers
(
	@StartIndex int = 0,
	@Count int = 0

)
as
begin
	select * from Users
	where
		(Users.Id >= @StartIndex) and (@Count = 0 or Users.Id <= @StartIndex + @Count - 1)
end

go
create procedure getUserById
(
	@Id int
)
as
begin
	select * from Users
	where
		(Id = @Id);
end

go
create procedure getUserByLogin
(
	@Login nvarchar(100)
)
as
begin
	select * from Users
	where
		[Login] like @Login
end

go
create procedure delUser
(
	@Id int
)
as
begin
	delete from Users
	where
		(Id = @Id);
end

go
create procedure getItems
(
	@CategoryId int,
	@Name nvarchar
)
as
begin
	select * from Items
	where
		((@CategoryId is null) or (@CategoryId = 0) or (CategoryId = @CategoryId))
		AND ((@Name is null) or (Name like ('%' + @Name+ '%')))
end

go
create procedure getItemById
(
	@Id int
)
as
begin
	select * from Items
	where
		(Id = @Id);
end

go
create procedure delItem
(
	@Id int
)
as
begin
	delete from Items
	where
		(Id = @Id);
end

go
create procedure getCategories
as
begin
	select * from Categories
end


go
create procedure getCategoryById
(
	@Id int
)
as
begin
	select * from Categories
	where
		(Id = @Id);
end
