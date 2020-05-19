-- drop tables
drop table TablePackagesBeverages;
drop table Beverages;
drop table ReservationsTablePackages;
drop table TablePackages;
drop table Reservations;
drop table Customers;
drop table Tables;
drop table VenueHours;
drop table VenueEmployees;
drop table Venues;
drop table Employees;

-- create tables

create table Employees
(
	Id int IDENTITY(1, 1),
	Name varchar(255) not null,
	Phone varchar(12) not null,
	Email varchar(255) not null,
	Password varchar(255) not null,
	EmployeeNo int not null,
	Title varchar(30) not null,
	Salt varchar(30) not null,

	primary key (Id)
);

create table Venues
(
	Id int IDENTITY(1, 1),
	Name varchar(255) not null,
	Address varchar(255) not null,
	Zip int not null,
	City varchar(255) not null,

	primary key (Id)
);


create table VenueEmployees
(
	Id int IDENTITY(1, 1),
	VenueId int not null,
	EmployeeId int not null,
	AccessLevel int not null,

	primary key (Id),
	constraint fk_venue_employees_venue foreign key (VenueId) references Venues(Id),
	constraint fk_venue_employees_employee foreign key (EmployeeId) references Employees(Id)
);

create table VenueHours
(
	Id int IDENTITY(1, 1),
	WeekDay varchar(12),
	OpenTime time not null,
	CloseTime time not null,
	VenueId int not null,

	primary key (Id),
	constraint fk_venue_hours_venue foreign key (VenueId) references Venues(Id)
);

create table Tables 
(
	Id int IDENTITY(1, 1),
	NoOfSeats int not null,
	Name varchar(255),
	VenueId int not null,

	primary key (Id),
	constraint fk_tables_venue foreign key (VenueId) references Venues(Id)
);

create table Customers
(
	Id int IDENTITY(1, 1),
	Name varchar(255) not null,
	Phone varchar(12) not null,
	Email varchar(255) not null,
	Password varchar(255) not null,
	CustomerNo int not null,
	Salt varchar(30) not null,
	FacebookUserID varchar(255),
	GoogleUserID varchar(255),

	primary key (Id)
);

create table Reservations
(
	Id int IDENTITY(1, 1),
	ReservationNo int not null,
	DateTimeStart DateTime not null,
	DateTimeEnd DateTime not null,
	State int not null,
	CustomerId int not null,
	VenueId int not null,
	TableId int not null,
	CreatedAt DateTime not null,
	UpdatedAt DateTime not null,

	primary key (Id),
	constraint fk_reservations_customer foreign key (CustomerId) references Customers(Id),
	constraint fk_reservations_venue foreign key (VenueId) references Venues(Id),
	constraint fk_reservations_table foreign key (TableId) references Tables(Id)
);

create table TablePackages
(
	Id int IDENTITY(1, 1),
	Name varchar(255) not null,
	Price float not null,
	VenueId int not null,

	primary key (Id),
	constraint fk_table_packages_venue foreign key (VenueId) references Venues(Id)
);

create table ReservationsTablePackages
(
	Id int IDENTITY(1, 1),
	ReservationId int not null,
	TablePackageId int not null,

	primary key (Id),
	constraint fk_reservations_table_packages_reservation foreign key (ReservationId) references Reservations(Id),
	constraint fk_reservations_table_packages_table_package foreign key (TablePackageId) references TablePackages(Id),
);

create table Beverages
(
	Id int IDENTITY(1, 1),
	Name varchar(255) not null,
	Barcode varchar(255) not null,
	Description text not null,
	CostPrice float not null,
	SalesPrice float not null,
	Stock int not null,
	VenueId int not null,

	primary key (Id),
	constraint fk_beverages_venue foreign key (VenueId) references Venues(Id)
);

create table TablePackagesBeverages
(
	Id int IDENTITY(1, 1),
	TablePackageId int not null,
	BeverageId int not null,

	primary key (Id),
	constraint fk_table_packages_beverages_table_package foreign key (TablePackageId) references TablePackages(Id),
	constraint fk_table_packages_beverages_beverage foreign key (BeverageId) references Beverages(Id)
);
