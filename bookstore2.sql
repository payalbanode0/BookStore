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
