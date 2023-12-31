USE [Cafe]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 10/31/2022 10:59:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[UserName] [nvarchar](100) NOT NULL,
	[DisplayName] [nvarchar](100) NOT NULL,
	[PassWord] [nvarchar](100) NOT NULL,
	[Member_ID] [int] NOT NULL,
	[Type] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bill]    Script Date: 10/31/2022 10:59:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bill](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Date] [date] NOT NULL,
	[Table_ID] [int] NOT NULL,
	[Staff_ID] [int] NOT NULL,
	[status] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BillInfo]    Script Date: 10/31/2022 10:59:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BillInfo](
	[Bill_ID] [int] NOT NULL,
	[Menu_ID] [int] NOT NULL,
	[Number] [tinyint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Bill_ID] ASC,
	[Menu_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Menu]    Script Date: 10/31/2022 10:59:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Price] [float] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Staff]    Script Date: 10/31/2022 10:59:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Staff](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Sex] [bit] NOT NULL,
	[Address] [nvarchar](100) NOT NULL,
	[PhoneNumber] [varchar](15) NOT NULL,
	[Birthday] [date] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TableList]    Script Date: 10/31/2022 10:59:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TableList](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](10) NOT NULL,
	[status] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Account] ([UserName], [DisplayName], [PassWord], [Member_ID], [Type]) VALUES (N'admin', N'Nguyễn Thị Thu Hà', N'12345678', 1, 1)
INSERT [dbo].[Account] ([UserName], [DisplayName], [PassWord], [Member_ID], [Type]) VALUES (N'staff1', N'Nguyễn Thị Thu Hà', N'00000000', 2, 0)
GO
SET IDENTITY_INSERT [dbo].[Bill] ON 

INSERT [dbo].[Bill] ([ID], [Date], [Table_ID], [Staff_ID], [status]) VALUES (1, CAST(N'2022-10-30' AS Date), 1, 2, 1)
INSERT [dbo].[Bill] ([ID], [Date], [Table_ID], [Staff_ID], [status]) VALUES (2, CAST(N'2022-10-31' AS Date), 4, 2, 0)
INSERT [dbo].[Bill] ([ID], [Date], [Table_ID], [Staff_ID], [status]) VALUES (3, CAST(N'2022-10-31' AS Date), 5, 2, 0)
SET IDENTITY_INSERT [dbo].[Bill] OFF
GO
INSERT [dbo].[BillInfo] ([Bill_ID], [Menu_ID], [Number]) VALUES (1, 1, 2)
INSERT [dbo].[BillInfo] ([Bill_ID], [Menu_ID], [Number]) VALUES (1, 2, 1)
INSERT [dbo].[BillInfo] ([Bill_ID], [Menu_ID], [Number]) VALUES (2, 1, 1)
INSERT [dbo].[BillInfo] ([Bill_ID], [Menu_ID], [Number]) VALUES (2, 3, 1)
GO
SET IDENTITY_INSERT [dbo].[Menu] ON 

INSERT [dbo].[Menu] ([ID], [Name], [Price]) VALUES (1, N'Trà sữa thái xanh', 18000)
INSERT [dbo].[Menu] ([ID], [Name], [Price]) VALUES (2, N'Trà tắc', 10000)
INSERT [dbo].[Menu] ([ID], [Name], [Price]) VALUES (3, N'Chân gà sả tắc', 35000)
SET IDENTITY_INSERT [dbo].[Menu] OFF
GO
SET IDENTITY_INSERT [dbo].[Staff] ON 

INSERT [dbo].[Staff] ([ID], [Name], [Sex], [Address], [PhoneNumber], [Birthday]) VALUES (1, N'Nguyễn Thị Thu Hà', 1, N'294 Nguyễn Lương Bằng, Liên Chiểu, Đà Nẵng', N'0762668222', CAST(N'2003-01-01' AS Date))
INSERT [dbo].[Staff] ([ID], [Name], [Sex], [Address], [PhoneNumber], [Birthday]) VALUES (2, N'Nguyễn Thị Thu Hà', 0, N'Nguyễn Chánh, Liên Chiểu, Đà Nẵng', N'0988274803', CAST(N'2003-03-25' AS Date))
SET IDENTITY_INSERT [dbo].[Staff] OFF
GO
SET IDENTITY_INSERT [dbo].[TableList] ON 

INSERT [dbo].[TableList] ([ID], [Name], [status]) VALUES (1, N'1', 0)
INSERT [dbo].[TableList] ([ID], [Name], [status]) VALUES (2, N'2', 0)
INSERT [dbo].[TableList] ([ID], [Name], [status]) VALUES (3, N'3', 0)
INSERT [dbo].[TableList] ([ID], [Name], [status]) VALUES (4, N'4', 1)
INSERT [dbo].[TableList] ([ID], [Name], [status]) VALUES (5, N'5', 0)
INSERT [dbo].[TableList] ([ID], [Name], [status]) VALUES (6, N'6', 0)
INSERT [dbo].[TableList] ([ID], [Name], [status]) VALUES (7, N'7', 0)
INSERT [dbo].[TableList] ([ID], [Name], [status]) VALUES (8, N'8', 0)
INSERT [dbo].[TableList] ([ID], [Name], [status]) VALUES (9, N'9', 0)
INSERT [dbo].[TableList] ([ID], [Name], [status]) VALUES (10, N'10', 0)
INSERT [dbo].[TableList] ([ID], [Name], [status]) VALUES (11, N'11', 0)
INSERT [dbo].[TableList] ([ID], [Name], [status]) VALUES (12, N'VIP', 0)
SET IDENTITY_INSERT [dbo].[TableList] OFF
GO
ALTER TABLE [dbo].[Account] ADD  DEFAULT ((0)) FOR [Type]
GO
ALTER TABLE [dbo].[Bill] ADD  DEFAULT (getdate()) FOR [Date]
GO
ALTER TABLE [dbo].[Bill] ADD  DEFAULT ((0)) FOR [status]
GO
ALTER TABLE [dbo].[BillInfo] ADD  DEFAULT ((0)) FOR [Number]
GO
ALTER TABLE [dbo].[Menu] ADD  DEFAULT ((0)) FOR [Price]
GO
ALTER TABLE [dbo].[TableList] ADD  DEFAULT ((0)) FOR [status]
GO
ALTER TABLE [dbo].[Account]  WITH CHECK ADD FOREIGN KEY([Member_ID])
REFERENCES [dbo].[Staff] ([ID])
GO
ALTER TABLE [dbo].[Bill]  WITH CHECK ADD FOREIGN KEY([Staff_ID])
REFERENCES [dbo].[Staff] ([ID])
GO
ALTER TABLE [dbo].[Bill]  WITH CHECK ADD FOREIGN KEY([Table_ID])
REFERENCES [dbo].[TableList] ([ID])
GO
ALTER TABLE [dbo].[BillInfo]  WITH CHECK ADD FOREIGN KEY([Bill_ID])
REFERENCES [dbo].[Bill] ([ID])
GO
ALTER TABLE [dbo].[BillInfo]  WITH CHECK ADD FOREIGN KEY([Menu_ID])
REFERENCES [dbo].[Menu] ([ID])
GO
/****** Object:  StoredProcedure [dbo].[USP_GetOrderList]    Script Date: 10/31/2022 10:59:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[USP_GetOrderList]
@idTable int
as begin
select Name, Number, Price, Price*Number as TotalPrice from dbo.Menu
join dbo.BillInfo on dbo.Menu.ID = dbo.BillInfo.Menu_ID
join dbo.Bill on dbo.Bill.ID = dbo.BillInfo.Bill_ID
where dbo.Bill.Table_ID = @idTable and status = 0
end
GO
/****** Object:  StoredProcedure [dbo].[USP_GetTableList]    Script Date: 10/31/2022 10:59:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[USP_GetTableList]
as select * from dbo.TableList
GO
/****** Object:  StoredProcedure [dbo].[USP_InsertBill]    Script Date: 10/31/2022 10:59:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[USP_InsertBill]
@idTable int, @idStaff int
as begin
	insert dbo.Bill (Date, Table_ID, Staff_ID)
	values
	(
		GETDATE(),
		@idTable,
		@idStaff
	)
end
GO
/****** Object:  StoredProcedure [dbo].[USP_InsertBillInfo]    Script Date: 10/31/2022 10:59:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[USP_InsertBillInfo]
@idBill int, @idMenu int, @number tinyint
as begin
	declare @isExistBillInfo int
	select @isExistBillInfo = count(*) from dbo.BillInfo where @idBill = Bill_ID and @idMenu = Menu_ID
	if @isExistBillInfo > 0
	begin
		if (@number > 0) 
			update dbo.BillInfo set Number = @number where @idMenu = Menu_ID
		else delete dbo.BillInfo where @idBill = Bill_ID and @idMenu = Menu_ID
	end
	else if @number > 0 
	insert dbo.BillInfo (Bill_ID, Menu_ID, Number)
	values
	(
		@idBill,
		@idMenu,
		@number
	)
end
GO
/****** Object:  StoredProcedure [dbo].[USP_Login]    Script Date: 10/31/2022 10:59:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[USP_Login] @username nvarchar(100), @password nvarchar(100) 
as begin
	select Member_ID, Type from dbo.Account where UserName = @username collate SQL_Latin1_General_CP1_CS_AS
	and PassWord = @password collate SQL_Latin1_General_CP1_CS_AS
end
GO
/****** Object:  StoredProcedure [dbo].[USP_Report]    Script Date: 10/31/2022 10:59:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[USP_Report] @startDay Date, @endDay Date
as begin
	select dbo.BillInfo.Bill_ID, dbo.Bill.Date as [Ngày], dbo.TableList.Name as [Bàn], sum(dbo.Menu.Price * dbo.BillInfo.Number) as [Tổng tiền]
	from dbo.Bill 
	join dbo.BillInfo on dbo.BillInfo.Bill_ID = dbo.Bill.ID
	join dbo.TableList on dbo.Bill.Table_ID = dbo.TableList.ID
	join dbo.Menu on dbo.BillInfo.Menu_ID = dbo.Menu.ID
	where dbo.Bill.status = 1
	and dbo.Bill.Date>=@startDay and dbo.Bill.Date <= @endDay
	group by dbo.BillInfo.Bill_ID, dbo.Bill.Date, dbo.TableList.Name
end
GO
/****** Object:  StoredProcedure [dbo].[USP_SaveBill]    Script Date: 10/31/2022 10:59:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[USP_SaveBill] @idTable int
as begin
	update dbo.Bill
	set status = 1 , Date = GETDATE()
	where @idTable = Table_ID and status = 0
end
GO
/****** Object:  StoredProcedure [dbo].[USP_SetTableStatus]    Script Date: 10/31/2022 10:59:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------
create proc [dbo].[USP_SetTableStatus] @idTable int, @status bit
as begin
	update dbo.TableList
	set status = @status
	where ID = @idTable
end
GO
/****** Object:  StoredProcedure [dbo].[USP_Trend]    Script Date: 10/31/2022 10:59:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[USP_Trend] @startDay Date, @endDay Date
as begin
	select dbo.Menu.Name as [Tên sản phẩm], dbo.Menu.Price as [Giá bán], sum(dbo.BillInfo.Number) as [Tổng số lượng]
	from dbo.Bill 
	join dbo.BillInfo on dbo.BillInfo.Bill_ID = dbo.Bill.ID
	join dbo.TableList on dbo.Bill.Table_ID = dbo.TableList.ID
	join dbo.Menu on dbo.BillInfo.Menu_ID = dbo.Menu.ID
	where dbo.Bill.status = 1
	--and dbo.Bill.Date>=@startDay and dbo.Bill.Date <= @endDay
	group by dbo.Menu.Name, dbo.Menu.Price
	order by sum(dbo.BillInfo.Number) desc
end
GO
