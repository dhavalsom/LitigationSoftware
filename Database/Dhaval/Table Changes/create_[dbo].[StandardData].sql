USE LitigationApp
GO

/****** Object:  Table [dbo].[StandardData]    Script Date: 3/18/2018 7:52:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[StandardData](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FYAYID] [bigint] NOT NULL,
	[BasicTaxRate] [decimal](18, 2) NULL,
	[MATRate] [decimal](18, 2) NULL,
	[EducationCess] [decimal](18,2) NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_StandardData] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[StandardData]  WITH CHECK ADD  CONSTRAINT [FK_StandardData_FYAYID] FOREIGN KEY([FYAYID])
REFERENCES [dbo].[FYAYMaster] ([Id])
GO