CREATE TABLE [dbo].[AccountTable] (
    [Account]          NVARCHAR (50)    NOT NULL,
    [Password]         NVARCHAR (50)    NOT NULL,
    [UserLevel]        INT              NOT NULL,
    [GUID]             UNIQUEIDENTIFIER CONSTRAINT [DF_AccountTable_GUID] DEFAULT (newid()) NOT NULL,
    [UserName]         NVARCHAR (50)    NOT NULL,
    [UserMail]         NVARCHAR (50)    NULL,
    [UserPhone]        NVARCHAR (50)    NULL,
    [UserCellPhone]    NVARCHAR (50)    NULL,
    [UserAddress]      NVARCHAR (50)    NULL,
    [UserRegisterDate] DATETIME         CONSTRAINT [DF_AccountTable_UserRegisterDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_AccountTable] PRIMARY KEY CLUSTERED ([GUID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'使用者登入帳號', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AccountTable', @level2type = N'COLUMN', @level2name = N'Account';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'使用者登入密碼', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AccountTable', @level2type = N'COLUMN', @level2name = N'Password';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'使用者權限等級', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AccountTable', @level2type = N'COLUMN', @level2name = N'UserLevel';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'GUID唯一碼', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AccountTable', @level2type = N'COLUMN', @level2name = N'GUID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'使用者名字', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AccountTable', @level2type = N'COLUMN', @level2name = N'UserName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'使用者Mail', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AccountTable', @level2type = N'COLUMN', @level2name = N'UserMail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'使用者家裡電話或公司電話', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AccountTable', @level2type = N'COLUMN', @level2name = N'UserPhone';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'使用者手機號碼', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AccountTable', @level2type = N'COLUMN', @level2name = N'UserCellPhone';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'使用者家裡住址', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AccountTable', @level2type = N'COLUMN', @level2name = N'UserAddress';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'使用者第一次註冊時間', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AccountTable', @level2type = N'COLUMN', @level2name = N'UserRegisterDate';

