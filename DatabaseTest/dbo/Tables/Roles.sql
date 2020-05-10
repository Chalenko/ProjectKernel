CREATE TABLE [dbo].[Roles] (
    [id]          INT            IDENTITY (1, 1) NOT NULL,
    [name]        NVARCHAR (50)  NOT NULL,
    [description] NVARCHAR (100) NULL,
    [creator_id]  INT            NOT NULL,
    [create_date] DATETIME       NOT NULL,
    [modifier_id] INT            NULL,
    [modify_date] DATETIME       NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_Roles_ToUsers_Creator] FOREIGN KEY ([creator_id]) REFERENCES [dbo].[Users] ([id]),
    CONSTRAINT [FK_Roles_ToUsers_Modifier] FOREIGN KEY ([modifier_id]) REFERENCES [dbo].[Users] ([id])
);

