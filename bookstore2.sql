---Create book table
create table BookTable(
BookId int identity (1,1)primary key,
BookName varchar(255),
AuthorName varchar(255),
TotalRating int,
RatingCount int,
OriginalPrice decimal,
DiscountPrice decimal,
BookDetails varchar(255),
BookImage varchar(255),
BookQuantity int
);

select *from BookTable;

create procedure SPAddBook
(
@BookName varchar(255),
@AuthorName varchar(255),
@TotalRating int,
@RatingCount int,
@OriginalPrice decimal,
@DiscountPrice decimal,
@BookDetails varchar(255),
@BookImage varchar(255),
@BookQuantity int
)
as
BEGIN
Insert into BookTable(BookName, AuthorName, TotalRating, RatingCount, OriginalPrice, 
DiscountPrice, BookDetails, BookImage, BookQuantity)
values (@BookName, @AuthorName, @TotalRating, @RatingCount ,@OriginalPrice, @DiscountPrice,
@BookDetails, @BookImage, @BookQuantity
);
End;

--create procedure to getbookbybookid
create procedure spGetBookByBookId
(
@BookId int
)
as
BEGIN
select * from BookTable
where BookId = @BookId;
End;

---Procedure to deletebook
create procedure spDeleteBook
(
@BookId int
)
as
BEGIN
Delete BookTable 
where BookId = @BookId;
End;


create procedure spUpdateBook
(
@BookId int,
@BookName varchar(255),
@AuthorName varchar(255),
@TotalRating int,
@RatingCount int,
@OriginalPrice Decimal,
@DiscountPrice Decimal,
@BookDetails varchar(255),
@BookImage varchar(255),
@BookQuantity int
)
as
BEGIN
Update BookTable set BookName = @BookName, 
AuthorName = @AuthorName,
TotalRating = @TotalRating,
RatingCount = @RatingCount,
OriginalPrice= @OriginalPrice,
DiscountPrice = @DiscountPrice,
BookDetails = @BookDetails,
BookImage =@BookImage,
BookQuantity = @BookQuantity
where BookId = @BookId;
End;


-- create procedure to get all book 
create procedure spGetAllBook
as
BEGIN
	select * from BookTable;
End;

create Table CartTable
(
CartId int primary key identity(1,1),
BooksQty int,
UserId int Foreign Key References Users(UserId),
BookId int Foreign Key References BookTable(BookId)
);


select * from CartTable;


create Procedure spAddCart
(
@BooksQty int,
@UserId int,
@BookId int
)
As
Begin
Insert into CartTable (BooksQty,UserId,BookId) 
values (@BooksQty,@UserId,@BookId);
End;

---Create procedure to deletecart
create procedure spDeleteCart
@CartId int
As
Begin 
Delete CartTable where CartId = @CartId
End


---create procedure to UpdateCart
create procedure spUpdateCart
@BooksQty int,
@CartId int
As
Begin
update CartTable set BooksQty = @BooksQty
where CartId = @CartId
End

--create procedure to GetAllBookinCart by UserId
create procedure spGetAllBookinCart
@UserId int
As
Begin
select CartTable.CartId,CartTable.UserId,CartTable.BookId,CartTable.BooksQty,
BookTable.BookName,BookTable.AuthorName,BookTable.TotalRating,BookTable.RatingCount,BookTable.OriginalPrice,BookTable.DiscountPrice,BookTable.BookDetails,BookTable.BookImage,BookTable.BookQuantity 
from CartTable inner join BookTable on CartTable.BookId = BookTable.BookId
where CartTable.UserId = @UserId
End



select * from WishlistTable


create procedure spAddInWishlist
@UserId int,
@BookId int
As
Begin
Insert Into WishlistTable (UserId,BookId) values (@UserId,@BookId)
End
DROP PROCEDURE spAddInWishlist;
GO


--create procedure to delete from wishlist
create procedure spDeleteFromWishlist
@WishListId int
As
Begin
Delete WishlistTable where WishListId=@WishListId
End




drop table WishlistTable;

use BookStore

create table WishListTable
(
	WishListId int identity(1,1) not null primary key,
	UserId int foreign key references Users(UserId) on delete no action,
	BookId int foreign key references BookTable(BookId) on delete no action
);

create proc AddWishList
(
@UserId int,
@BookId int
)
as
begin 
       insert into WishlistTable
	   values (@UserId,@BookId);
end;


create procedure spGetAllBooksinWishList
@UserId int
As
Begin
select WishlistTable.WishListId,WishlistTable.UserId,WishlistTable.BookId,
BookTable.BookName,BookTable.AuthorName,BookTable.TotalRating,BookTable.RatingCount,BookTable.OriginalPrice,BookTable.DiscountPrice,BookTable.BookDetails,BookTable.BookImage,BookTable.BookQuantity 
from WishlistTable inner join BookTable on WishlistTable.BookId=BookTable.BookId
where WishlistTable.userId=@UserId
End



---create address type table
create Table AddressTypeTable
(
	TypeId INT IDENTITY(1,1) PRIMARY KEY,
	AddressType varchar(255)
);

select * from AddressTypeTable


---insert record for addresstype table
insert into AddressTypeTable values('Home'),('Office'),('Other');

---create address table
create Table AddressTable
(
AddressId INT IDENTITY(1,1) PRIMARY KEY,
Address varchar(255),
City varchar(100),
State varchar(100),
TypeId int 
FOREIGN KEY (TypeId) REFERENCES AddressTypeTable(TypeId),
UserId INT FOREIGN KEY (UserId) REFERENCES Users(UserId)
);
select * from AddressTable


--create procedure to AddAddress
--- Procedure To Add Address
create procedure spAddAddress
(
@Address varchar(max),
@City varchar(100),
@State varchar(100),
@TypeId int,
@UserId int
)
as
BEGIN
If Exists (select * from AddressTypeTable where TypeId = @TypeId)
begin
Insert into AddressTable 
values(@Address, @City, @State, @TypeId, @UserId);
end
Else
begin
select 2
end
End;


--create procedure for updateAddress
create procedure spUpdateAddress
(
	@AddressId int,
	@Address varchar(max),
	@City varchar(100),
	@State varchar(100),
	@TypeId int
)
as
BEGIN
If Exists (select * from AddressTypeTable where TypeId = @TypeId)
begin
Update AddressTable set
Address = @Address, City = @City,
State = @State , TypeId = @TypeId
where AddressId = @AddressId
end
Else
begin
select 2
end
End;

--create procedure to delete address
create Procedure spDeleteAddress
(
@AddressId int
)
as
BEGIN
Delete AddressTable where AddressId = @AddressId 
End;


create table OrderTable
(
OrderId int primary key identity(1,1),
TotalPrice int,
BooksQty int,
OrderDate Date,
BookCount int,
UserId int FOREIGN KEY (UserId) REFERENCES Users(UserId),
bookId int FOREIGN KEY (bookId) REFERENCES BookTable(bookId),
AddressId int FOREIGN KEY (AddressId) REFERENCES AddressTable(AddressId)
);


create table Orders(
	OrdersId int identity(1,1) not null primary key,
	TotalPrice int not null,
	OrderBookQuantity int not null,
	OrderDate Date not null,
	UserId int not null foreign key (UserId) references Users(UserId),
	BookId int not null foreign key (BookId) references BookTable(BookId),
	AddressId int not null foreign key (AddressId) references AddressTable(AddressId)
);


create procedure spAddOrder
(
	@OrderBookQuantity int,
	@UserId int,
	@BookId int,
	@AddressId int
)
as
Declare @TotalPrice int
begin
	set @TotalPrice = (select DiscountPrice from BookTable where BookId = @BookId);
	If(Exists(Select * from BookTable where BookId = @BookId))
		begin
			If(Exists (Select * from Users where UserId = @UserId))
				BEGIN
					Begin try
						Begin Transaction
						Insert Into Orders(TotalPrice, OrderBookQuantity, OrderDate, UserId, BookId, AddressId)
						Values(@TotalPrice*@OrderBookQuantity, @OrderBookQuantity, GETDATE(), @UserId, @BookId, @AddressId);
						Update BookTable set BookQuantity=BookQuantity-@OrderBookQuantity where BookId = @BookId;
						Delete from CartTable where BookId = @BookId and UserId = @UserId;
						select * from Orders;
						commit Transaction
					End try
					Begin Catch
							rollback;
					End Catch
				end
			Else
				Begin
					Select 3;
				End
		End
	Else
		Begin
			Select 2;
		End
end;

drop table Orders

Create procedure spGetOrders
(
	@UserId int
)
as
begin
		Select 
		O.OrdersId, O.UserId, O.AddressId, b.bookId,
		O.TotalPrice, O.OrderBookQuantity, O.OrderDate,
		b.BookName, b.AuthorName, b.BookImage
		FROM BookTable b
		inner join Orders O on O.BookId = b.BookId 
		where 
			O.UserId = @UserId;
end;


create table Orders(
	OrdersId int identity(1,1) not null primary key,
	TotalPrice int not null,
	OrderBookQuantity int not null,
	OrderDate Date not null,
	UserId int not null foreign key (UserId) references Users(UserId),
	BookId int not null foreign key (BookId) references BookTable(BookId),
	AddressId int not null foreign key (AddressId) references AddressTable(AddressId)
);


---Adding Orders Store Procedure----
create or alter Proc spAddOrder
(
	@OrderBookQuantity int,
	@UserId int,
	@BookId int,
	@AddressId int
)
as
Declare @TotalPrice int
begin
	set @TotalPrice = (select DiscountPrice from BookTable where BookId = @BookId);
	If(Exists(Select * from BookTable where BookId = @BookId))
		begin
			If(Exists (Select * from Users where UserId = @UserId))
				BEGIN
					Begin try
						Begin Transaction
						Insert Into Orders(TotalPrice, OrderBookQuantity, OrderDate, UserId, BookId, AddressId)
						Values(@TotalPrice*@OrderBookQuantity, @OrderBookQuantity, GETDATE(), @UserId, @BookId, @AddressId);
						Update BookTable set BookQuantity=BookQuantity-@OrderBookQuantity where BookId = @BookId;
						Delete from CartTable where BookId = @BookId and UserId = @UserId;
						select * from Orders;
						commit Transaction
					End try
					Begin Catch
							rollback;
					End Catch
				end
			Else
				Begin
					Select 3;
				End
		End
	Else
		Begin
			Select 2;
		End
end;



---Get ALl Orders By UserId=----
Create or Alter Proc spGetOrders
(
	@UserId int
)
as
begin
		Select 
		O.OrdersId, O.UserId, O.AddressId, b.bookId,
		O.TotalPrice, O.OrderBookQuantity, O.OrderDate,
		b.BookName, b.AuthorName, b.BookImage
		FROM BookTable b
		inner join Orders O on O.BookId = b.BookId 
		where 
			O.UserId = @UserId;
end;


use BookStore

-- Table For Feedback----
create Table Feedback
(
	FeedbackId int identity(1,1) not null primary key,
	Comment varchar(max) not null,
	Rating int not null,
	BookId int not null 
	foreign key (BookId) references BookTable(BookId),
	UserId INT not null
	foreign key (UserId) references Users(UserId),
);

select *from Feedback

 alter  Proc spAddFeedback
(
	@Comment varchar(max),
	@Rating int,
	@BookId int,
	@UserId int
)
as
Declare @AverageRating int;
begin
	if (exists(SELECT * FROM Feedback where BookId = @BookId and UserId=@UserId))
		select 1;
	Else
	Begin
		if (exists(SELECT * FROM BookTable WHERE BookId = @BookId))
		Begin  select * from Feedback
			Begin try
				Begin transaction
					Insert into Feedback(Comment, Rating, BookId, UserId) values(@Comment, @Rating, @BookId, @UserId);		
					set @AverageRating = (Select AVG(Rating) from Feedback where BookId = @BookId);
					Update BookTable set Rating = @AverageRating,  RatingCount = RatingCount + 1 
								 where  BookId = @BookId;
				Commit Transaction
			End Try
			Begin catch
				Rollback transaction
			End catch
		End
		Else
		Begin
			Select 2; 
		End
	End
end;

create Proc spGetFeedback
(
	@BookId int
)
as
begin
	Select FeedbackId, Comment, Rating, BookId, u.FullName
	From Users u
	Inner Join Feedback f
	on f.UserId = u.UserId
	where
	 BookId = @BookId;
end;

