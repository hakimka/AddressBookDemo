USE [ToyotaDemo]
GO

/****** Object:  Table [dbo].[tblCustomer]    Script Date: 1/4/2023 2:14:58 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblCustomer](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[firstName] [varchar](200) NOT NULL,
	[lastName] [varchar](200) NOT NULL,
	[emailAddress] [varchar](50) NOT NULL,
	[postAddressStreet] [varchar](200) NULL,
	[phoneNumber] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

