USE [master]
GO
/****** Object:  Database [OperaHouseManagement]    Script Date: marți 16.apr.2024 7:10:51 AM ******/
CREATE DATABASE [OperaHouseManagement]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'OperaHouseManagement', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS01\MSSQL\DATA\OperaHouseManagement.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'OperaHouseManagement_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS01\MSSQL\DATA\OperaHouseManagement_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [OperaHouseManagement] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [OperaHouseManagement].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [OperaHouseManagement] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [OperaHouseManagement] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [OperaHouseManagement] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [OperaHouseManagement] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [OperaHouseManagement] SET ARITHABORT OFF 
GO
ALTER DATABASE [OperaHouseManagement] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [OperaHouseManagement] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [OperaHouseManagement] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [OperaHouseManagement] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [OperaHouseManagement] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [OperaHouseManagement] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [OperaHouseManagement] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [OperaHouseManagement] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [OperaHouseManagement] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [OperaHouseManagement] SET  DISABLE_BROKER 
GO
ALTER DATABASE [OperaHouseManagement] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [OperaHouseManagement] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [OperaHouseManagement] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [OperaHouseManagement] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [OperaHouseManagement] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [OperaHouseManagement] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [OperaHouseManagement] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [OperaHouseManagement] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [OperaHouseManagement] SET  MULTI_USER 
GO
ALTER DATABASE [OperaHouseManagement] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [OperaHouseManagement] SET DB_CHAINING OFF 
GO
ALTER DATABASE [OperaHouseManagement] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [OperaHouseManagement] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [OperaHouseManagement] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [OperaHouseManagement] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [OperaHouseManagement] SET QUERY_STORE = ON
GO
ALTER DATABASE [OperaHouseManagement] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [OperaHouseManagement]
GO
/****** Object:  Table [dbo].[Seats]    Script Date: marți 16.apr.2024 7:10:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Seats](
	[SeatId] [int] IDENTITY(1,1) NOT NULL,
	[ZoneId] [varchar](50) NOT NULL,
	[Number] [int] NOT NULL,
	[IsOccupied] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SeatId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tickets]    Script Date: marți 16.apr.2024 7:10:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tickets](
	[TicketId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ZoneId] [varchar](50) NOT NULL,
	[TotalPrice] [decimal](10, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TicketId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TicketSeats]    Script Date: marți 16.apr.2024 7:10:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TicketSeats](
	[TicketId] [int] NOT NULL,
	[SeatId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TicketId] ASC,
	[SeatId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: marți 16.apr.2024 7:10:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](100) NOT NULL,
	[PasswordHash] [varchar](255) NOT NULL,
	[UserType] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Zones]    Script Date: marți 16.apr.2024 7:10:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Zones](
	[ZoneId] [varchar](50) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Price] [decimal](10, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ZoneId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Seats] ADD  DEFAULT ((0)) FOR [IsOccupied]
GO
ALTER TABLE [dbo].[Seats]  WITH CHECK ADD FOREIGN KEY([ZoneId])
REFERENCES [dbo].[Zones] ([ZoneId])
GO
ALTER TABLE [dbo].[Tickets]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Tickets]  WITH CHECK ADD FOREIGN KEY([ZoneId])
REFERENCES [dbo].[Zones] ([ZoneId])
GO
ALTER TABLE [dbo].[TicketSeats]  WITH CHECK ADD FOREIGN KEY([SeatId])
REFERENCES [dbo].[Seats] ([SeatId])
GO
ALTER TABLE [dbo].[TicketSeats]  WITH CHECK ADD FOREIGN KEY([TicketId])
REFERENCES [dbo].[Tickets] ([TicketId])
GO
USE [master]
GO
ALTER DATABASE [OperaHouseManagement] SET  READ_WRITE 
GO
