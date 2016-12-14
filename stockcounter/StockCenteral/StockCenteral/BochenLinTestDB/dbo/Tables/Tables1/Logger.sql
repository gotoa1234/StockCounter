CREATE TABLE [dbo].[Logger] (
    [Level]   NVARCHAR (10)  NOT NULL,
    [Date]    DATETIME       NOT NULL,
    [Message] NVARCHAR (500) NOT NULL,
    [Stack]   NVARCHAR (250) NULL
);

