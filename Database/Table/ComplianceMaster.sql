USE [LitigationApp]
GO

/****** Object:  Table [dbo].[ComplianceMaster]    Script Date: 7/3/2018 11:23:36 AM ******/
DROP TABLE [dbo].[ComplianceMaster]
GO

/****** Object:  Table [dbo].[ComplianceMaster]    Script Date: 7/3/2018 11:23:36 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ComplianceMaster](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[SrNo] [int] NOT NULL,
	[Description] [nvarchar](1000) NOT NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_ComplianceMaster] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


