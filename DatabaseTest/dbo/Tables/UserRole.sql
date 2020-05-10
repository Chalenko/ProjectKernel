CREATE TABLE [dbo].[UserRole] (
    [id]          INT      IDENTITY (1, 1) NOT NULL,
    [user_id]     INT      NOT NULL,
    [role_id]     INT      NOT NULL,
    [creator_id]  INT      NOT NULL,
    [create_date] DATETIME NOT NULL,
    CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED ([user_id] ASC, [role_id] ASC),
    CONSTRAINT [FK_UserRole_Role] FOREIGN KEY ([role_id]) REFERENCES [dbo].[Roles] ([id]),
    CONSTRAINT [FK_UserRole_User] FOREIGN KEY ([user_id]) REFERENCES [dbo].[Users] ([id]),
    CONSTRAINT [FK_UserRole_User_Creator] FOREIGN KEY ([creator_id]) REFERENCES [dbo].[Users] ([id])
);

