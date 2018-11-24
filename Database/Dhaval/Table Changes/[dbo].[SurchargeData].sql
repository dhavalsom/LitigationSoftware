USE LitigationApp
GO

/****** Object:  Table [dbo].[SurchargeData]    Script Date: 3/18/2018 7:52:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SurchargeData](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FYAYID] [bigint] NOT NULL,
	[surchargefromthreshold] [decimal](18, 2) NULL,
	[surchargetothreshold] [decimal](18, 2) NULL,
	[surchargerate] [decimal](18,2) NULL,
	[entitycategorytypeid] [bigint] null,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_SurchargeData] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SurchargeData]  WITH CHECK ADD  CONSTRAINT [FK_SurchargeData_FYAYID] FOREIGN KEY([FYAYID])
REFERENCES [dbo].[FYAYMaster] ([Id])
GO