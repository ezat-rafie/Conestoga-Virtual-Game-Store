/****** Object:  Database [CVGS]    Script Date: 2022-12-08 4:41:23 PM ******/
CREATE DATABASE [CVGS]
GO
ALTER DATABASE [CVGS] SET COMPATIBILITY_LEVEL = 150
GO
ALTER DATABASE [CVGS] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CVGS] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CVGS] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CVGS] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CVGS] SET ARITHABORT OFF 
GO
ALTER DATABASE [CVGS] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CVGS] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CVGS] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CVGS] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CVGS] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CVGS] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CVGS] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CVGS] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CVGS] SET ALLOW_SNAPSHOT_ISOLATION ON 
GO
ALTER DATABASE [CVGS] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CVGS] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [CVGS] SET  MULTI_USER 
GO
ALTER DATABASE [CVGS] SET QUERY_STORE = ON
GO
ALTER DATABASE [CVGS] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 100, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
/*** The scripts of database scoped configurations in Azure should be executed inside the target database connection. ***/
GO
-- ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 8;
GO

USE CVGS;
/****** Object:  Table [dbo].[Address]    Script Date: 2022-12-08 4:41:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Address](
	[AddressId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[FullName] [varchar](255) NULL,
	[AddressLine1] [varchar](255) NOT NULL,
	[AddressLine2] [varchar](255) NULL,
	[AddressLine3] [varchar](255) NULL,
	[City] [varchar](50) NOT NULL,
	[Province] [varchar](50) NOT NULL,
	[Postal] [varchar](6) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[AddressId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[ApprovalStatus]    Script Date: 2022-12-08 4:41:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApprovalStatus](
	[ApprovalStatusId] [int] IDENTITY(1,1) NOT NULL,
	[IconPictureId] [int] NOT NULL,
	[Name] [varchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ApprovalStatusId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Colour]    Script Date: 2022-12-08 4:41:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Colour](
	[ColourId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](30) NOT NULL,
	[Value] [varchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ColourId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CreditCard]    Script Date: 2022-12-08 4:41:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CreditCard](
	[CreditCardId] [int] IDENTITY(1,1) NOT NULL,
	[DisplayName] [varchar](30) NULL,
	[CardName] [varchar](30) NOT NULL,
	[CardNumber] [bigint] NOT NULL,
	[Expiry] [datetime] NOT NULL,
	[CVV] [int] NOT NULL,
	[BillingAddressId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[CreditCardId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Event]    Script Date: 2022-12-08 4:41:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Event](
	[EventId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](50) NOT NULL,
	[Description] [text] NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[Place] [varchar](50) NOT NULL,
	[Capacity] [int] NOT NULL,
	[Price] [money] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[EventId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EventRegistration]    Script Date: 2022-12-08 4:41:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EventRegistration](
	[EventId] [int] NOT NULL,
	[UserId] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Game]    Script Date: 2022-12-08 4:41:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Game](
	[ItemId] [int] NOT NULL,
	[PlatformId] [int] NOT NULL,
	[ESRBRating] [int] NULL,
	[Genreid] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Gender]    Script Date: 2022-12-08 4:41:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gender](
	[GenderId] [int] IDENTITY(1,1) NOT NULL,
	[DisplayName] [varchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[GenderId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Genre]    Script Date: 2022-12-08 4:41:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Genre](
	[GenreId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Type] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[GenreId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GenreType]    Script Date: 2022-12-08 4:41:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GenreType](
	[GenreTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[GenreTypeId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Invoice]    Script Date: 2022-12-08 4:41:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoice](
	[InvoiceId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[StatusId] [int] NOT NULL,
	[CreditCardId] [int] NOT NULL,
	[AddressId] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Paid] [bit] NOT NULL,
	[BillingAddressId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[InvoiceId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InvoiceItem]    Script Date: 2022-12-08 4:41:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvoiceItem](
	[ItemId] [int] NOT NULL,
	[InvoiceId] [int] NOT NULL,
	[InvoiceLine] [int] NOT NULL,
	[InvoicePrice] [money] NOT NULL,
	[Quantity] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Item]    Script Date: 2022-12-08 4:41:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Item](
	[ItemId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Description] [varchar](50) NULL,
	[Price] [money] NOT NULL,
	[QuantityInStock] [int] NOT NULL,
	[GameTag] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ItemId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ItemGenre]    Script Date: 2022-12-08 4:41:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemGenre](
	[ItemId] [int] NOT NULL,
	[GenreId] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ItemPicture]    Script Date: 2022-12-08 4:41:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemPicture](
	[ItemId] [int] NOT NULL,
	[PictureId] [int] NOT NULL,
	[Index] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ItemRating]    Script Date: 2022-12-08 4:41:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemRating](
	[ItemId] [int] NOT NULL,
	[RatingId] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ItemReview]    Script Date: 2022-12-08 4:41:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemReview](
	[ItemId] [int] NOT NULL,
	[ReviewId] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LinkedUser(FriendsFamily)]    Script Date: 2022-12-08 4:41:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LinkedUser(FriendsFamily)](
	[RequesterUserId] [int] NOT NULL,
	[RequesteeUserId] [int] NOT NULL,
	[Approved] [bit] NOT NULL,
	[Blocked] [bit] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Merchandise]    Script Date: 2022-12-08 4:41:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Merchandise](
	[ItemId] [int] NOT NULL,
	[ColourId] [int] NOT NULL,
	[Size] [varchar](20) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Picture]    Script Date: 2022-12-08 4:41:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Picture](
	[PictureId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](30) NOT NULL,
	[Description] [varchar](30) NULL,
	[Location] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PictureId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Platform]    Script Date: 2022-12-08 4:41:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Platform](
	[PlatformId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PlatformId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rating]    Script Date: 2022-12-08 4:41:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rating](
	[RatingId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[RatingValue] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RatingId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Report]    Script Date: 2022-12-08 4:41:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Report](
	[ReportId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Script] [varchar](3000) NOT NULL,
	[ChartTitle] [varchar](50) NULL,
	[ChartType] [varchar](50) NULL,
	[ChartXLabel] [varchar](50) NULL,
	[ChartYLabel] [varchar](50) NULL,
	[ChartXColumn] [varchar](50) NULL,
	[ChartYColumn] [varchar](50) NULL,
	[ColumnFormats] [varchar](1000) NULL,
	[Description] [varchar](255) NULL,
 CONSTRAINT [PK_Report] PRIMARY KEY CLUSTERED 
(
	[ReportId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Review]    Script Date: 2022-12-08 4:41:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Review](
	[ReviewId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ApprovalUserId] [int] NOT NULL,
	[ApprovalStatusId] [int] NOT NULL,
	[ApprovalDate] [datetime] NULL,
	[WrittenReview] [text] NOT NULL,
	[RatingId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ReviewId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Status]    Script Date: 2022-12-08 4:41:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Status](
	[StatusId] [int] IDENTITY(1,1) NOT NULL,
	[IconPictureId] [int] NOT NULL,
	[DisplayName] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[StatusId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[sysdiagrams]    Script Date: 2022-12-08 4:41:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sysdiagrams](
	[name] [sysname] NOT NULL,
	[principal_id] [int] NOT NULL,
	[diagram_id] [int] IDENTITY(1,1) NOT NULL,
	[version] [int] NULL,
	[definition] [varbinary](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[diagram_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UK_principal_name] UNIQUE NONCLUSTERED 
(
	[principal_id] ASC,
	[name] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 2022-12-08 4:41:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserTypeId] [int] NOT NULL,
	[EmailAddress] [varchar](50) NOT NULL,
	[Password] [varchar](40) NOT NULL,
	[EmailValidationToken] [varchar](50) NULL,
	[EmailValid] [bit] NOT NULL,
	[LoginAttempts] [int] NOT NULL,
	[DisplayName] [varchar](30) NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[BirthDate] [datetime] NULL,
	[ReceivePromotional] [bit] NOT NULL,
	[GenderId] [int] NULL,
	[MailingAddressId] [int] NULL,
	[ShippingAddresId] [int] NULL,
	[PlatformId] [int] NULL,
	[GenreId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserCreditCard]    Script Date: 2022-12-08 4:41:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserCreditCard](
	[UserId] [int] NOT NULL,
	[CreditCardId] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserGenre]    Script Date: 2022-12-08 4:41:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserGenre](
	[UserId] [int] NOT NULL,
	[GenreId] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserItem]    Script Date: 2022-12-08 4:41:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserItem](
	[UserId] [int] NOT NULL,
	[ItemId] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserPlatform]    Script Date: 2022-12-08 4:41:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserPlatform](
	[UserId] [int] NOT NULL,
	[PlatformId] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserType]    Script Date: 2022-12-08 4:41:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserType](
	[UserTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](30) NOT NULL,
	[IconPictureId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserTypeId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Event] ADD  DEFAULT ((0)) FOR [Price]
GO
ALTER TABLE [dbo].[Invoice] ADD  DEFAULT ((0)) FOR [Paid]
GO
ALTER TABLE [dbo].[InvoiceItem] ADD  DEFAULT ((0)) FOR [InvoicePrice]
GO
ALTER TABLE [dbo].[InvoiceItem] ADD  DEFAULT ((0)) FOR [Quantity]
GO
ALTER TABLE [dbo].[User] ADD  DEFAULT ((0)) FOR [EmailValid]
GO
ALTER TABLE [dbo].[User] ADD  DEFAULT ((0)) FOR [LoginAttempts]
GO
ALTER TABLE [dbo].[User] ADD  DEFAULT ((1)) FOR [ReceivePromotional]
GO
ALTER TABLE [dbo].[ApprovalStatus]  WITH CHECK ADD  CONSTRAINT [FKApprovalSt456222] FOREIGN KEY([IconPictureId])
REFERENCES [dbo].[Picture] ([PictureId])
GO
ALTER TABLE [dbo].[ApprovalStatus] CHECK CONSTRAINT [FKApprovalSt456222]
GO
ALTER TABLE [dbo].[EventRegistration]  WITH CHECK ADD  CONSTRAINT [FKUser999998] FOREIGN KEY([EventId])
REFERENCES [dbo].[Event] ([EventId])
GO
ALTER TABLE [dbo].[EventRegistration] CHECK CONSTRAINT [FKUser999998]
GO
ALTER TABLE [dbo].[EventRegistration]  WITH CHECK ADD  CONSTRAINT [FKUser999999] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[EventRegistration] CHECK CONSTRAINT [FKUser999999]
GO
ALTER TABLE [dbo].[Game]  WITH CHECK ADD  CONSTRAINT [FKGame631635] FOREIGN KEY([PlatformId])
REFERENCES [dbo].[Platform] ([PlatformId])
GO
ALTER TABLE [dbo].[Game] CHECK CONSTRAINT [FKGame631635]
GO
ALTER TABLE [dbo].[Game]  WITH CHECK ADD  CONSTRAINT [FKGame879054] FOREIGN KEY([ItemId])
REFERENCES [dbo].[Item] ([ItemId])
GO
ALTER TABLE [dbo].[Game] CHECK CONSTRAINT [FKGame879054]
GO
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD FOREIGN KEY([BillingAddressId])
REFERENCES [dbo].[Address] ([AddressId])
GO
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FKInvoice170879] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FKInvoice170879]
GO
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FKInvoice246451] FOREIGN KEY([StatusId])
REFERENCES [dbo].[Status] ([StatusId])
GO
ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FKInvoice246451]
GO
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FKInvoice896166] FOREIGN KEY([CreditCardId])
REFERENCES [dbo].[CreditCard] ([CreditCardId])
GO
ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FKInvoice896166]
GO
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FKInvoice902427] FOREIGN KEY([AddressId])
REFERENCES [dbo].[Address] ([AddressId])
GO
ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FKInvoice902427]
GO
ALTER TABLE [dbo].[InvoiceItem]  WITH CHECK ADD  CONSTRAINT [FKInvoiceIte202656] FOREIGN KEY([ItemId])
REFERENCES [dbo].[Item] ([ItemId])
GO
ALTER TABLE [dbo].[InvoiceItem] CHECK CONSTRAINT [FKInvoiceIte202656]
GO
ALTER TABLE [dbo].[InvoiceItem]  WITH CHECK ADD  CONSTRAINT [FKInvoiceIte470389] FOREIGN KEY([InvoiceId])
REFERENCES [dbo].[Invoice] ([InvoiceId])
GO
ALTER TABLE [dbo].[InvoiceItem] CHECK CONSTRAINT [FKInvoiceIte470389]
GO
ALTER TABLE [dbo].[ItemGenre]  WITH CHECK ADD  CONSTRAINT [FKItemGenre129685] FOREIGN KEY([ItemId])
REFERENCES [dbo].[Item] ([ItemId])
GO
ALTER TABLE [dbo].[ItemGenre] CHECK CONSTRAINT [FKItemGenre129685]
GO
ALTER TABLE [dbo].[ItemGenre]  WITH CHECK ADD  CONSTRAINT [FKItemGenre318865] FOREIGN KEY([GenreId])
REFERENCES [dbo].[Genre] ([GenreId])
GO
ALTER TABLE [dbo].[ItemGenre] CHECK CONSTRAINT [FKItemGenre318865]
GO
ALTER TABLE [dbo].[ItemPicture]  WITH CHECK ADD  CONSTRAINT [FKItemPictur641318] FOREIGN KEY([PictureId])
REFERENCES [dbo].[Picture] ([PictureId])
GO
ALTER TABLE [dbo].[ItemPicture] CHECK CONSTRAINT [FKItemPictur641318]
GO
ALTER TABLE [dbo].[ItemPicture]  WITH CHECK ADD  CONSTRAINT [FKItemPictur877489] FOREIGN KEY([ItemId])
REFERENCES [dbo].[Item] ([ItemId])
GO
ALTER TABLE [dbo].[ItemPicture] CHECK CONSTRAINT [FKItemPictur877489]
GO
ALTER TABLE [dbo].[ItemRating]  WITH CHECK ADD  CONSTRAINT [FKItemRating667120] FOREIGN KEY([ItemId])
REFERENCES [dbo].[Item] ([ItemId])
GO
ALTER TABLE [dbo].[ItemRating] CHECK CONSTRAINT [FKItemRating667120]
GO
ALTER TABLE [dbo].[ItemRating]  WITH CHECK ADD  CONSTRAINT [FKItemRating770149] FOREIGN KEY([RatingId])
REFERENCES [dbo].[Rating] ([RatingId])
GO
ALTER TABLE [dbo].[ItemRating] CHECK CONSTRAINT [FKItemRating770149]
GO
ALTER TABLE [dbo].[ItemReview]  WITH CHECK ADD  CONSTRAINT [FKItemReview812852] FOREIGN KEY([ReviewId])
REFERENCES [dbo].[Review] ([ReviewId])
GO
ALTER TABLE [dbo].[ItemReview] CHECK CONSTRAINT [FKItemReview812852]
GO
ALTER TABLE [dbo].[ItemReview]  WITH CHECK ADD  CONSTRAINT [FKItemReview913713] FOREIGN KEY([ItemId])
REFERENCES [dbo].[Item] ([ItemId])
GO
ALTER TABLE [dbo].[ItemReview] CHECK CONSTRAINT [FKItemReview913713]
GO
ALTER TABLE [dbo].[LinkedUser(FriendsFamily)]  WITH CHECK ADD  CONSTRAINT [FKLinkedUser171796] FOREIGN KEY([RequesterUserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[LinkedUser(FriendsFamily)] CHECK CONSTRAINT [FKLinkedUser171796]
GO
ALTER TABLE [dbo].[LinkedUser(FriendsFamily)]  WITH CHECK ADD  CONSTRAINT [FKLinkedUser183586] FOREIGN KEY([RequesteeUserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[LinkedUser(FriendsFamily)] CHECK CONSTRAINT [FKLinkedUser183586]
GO
ALTER TABLE [dbo].[Merchandise]  WITH CHECK ADD  CONSTRAINT [FKMerchandis500584] FOREIGN KEY([ColourId])
REFERENCES [dbo].[Colour] ([ColourId])
GO
ALTER TABLE [dbo].[Merchandise] CHECK CONSTRAINT [FKMerchandis500584]
GO
ALTER TABLE [dbo].[Merchandise]  WITH CHECK ADD  CONSTRAINT [FKMerchandis677159] FOREIGN KEY([ItemId])
REFERENCES [dbo].[Item] ([ItemId])
GO
ALTER TABLE [dbo].[Merchandise] CHECK CONSTRAINT [FKMerchandis677159]
GO
ALTER TABLE [dbo].[Rating]  WITH CHECK ADD  CONSTRAINT [FKRating950448] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Rating] CHECK CONSTRAINT [FKRating950448]
GO
ALTER TABLE [dbo].[Review]  WITH CHECK ADD  CONSTRAINT [FKReview197042] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Review] CHECK CONSTRAINT [FKReview197042]
GO
ALTER TABLE [dbo].[Status]  WITH CHECK ADD  CONSTRAINT [FKStatus548338] FOREIGN KEY([IconPictureId])
REFERENCES [dbo].[Picture] ([PictureId])
GO
ALTER TABLE [dbo].[Status] CHECK CONSTRAINT [FKStatus548338]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FKUser442548] FOREIGN KEY([GenderId])
REFERENCES [dbo].[Gender] ([GenderId])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FKUser442548]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FKUser559903] FOREIGN KEY([UserTypeId])
REFERENCES [dbo].[UserType] ([UserTypeId])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FKUser559903]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FKUser647226] FOREIGN KEY([MailingAddressId])
REFERENCES [dbo].[Address] ([AddressId])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FKUser647226]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FKUser701072] FOREIGN KEY([ShippingAddresId])
REFERENCES [dbo].[Address] ([AddressId])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FKUser701072]
GO
ALTER TABLE [dbo].[UserCreditCard]  WITH CHECK ADD  CONSTRAINT [FKUserCredit694399] FOREIGN KEY([CreditCardId])
REFERENCES [dbo].[CreditCard] ([CreditCardId])
GO
ALTER TABLE [dbo].[UserCreditCard] CHECK CONSTRAINT [FKUserCredit694399]
GO
ALTER TABLE [dbo].[UserGenre]  WITH CHECK ADD  CONSTRAINT [FKUserGenre369255] FOREIGN KEY([GenreId])
REFERENCES [dbo].[Genre] ([GenreId])
GO
ALTER TABLE [dbo].[UserGenre] CHECK CONSTRAINT [FKUserGenre369255]
GO
ALTER TABLE [dbo].[UserGenre]  WITH CHECK ADD  CONSTRAINT [FKUserGenre793741] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[UserGenre] CHECK CONSTRAINT [FKUserGenre793741]
GO
ALTER TABLE [dbo].[UserItem]  WITH CHECK ADD  CONSTRAINT [FKUserItem132836] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[UserItem] CHECK CONSTRAINT [FKUserItem132836]
GO
ALTER TABLE [dbo].[UserItem]  WITH CHECK ADD  CONSTRAINT [FKUserItem486577] FOREIGN KEY([ItemId])
REFERENCES [dbo].[Item] ([ItemId])
GO
ALTER TABLE [dbo].[UserItem] CHECK CONSTRAINT [FKUserItem486577]
GO
ALTER TABLE [dbo].[UserPlatform]  WITH CHECK ADD  CONSTRAINT [FKUserPlatfo363077] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[UserPlatform] CHECK CONSTRAINT [FKUserPlatfo363077]
GO
ALTER TABLE [dbo].[UserPlatform]  WITH CHECK ADD  CONSTRAINT [FKUserPlatfo475346] FOREIGN KEY([PlatformId])
REFERENCES [dbo].[Platform] ([PlatformId])
GO
ALTER TABLE [dbo].[UserPlatform] CHECK CONSTRAINT [FKUserPlatfo475346]
GO
ALTER TABLE [dbo].[UserType]  WITH CHECK ADD  CONSTRAINT [FKUserType246919] FOREIGN KEY([IconPictureId])
REFERENCES [dbo].[Picture] ([PictureId])
GO
ALTER TABLE [dbo].[UserType] CHECK CONSTRAINT [FKUserType246919]
GO
/****** Object:  StoredProcedure [dbo].[Insert_Genre]    Script Date: 2022-12-08 4:41:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      ILshin Ji
-- Create Date: 2022-10-14
-- Description: Inserting Enum tables data
-- =============================================
CREATE    PROCEDURE [dbo].[Insert_Genre]

    -- Add the parameters for the stored procedure here
    @name VARCHAR(50),
	@type INT
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

    -- Insert statements for procedure here
    INSERT INTO dbo.Genre
	(
		Name,
		Type
	)
	VALUES
	(
		@name ,
		@type
	)
END
GO

ALTER DATABASE [CVGS] SET  READ_WRITE 
GO