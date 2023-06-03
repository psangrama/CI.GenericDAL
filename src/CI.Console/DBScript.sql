CREATE DATABASE CIGenericDALDB;

USE CIGenericDALDB;

CREATE TABLE [dbo].[UserInfo](
	[ID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[FullName] [varchar](64) NOT NULL,
	[Email] [varchar](100) NULL,
	[Mobile] [char](10) NULL,
	[UserAdded] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL
)

INSERT [dbo].[UserInfo] ([FullName], [Email], [Mobile], [UserAdded], [IsActive]) VALUES (N'Mr. Narendra Modi', N'NModi@Email.com', '', GETDATE(), 1)
INSERT [dbo].[UserInfo] ([FullName], [Email], [Mobile], [UserAdded], [IsActive]) VALUES (N'Mr. S. Jaishankar', N'Jai@Email.com', '', GETDATE(), 1)
INSERT [dbo].[UserInfo] ([FullName], [Email], [Mobile], [UserAdded], [IsActive]) VALUES (N'Ajit D.', N'Ajit@Email.com', '' ,GETDATE(), 1)

SELECT * FROM [UserInfo]
