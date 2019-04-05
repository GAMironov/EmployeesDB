USE [DBTest]
GO

/****** Object:  Table [dbo].[Employees]    Script Date: 04.04.2019 23:26:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Employees](
	[ID] [int] IDENTITY(1001,1) NOT NULL,
	[Family] [text] NULL,
	[Name] [text] NULL,
	[MiddleName] [text] NULL,
	[Birthday] [datetime] NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 1) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

