-- Shrink ForumGroups.ImageSource

ALTER TABLE [ForumGroups] ALTER COLUMN [ImageSource] nvarchar(800)

GO

-- Shrink ForumMessages.Text

UPDATE [ForumMessages] SET [Text] = LEFT([Text], 800)

GO

ALTER TABLE [ForumMessages] ALTER COLUMN [Text] nvarchar(800) NOT NULL

GO

-- Shrink Resources.Description

UPDATE [Resources] SET [Description] = LEFT([Description], 800)

GO

ALTER TABLE [Resources] ALTER COLUMN [Description] nvarchar(800) NOT NULL

GO

-- Shrink Resources.Url

ALTER TABLE [Resources] ALTER COLUMN [Url] nvarchar(800)

GO

-- Shrink ResourceTypes.ImageSource

ALTER TABLE [ResourceTypes] ALTER COLUMN [ImageSource] nvarchar(800)

GO

-- Make Users.City not null

UPDATE [Users] SET [City] = '?' WHERE [City] IS NULL

GO

DROP INDEX [IX_Users_City] ON [Users]

GO

ALTER TABLE [Users] ALTER COLUMN [City] nvarchar(100) NOT NULL

GO

CREATE NONCLUSTERED INDEX [IX_Users_City] ON [Users]
(
	[City] ASC
)

GO

-- Add Users.NotifyForum

ALTER TABLE [Users] ADD [NotifyForum] bit

GO

UPDATE [Users] SET [NotifyForum] = 1

GO

ALTER TABLE [Users] ALTER COLUMN [NotifyForum] bit NOT NULL

GO

-- Add Users.NotifyChat

ALTER TABLE [Users] ADD [NotifyChat] bit

GO

UPDATE [Users] SET [NotifyChat] = 1

GO

ALTER TABLE [Users] ALTER COLUMN [NotifyChat] bit NOT NULL

GO

-- Add Users.PasswordHash

ALTER TABLE [Users] ADD [PasswordHash] nvarchar(64)

GO

UPDATE [Users] SET [PasswordHash] = ''

GO

ALTER TABLE [Users] ALTER COLUMN [PasswordHash] nvarchar(64) NOT NULL

GO

-- Make Users.Password null

ALTER TABLE [Users] ALTER COLUMN [Password] nvarchar(100)

GO

-- Add UserGroups.Code

ALTER TABLE [UserGroups] ADD [Code] nvarchar(20)

GO
