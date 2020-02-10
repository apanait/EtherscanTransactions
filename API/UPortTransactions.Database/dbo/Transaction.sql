CREATE TABLE [dbo].[Transaction] (
    [TransactionID]    UNIQUEIDENTIFIER CONSTRAINT [TransactionID_default] DEFAULT (newid()) NOT NULL,
    [RequestID]        UNIQUEIDENTIFIER NOT NULL,
    [Address]          VARCHAR (255)    NULL,
    [Topics]           VARCHAR (255)    NULL,
    [Data]             VARCHAR (MAX)    NULL,
    [BlockNumber]      VARCHAR (255)    NULL,
    [TimeStamp]        VARCHAR (255)    NULL,
    [GasPrice]         VARCHAR (255)    NULL,
    [GasUsed]          VARCHAR (255)    NULL,
    [LogIndex]         VARCHAR (255)    NULL,
    [TransactionHash]  VARCHAR (255)    NOT NULL,
    [TransactionIndex] VARCHAR (255)    NULL,
    CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED ([TransactionID] ASC, [RequestID] ASC),
    CONSTRAINT [FK_Transaction_TransactionRequest] FOREIGN KEY ([RequestID]) REFERENCES [dbo].[TransactionRequest] ([RequestID]),
    UNIQUE NONCLUSTERED ([TransactionHash] ASC)
);


GO

GO


GO
