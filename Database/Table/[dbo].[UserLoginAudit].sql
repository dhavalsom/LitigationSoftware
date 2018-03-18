USE LitigationApp
GO

/****** Object:  Table [dbo].[UserLoginAudit]    Script Date: 3/18/2018 7:52:15 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserLoginAudit](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[UserDeviceId] [bigint] NOT NULL,
	[IsTwoWayAuthNeeded] [bit] NOT NULL,
	[AccessCode] [nvarchar](10) NULL,
	[IsTwoWayAuthPassed] [bit] NOT NULL,
	[TwoFactorAuthTimestamp] [datetime] NULL,
	[SessionId] [nvarchar](255) NOT NULL,
	[Active] [bit] NOT NULL,
	[AddedBy] [bigint] NULL,
	[AddedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_UserLoginAudit] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[UserLoginAudit]  WITH CHECK ADD  CONSTRAINT [FK_UserLoginAudit_UserDetail_UserDeviceId] FOREIGN KEY([UserDeviceId])
REFERENCES [dbo].[UserDeviceDetail] ([Id])
GO

ALTER TABLE [dbo].[UserLoginAudit] CHECK CONSTRAINT [FK_UserLoginAudit_UserDetail_UserDeviceId]
GO

ALTER TABLE [dbo].[UserLoginAudit]  WITH CHECK ADD  CONSTRAINT [FK_UserLoginAudit_UserDetail_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[UserDetail] ([Id])
GO

ALTER TABLE [dbo].[UserLoginAudit] CHECK CONSTRAINT [FK_UserLoginAudit_UserDetail_UserId]
GO


