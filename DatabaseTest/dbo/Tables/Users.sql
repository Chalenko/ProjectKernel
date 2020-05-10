CREATE TABLE [dbo].[Users] (
    [id]              INT            IDENTITY (1, 1) NOT NULL,
    [surname]         NVARCHAR (50)  NOT NULL,
    [first_name]      NVARCHAR (50)  NOT NULL,
    [patronymic_name] NVARCHAR (50)  NULL,
    [login]           NVARCHAR (50)  NOT NULL,
    [salt]            NVARCHAR (50)  NOT NULL,
    [password]        NVARCHAR (100) NOT NULL,
    [department]      NVARCHAR (50)  NOT NULL,
    [creator_id]      INT            NOT NULL,
    [create_date]     DATETIME       NOT NULL,
    [modifier_id]     INT            NULL,
    [modify_date]     DATETIME       NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_Users_ToUsers_Creator] FOREIGN KEY ([creator_id]) REFERENCES [dbo].[Users] ([id]),
    CONSTRAINT [FK_Users_ToUsers_Modifier] FOREIGN KEY ([modifier_id]) REFERENCES [dbo].[Users] ([id]),
    CONSTRAINT [CK_Users_Column] UNIQUE NONCLUSTERED ([login] ASC)
);

