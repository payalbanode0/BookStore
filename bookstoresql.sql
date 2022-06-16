create table Users
(
UserId int IDENTITY(1,1) PRIMARY KEY,
FullName varchar(255),
Email varchar(255),
Password varchar(255),
MobileNumber bigint
);



Create procedure spUserRegister       
(        
    @FullName varchar(255),
    @Email varchar(255),
    @Password varchar(255),
    @MobileNumber bigint       
)
as         
Begin         
    Insert into Users (FullName,Email,Password,MobileNumber)         
    Values (@FullName,@Email,@Password,@MobileNumber);        
End

---Create procedured for User Login
create procedure spUserLogin
(
@Email varchar(255),
@Password varchar(255)
)
as
begin
select * from Users
where Email = @Email and Password = @Password
End;

create procedure spUserForgotPassword
(
@Email varchar(Max)
)
as
begin
Update Users
set Password = 'Null'
where Email = @Email;
select * from Users where Email = @Email;
End;


Create proc SpUserResetPassword
(@Email varchar(225),
@Password varchar(225)
)
As
Begin
	Update Users
	set Password = @Password where Email = @Email
End;