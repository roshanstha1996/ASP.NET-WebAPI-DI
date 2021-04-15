use [AasthaDB_new]

Create Table UserTable
(
UserId int identity(1,1) primary key,
UserName varchar(100),
Password varchar(100),
Role varchar(50),
CreatedBy varchar(50),
CreatedDate varchar(40),
LoginDate datetime,
IsActive bit
)
GO

Create Table CategoryTable
(
CategoryId int identity(1,1) primary key,
CategoryName varchar(100),
Description varchar(max)
)
Go

Create Table ProductTable
(
ProductId int identity(1,1) primary key,
CategoryId int,,
ProductName varchar(100),
Rate numeric(18,2),
QuantityInStock int,
ThresholdValue int,
MfgDate date,
ExpDate date,
Remarks varchar(max)
)
Go

Create Table SalesCodeTable
(
SalesCodeId int identity(1,1) primary key,
CustomerName varchar(100),
SalesDate date,
CustomerEmail varchar(100),
ContactNumber varchar(30),
Address varchar(200)
)
Go

Create Table SalesTable
(
SalesId int identity(1,1) primary key,
SalesCodeId int,
ProductId int,
SalesQuantity float,
)
Go

Create Table BillTable
(
BillId int identity(1,1) primary key,
SalesCodeId int,,
Vat numeric(18,2),
Discount numeric(18,2),
NetTotal numeric(18,2)
)
GO

Create Procedure [dbo].[ManageBill]
(
@BillId int,
@SalesCodeId int,
@Vat numeric(18,2),
@Discount numeric(18,2),
@NetTotal numeric(18,2),
@Mode int
)
as
begin
if(@Mode=1)
insert into BillTable(
SalesCodeId,
Vat,
Discount,
NetTotal) 
values 
(
@SalesCodeId,
@Vat,
@Discount,
@NetTotal)
if(@Mode=2)
Update BillTable set 
SalesCodeId = @SalesCodeId,
Vat = @Vat,
NetTotal= @NetTotal,
Discount = @Discount where BillId =@BillId 
if(@Mode=3)
Delete from BillTable where BillId =@BillId 
end
GO


Create Procedure [dbo].[ManageProducts]
(
@ProductId int,
@SubCategoryId int,
@ProductName varchar(100),
@Rate numeric(18,2),
@QuantityInStock int,
@ThresholdValue int,
@MfgDate date,
@ExpDate date,
@Mode int
)
as
begin
if(@Mode=1)
insert into ProductTable(
ProductName,
Rate,
QuantityInStock,
ThresholdValue,
MfgDate,
ExpDate) values (
@ProductName,
@Rate,
@QuantityInStock,
@ThresholdValue,
@MfgDate,
@ExpDate)
if(@Mode=2)
Update ProductTable set 
ProductName=@ProductName,
Rate=@Rate,
QuantityInStock=@QuantityInStock,
ThresholdValue=@ThresholdValue,
MfgDate=@MfgDate,
ExpDate=@ExpDate where ProductId=@ProductId
if(@Mode=3)
Delete from ProductTable where ProductId=@ProductId
end
GO


Create Procedure [dbo].[ManageSalesCodeTable]
(
@SalesCodeId  int,
@CustomerName varchar(100),
@SalesDate date,
@CustomerEmail varchar(100),
@ContactNumber varchar(30),
@Address varchar(200),
@Mode int
)
as
begin
if(@Mode=1)
insert into SalesCodeTable(
CustomerName,
SalesDate,
CustomerEmail,
ContactNumber,
Address) 
values 
(
@CustomerName,
@SalesDate,
@CustomerEmail,
@ContactNumber,
@Address)
if(@Mode=2)
Update SalesCodeTable set 
CustomerName = @CustomerName,
SalesDate= @SalesDate,
CustomerEmail = @CustomerEmail,
ContactNumber = @ContactNumber,
Address = @Address where SalesCodeId=@SalesCodeId
if(@Mode=3)
Delete from SalesCodeTable where SalesCodeId=@SalesCodeId
end
GO



Create Procedure [dbo].[ManageSalesTable]
(
@SalesId int,
@SalesCodeId int,
@ProductId varchar(100),
@SalesQuantity int,
@Mode int
)
as
begin
if(@Mode=1)
insert into SalesTable(
SalesCodeId,
ProductId,
SalesQuantity)
 values 
(
@SalesCodeId,
@ProductId,
@SalesQuantity
)
if(@Mode=2)
Update SalesTable set SalesCodeId = @SalesCodeId,
ProductId = @ProductId,
SalesQuantity = @SalesQuantity where SalesId=@SalesId
if(@Mode=3)
Delete from SalesTable where SalesId=@SalesId
end
GO


Create Procedure [dbo].[SP_ManageCategory]
(
@CategoryId int,
@CategoryName varchar(100),
@Description varchar(max),
@Mode int
)
as
begin
if(@Mode=1)
insert into CategoryTable(CategoryName,Description) values (@CategoryName,@Description)
if(@Mode=2)
Update CategoryTable set CategoryName=@CategoryName,Description=@Description where CategoryId=@CategoryId
if(@Mode=3)
Delete from CategoryTable where CategoryId=@CategoryId
end
go


create  PROCEDURE [dbo].[InsertUser]
(
@UserId int,
@Username varchar(100),
@Password varchar(100),
@Role varchar(50),
@CreatedBy varchar(50),
@CreatedDate datetime,
@IsActive bit
)
AS
BEGIN
SET NOCOUNT ON;
IF EXISTS(SELECT UserId FROM UserTable WHERE Username = @Username)
BEGIN
SELECT -1 AS UserId -- Username exists.
END
ELSE
BEGIN
	INSERT INTO UserTable
		(UserName
		,Password
		,Role
		,CreatedBy
		,IsActive
		,CreatedDate)
	VALUES
		(@UserName
		,@Password
		,@Role
		,@CreatedBy
		,@IsActive
		,GETDATE())
             
	SELECT SCOPE_IDENTITY() AS UserId -- UserId 
END
END
GO



Create PROCEDURE [dbo].[UpdateUser]
(
@UserId int,
@Username varchar(100),
@Password varchar(100),
@Role varchar(50),
@CreatedBy varchar(50),
@IsActive bit
)
AS
BEGIN
SET NOCOUNT ON;
IF EXISTS(SELECT UserId FROM UserTable WHERE UserId = @UserId)
BEGIN
	UPDAte UserTable set
	UserName=@Username
	,Password=@Password
	,Role=@Role
	,CreatedBy=@CreatedBy
	,IsActive=@IsActive
	where UserId = @UserId

	Select @UserId as UserId

END
ELSE
BEGIN
	Select -1 as UserId --user doesnt exist
END
END
GO



CREATE PROCEDURE [dbo].[ValidateUser]
(
@username varchar(100),
@password varchar(100)
)
AS
BEGIN
SET NOCOUNT ON;
declare @UserId int;
     
select @UserId = UserId from UserTable 
where UserName = @username AND Password = @password
     
IF @UserId is not null
BEGIN
	IF NOT EXISTS(SELECT UserId FROM UserTable WHERE UserId = @UserId)
	BEGIN
		UPDATE UserTable
		SET LoginDate = GETDATE()
		WHERE UserId = @UserId

		SELECT @UserId [UserId] -- User Valid
	END
	ELSE
	BEGIN
		SELECT -2 -- User is not activated.
	END
END
ELSE
BEGIN
    SELECT -1 -- User is invalid.
END
END
GO


CREATE Procedure [dbo].[GetSalesReportByDate]
(
@fromdate datetime,
@todate datetime
)
as
begin
select sc.CustomerName, sc.SalesDate, p.ProductName, p.Rate Price, s.SalesQuantity Quantity from SalesTable s
inner join SalesCodeTable sc on sc.SalesCodeId = s.SalesCodeId
inner join ProductTable p on p.ProductId = s.ProductId 
where sc.SalesDate between @fromdate and @todate
end

GO









