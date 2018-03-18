USE [LitigationApp]
GO

/****** Object:  Table [dbo].[UserDetail]    Script Date: 3/18/2018 7:50:45 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserDetail](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](255) NOT NULL,
	[LastName] [nvarchar](255) NOT NULL,
	[EmailAddress] [nvarchar](255) NULL,
	[PhoneNumber] [nvarchar](50) NULL,
	[Gender] [int] NULL,
	[DOB] [nvarchar](100) NULL,
	[Password] [nvarchar](500) NULL,
	[AlternateNo] [nvarchar](50) NULL,
	[EmergencyContactNo] [nvarchar](50) NULL,
	[EmergencyContactPerson] [nvarchar](255) NULL,
	[DLNumber] [nvarchar](500) NULL,
	[DLCopy] [nvarchar](500) NULL,
	[SSN] [nvarchar](255) NULL,
	[UnsuccessfulAttemptCount] [int] NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetime] NULL,
	[IsEmailVerified] [bit] NULL,
	[IsPhoneVerified] [bit] NULL,
 CONSTRAINT [PK_UserDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UC_UserDetail_EmailAddress] UNIQUE NONCLUSTERED 
(
	[EmailAddress] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


