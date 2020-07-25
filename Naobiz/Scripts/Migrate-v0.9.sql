-- Extend ChatMessages.Text

ALTER TABLE [ChatMessages] ALTER COLUMN [Text] nvarchar(250)

GO

-- Add Users.Company

ALTER TABLE [Users] ADD [Company] nvarchar(100)

GO

CREATE NONCLUSTERED INDEX [IX_Users_Company] ON [Users] 
(
	[Company] ASC
)

GO

-- Add ForumGroups.OrderNumber

ALTER TABLE [ForumGroups] ADD [OrderNumber] [int]

GO

UPDATE [ForumGroups] SET [OrderNumber] = 0

GO

ALTER TABLE [ForumGroups] ALTER COLUMN [OrderNumber] [int] NOT NULL

GO

-- Add ResourceTypes.OrderNumber

ALTER TABLE [ResourceTypes] ADD [OrderNumber] [int]

GO

UPDATE [ResourceTypes] SET [OrderNumber] = 0

GO

ALTER TABLE [ResourceTypes] ALTER COLUMN [OrderNumber] [int] NOT NULL

GO
