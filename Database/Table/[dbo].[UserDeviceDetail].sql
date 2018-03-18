USE LitigationApp
GO

/****** Object:  Table [dbo].[UserDeviceDetail]    Script Date: 3/18/2018 7:51:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserDeviceDetail](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[IpAddress] [nvarchar](50) NOT NULL,
	[DeviceType] [nvarchar](50) NULL,
	[TwoFactorAuthDone] [bit] NOT NULL,
	[TwoFactorAuthTimestamp] [datetime] NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_UserDeviceDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[UserDeviceDetail]  WITH CHECK ADD  CONSTRAINT [FK_UserDeviceDetail_UserDetail_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[UserDetail] ([Id])
GO

ALTER TABLE [dbo].[UserDeviceDetail] CHECK CONSTRAINT [FK_UserDeviceDetail_UserDetail_UserId]
GO


