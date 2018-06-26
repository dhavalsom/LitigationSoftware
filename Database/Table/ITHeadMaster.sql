USE [LitigationApp]
GO

/****** Object:  Table [dbo].[ITHeadMaster]    Script Date: 6/26/2018 9:53:57 AM ******/
DROP TABLE [dbo].[ITHeadMaster]
GO

/****** Object:  Table [dbo].[ITHeadMaster]    Script Date: 6/26/2018 9:53:57 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ITHeadMaster](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ExcelSrNo] [nvarchar](10) NULL,
	[Description] [nvarchar](1000) NOT NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_ITHeadMaster] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


