CREATE TABLE [dbo].[Results]
(
	[Id] INT IDENTITY (1, 1) NOT NULL,
    [Date] DATETIMEOFFSET NOT NULL,
    [MonitoredUrlId] INT NOT NULL,
    [Success]   BIT NOT NULL,
    CONSTRAINT [PK_Results] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Results_MonitoredUrls] FOREIGN KEY ([MonitoredUrlId]) REFERENCES [dbo].[MonitoredUrls] ([Id]) ON DELETE CASCADE
)

GO
CREATE INDEX [IX_Results_MonitoredUrlId] ON [dbo].[Results] ([MonitoredUrlId])