USE [master]
GO
/****** Object:  Database [DrisionMVCFrame]    Script Date: 05/14/2013 18:01:45 ******/
CREATE DATABASE [DrisionMVCFrame] ON  PRIMARY 
( NAME = N'DrisionMVCFrame', FILENAME = N'E:\Soft\Microsoft\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\DrisionMVCFrame.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'DrisionMVCFrame_log', FILENAME = N'E:\Soft\Microsoft\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\DrisionMVCFrame_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [DrisionMVCFrame] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DrisionMVCFrame].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DrisionMVCFrame] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [DrisionMVCFrame] SET ANSI_NULLS OFF
GO
ALTER DATABASE [DrisionMVCFrame] SET ANSI_PADDING OFF
GO
ALTER DATABASE [DrisionMVCFrame] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [DrisionMVCFrame] SET ARITHABORT OFF
GO
ALTER DATABASE [DrisionMVCFrame] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [DrisionMVCFrame] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [DrisionMVCFrame] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [DrisionMVCFrame] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [DrisionMVCFrame] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [DrisionMVCFrame] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [DrisionMVCFrame] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [DrisionMVCFrame] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [DrisionMVCFrame] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [DrisionMVCFrame] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [DrisionMVCFrame] SET  DISABLE_BROKER
GO
ALTER DATABASE [DrisionMVCFrame] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [DrisionMVCFrame] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [DrisionMVCFrame] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [DrisionMVCFrame] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [DrisionMVCFrame] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [DrisionMVCFrame] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [DrisionMVCFrame] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [DrisionMVCFrame] SET  READ_WRITE
GO
ALTER DATABASE [DrisionMVCFrame] SET RECOVERY FULL
GO
ALTER DATABASE [DrisionMVCFrame] SET  MULTI_USER
GO
ALTER DATABASE [DrisionMVCFrame] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [DrisionMVCFrame] SET DB_CHAINING OFF
GO
EXEC sys.sp_db_vardecimal_storage_format N'DrisionMVCFrame', N'ON'
GO
USE [DrisionMVCFrame]
GO
/****** Object:  User [DRISION\hejin.wang]    Script Date: 05/14/2013 18:01:45 ******/
CREATE USER [DRISION\hejin.wang] FOR LOGIN [DRISION\hejin.wang] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[SysUser]    Script Date: 05/14/2013 18:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysUser](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](128) NOT NULL,
	[NickName] [nvarchar](128) NOT NULL,
	[Password] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](100) NULL,
	[CreateTime] [datetime] NOT NULL,
	[LastLoginTime] [datetime] NULL,
	[DelFlag] [int] NOT NULL,
 CONSTRAINT [PK_SysUser] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SysRole]    Script Date: 05/14/2013 18:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysRole](
	[RoleID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](30) NOT NULL,
	[RoleDescription] [nvarchar](50) NULL,
	[RoleOrder] [int] NOT NULL,
	[DelFlag] [int] NOT NULL,
 CONSTRAINT [PK_SysRole] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SysGroup]    Script Date: 05/14/2013 18:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysGroup](
	[GroupID] [int] IDENTITY(1,1) NOT NULL,
	[GroupName] [nvarchar](30) NOT NULL,
	[GroupOrder] [int] NOT NULL,
	[GroupDescription] [nvarchar](50) NULL,
	[GroupType] [int] NOT NULL,
	[DelFlag] [int] NOT NULL,
 CONSTRAINT [PK_SysGroup] PRIMARY KEY CLUSTERED 
(
	[GroupID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SysAction]    Script Date: 05/14/2013 18:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[SysAction](
	[ActionID] [int] IDENTITY(1,1) NOT NULL,
	[RequestUrl] [varchar](256) NOT NULL,
	[RequestHttpType] [varchar](16) NOT NULL,
	[ActionName] [nvarchar](50) NOT NULL,
	[SubTime] [datetime] NOT NULL,
	[ActionType] [int] NOT NULL,
	[DelFlag] [int] NOT NULL,
 CONSTRAINT [PK_SysAction] PRIMARY KEY CLUSTERED 
(
	[ActionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Sys_R_User_Role]    Script Date: 05/14/2013 18:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sys_R_User_Role](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[RoleID] [int] NOT NULL,
 CONSTRAINT [PK_R_User_Role] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sys_R_User_Group]    Script Date: 05/14/2013 18:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sys_R_User_Group](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[GroupID] [int] NOT NULL,
 CONSTRAINT [PK_Sys_R_User_Group] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sys_R_User_Action]    Script Date: 05/14/2013 18:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sys_R_User_Action](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[ActionID] [int] NOT NULL,
 CONSTRAINT [PK_Sys_R_User_Action] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sys_R_Role_Action]    Script Date: 05/14/2013 18:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sys_R_Role_Action](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RoleID] [int] NOT NULL,
	[ActionID] [int] NOT NULL,
 CONSTRAINT [PK_Sys_R_Role_Action] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sys_R_Group_Action]    Script Date: 05/14/2013 18:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sys_R_Group_Action](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[GroupID] [int] NOT NULL,
	[ActionID] [int] NOT NULL,
 CONSTRAINT [PK_Sys_R_Group_Action] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Default [DF_SysUser_DelFlag]    Script Date: 05/14/2013 18:01:48 ******/
ALTER TABLE [dbo].[SysUser] ADD  CONSTRAINT [DF_SysUser_DelFlag]  DEFAULT ((0)) FOR [DelFlag]
GO
/****** Object:  Default [DF_SysRole_DelFlag]    Script Date: 05/14/2013 18:01:48 ******/
ALTER TABLE [dbo].[SysRole] ADD  CONSTRAINT [DF_SysRole_DelFlag]  DEFAULT ((0)) FOR [DelFlag]
GO
/****** Object:  Default [DF_SysGroup_DelFlag]    Script Date: 05/14/2013 18:01:48 ******/
ALTER TABLE [dbo].[SysGroup] ADD  CONSTRAINT [DF_SysGroup_DelFlag]  DEFAULT ((0)) FOR [DelFlag]
GO
/****** Object:  Default [DF_SysAction_DelFlag]    Script Date: 05/14/2013 18:01:48 ******/
ALTER TABLE [dbo].[SysAction] ADD  CONSTRAINT [DF_SysAction_DelFlag]  DEFAULT ((0)) FOR [DelFlag]
GO
/****** Object:  ForeignKey [FK_Sys_R_User_Role_SysRole]    Script Date: 05/14/2013 18:01:48 ******/
ALTER TABLE [dbo].[Sys_R_User_Role]  WITH CHECK ADD  CONSTRAINT [FK_Sys_R_User_Role_SysRole] FOREIGN KEY([RoleID])
REFERENCES [dbo].[SysRole] ([RoleID])
GO
ALTER TABLE [dbo].[Sys_R_User_Role] CHECK CONSTRAINT [FK_Sys_R_User_Role_SysRole]
GO
/****** Object:  ForeignKey [FK_Sys_R_User_Role_SysUser]    Script Date: 05/14/2013 18:01:48 ******/
ALTER TABLE [dbo].[Sys_R_User_Role]  WITH CHECK ADD  CONSTRAINT [FK_Sys_R_User_Role_SysUser] FOREIGN KEY([UserID])
REFERENCES [dbo].[SysUser] ([UserID])
GO
ALTER TABLE [dbo].[Sys_R_User_Role] CHECK CONSTRAINT [FK_Sys_R_User_Role_SysUser]
GO
/****** Object:  ForeignKey [FK_Sys_R_User_Group_SysGroup]    Script Date: 05/14/2013 18:01:48 ******/
ALTER TABLE [dbo].[Sys_R_User_Group]  WITH CHECK ADD  CONSTRAINT [FK_Sys_R_User_Group_SysGroup] FOREIGN KEY([GroupID])
REFERENCES [dbo].[SysGroup] ([GroupID])
GO
ALTER TABLE [dbo].[Sys_R_User_Group] CHECK CONSTRAINT [FK_Sys_R_User_Group_SysGroup]
GO
/****** Object:  ForeignKey [FK_Sys_R_User_Group_SysUser]    Script Date: 05/14/2013 18:01:48 ******/
ALTER TABLE [dbo].[Sys_R_User_Group]  WITH CHECK ADD  CONSTRAINT [FK_Sys_R_User_Group_SysUser] FOREIGN KEY([UserID])
REFERENCES [dbo].[SysUser] ([UserID])
GO
ALTER TABLE [dbo].[Sys_R_User_Group] CHECK CONSTRAINT [FK_Sys_R_User_Group_SysUser]
GO
/****** Object:  ForeignKey [FK_Sys_R_User_Action_SysAction]    Script Date: 05/14/2013 18:01:48 ******/
ALTER TABLE [dbo].[Sys_R_User_Action]  WITH CHECK ADD  CONSTRAINT [FK_Sys_R_User_Action_SysAction] FOREIGN KEY([ActionID])
REFERENCES [dbo].[SysAction] ([ActionID])
GO
ALTER TABLE [dbo].[Sys_R_User_Action] CHECK CONSTRAINT [FK_Sys_R_User_Action_SysAction]
GO
/****** Object:  ForeignKey [FK_Sys_R_User_Action_SysUser]    Script Date: 05/14/2013 18:01:48 ******/
ALTER TABLE [dbo].[Sys_R_User_Action]  WITH CHECK ADD  CONSTRAINT [FK_Sys_R_User_Action_SysUser] FOREIGN KEY([UserID])
REFERENCES [dbo].[SysUser] ([UserID])
GO
ALTER TABLE [dbo].[Sys_R_User_Action] CHECK CONSTRAINT [FK_Sys_R_User_Action_SysUser]
GO
/****** Object:  ForeignKey [FK_Sys_R_Role_Action_SysAction]    Script Date: 05/14/2013 18:01:48 ******/
ALTER TABLE [dbo].[Sys_R_Role_Action]  WITH CHECK ADD  CONSTRAINT [FK_Sys_R_Role_Action_SysAction] FOREIGN KEY([ActionID])
REFERENCES [dbo].[SysAction] ([ActionID])
GO
ALTER TABLE [dbo].[Sys_R_Role_Action] CHECK CONSTRAINT [FK_Sys_R_Role_Action_SysAction]
GO
/****** Object:  ForeignKey [FK_Sys_R_Role_Action_SysRole]    Script Date: 05/14/2013 18:01:48 ******/
ALTER TABLE [dbo].[Sys_R_Role_Action]  WITH CHECK ADD  CONSTRAINT [FK_Sys_R_Role_Action_SysRole] FOREIGN KEY([RoleID])
REFERENCES [dbo].[SysRole] ([RoleID])
GO
ALTER TABLE [dbo].[Sys_R_Role_Action] CHECK CONSTRAINT [FK_Sys_R_Role_Action_SysRole]
GO
/****** Object:  ForeignKey [FK_Sys_R_Group_Action_SysAction]    Script Date: 05/14/2013 18:01:48 ******/
ALTER TABLE [dbo].[Sys_R_Group_Action]  WITH CHECK ADD  CONSTRAINT [FK_Sys_R_Group_Action_SysAction] FOREIGN KEY([ActionID])
REFERENCES [dbo].[SysAction] ([ActionID])
GO
ALTER TABLE [dbo].[Sys_R_Group_Action] CHECK CONSTRAINT [FK_Sys_R_Group_Action_SysAction]
GO
/****** Object:  ForeignKey [FK_Sys_R_Group_Action_SysGroup]    Script Date: 05/14/2013 18:01:48 ******/
ALTER TABLE [dbo].[Sys_R_Group_Action]  WITH CHECK ADD  CONSTRAINT [FK_Sys_R_Group_Action_SysGroup] FOREIGN KEY([GroupID])
REFERENCES [dbo].[SysGroup] ([GroupID])
GO
ALTER TABLE [dbo].[Sys_R_Group_Action] CHECK CONSTRAINT [FK_Sys_R_Group_Action_SysGroup]
GO
