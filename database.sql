CREATE DATABASE PerformancePresentation;
GO
USE [PerformancePresentation]
GO
/****** Object:  Table [dbo].[Websites]    Script Date: 22-May-17 22:57:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Websites](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Url] [varchar](50) NOT NULL,
	[IsDone] [bit] NOT NULL,
 CONSTRAINT [PK_Websites] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Websites] ON 
SET IDENTITY_INSERT [dbo].[Websites] OFF
