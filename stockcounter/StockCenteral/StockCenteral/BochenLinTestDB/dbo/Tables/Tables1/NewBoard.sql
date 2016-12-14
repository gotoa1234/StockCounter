CREATE TABLE [dbo].[NewBoard] (
    [Guid]      UNIQUEIDENTIFIER CONSTRAINT [DF_NewBoard_Guid] DEFAULT (newid()) NOT NULL,
    [Kind]      NVARCHAR (50)    NOT NULL,
    [Title]     NVARCHAR (100)   NOT NULL,
    [Message]   NVARCHAR (800)   NOT NULL,
    [Datetime]  DATETIME         NOT NULL,
    [ShowInfom] BIT              NOT NULL,
    [Note]      NVARCHAR (500)   NOT NULL,
    CONSTRAINT [PK_NewBoard] PRIMARY KEY CLUSTERED ([Guid] ASC)
);

