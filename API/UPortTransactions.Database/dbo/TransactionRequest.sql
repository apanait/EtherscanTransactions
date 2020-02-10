CREATE TABLE [dbo].[TransactionRequest] (
    [RequestID] UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Status]    NVARCHAR (255)   NOT NULL,
    [Message]   NVARCHAR (255)   NOT NULL,
    [Url]       NVARCHAR (255)   NOT NULL,
    CONSTRAINT [PK_TransactionRequest] PRIMARY KEY CLUSTERED ([RequestID] ASC)
);


GO
