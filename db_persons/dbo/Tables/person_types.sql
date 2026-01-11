CREATE TABLE [dbo].[person_types]
(
	[id] INT NOT NULL IDENTITY (1, 1),
	[name] NVARCHAR(50) NOT NULL,
	CONSTRAINT [pk_person_types] PRIMARY KEY CLUSTERED ([id])
)
GO