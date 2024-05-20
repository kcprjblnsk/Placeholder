CREATE TABLE [dbo].[MonitoredUrls]
(
	[Id] INT IDENTITY (1, 1) NOT NULL,
    [CreateDate] DATETIMEOFFSET NOT NULL,
    [AccountId] INT NOT NULL,
    [Url] NVARCHAR(400) NOT NULL, 
    [RuleSetJson] NVARCHAR(MAX) NOT NULL, 
    CONSTRAINT [PK_MonitoredUrls] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MonitoredUrls_Accounts] FOREIGN KEY ([AccountId]) REFERENCES [dbo].[Accounts] ([Id]) ON DELETE CASCADE
)

GO
CREATE INDEX [IX_MonitoredUrls_AccountId] ON [dbo].[MonitoredUrls] ([AccountId])