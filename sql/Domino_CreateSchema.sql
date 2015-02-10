USE [Domino]

GO


CREATE TABLE [dbo].[UserRoleType]
(
	[ID] int NOT NULL,
	[Name] nvarchar(50) NOT NULL,
	
	CONSTRAINT [PK_UserRoleType] PRIMARY KEY CLUSTERED 
	(
		ID ASC
	)
)

GO


CREATE TABLE [dbo].[GradeType]
(
	[ID] int NOT NULL,
	[Name] nvarchar(50) NOT NULL,
	
	CONSTRAINT [PK_GradeType] PRIMARY KEY CLUSTERED 
	(
		ID ASC
	)
)

GO


CREATE TABLE [dbo].[Semester]
(
	[ID] int IDENTITY(1,1) NOT NULL,
	[Name] nvarchar(50) NOT NULL,
	[Description] nvarchar(250) NOT NULL,
	[Hidden] bit NOT NULL,
	[ReadOnly] bit NOT NULL,
	[CreatedDate] datetime NOT NULL,
	[ModifiedDate] datetime NOT NULL,
	[Comments] nvarchar(max) NOT NULL,

	[StartDate] datetime NOT NULL,
	[EndDate] datetime NOT NULL,
	
	CONSTRAINT [PK_Semester] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)
)

GO


CREATE TABLE [dbo].[User]
(
	[ID] int IDENTITY(1,1) NOT NULL,
	[Name] nvarchar(50) NOT NULL,
	[Description] nvarchar(250) NOT NULL,
	[Hidden] bit NOT NULL,
	[ReadOnly] bit NOT NULL,
	[CreatedDate] datetime NOT NULL,
	[ModifiedDate] datetime NOT NULL,
	[Comments] nvarchar(max) NOT NULL,

	[Email] nvarchar(150) NOT NULL,
	[ActivationCode] nvarchar(50),
	[PasswordHash] varchar(1024),
	
	CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)
)

GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_User_Name] ON [dbo].[User]
(
	[Name] ASC
)

GO


CREATE TABLE [dbo].[Course]
(
	[ID] int IDENTITY(1,1) NOT NULL,
	[SemesterID] int NOT NULL,
	[Name] nvarchar(50) NOT NULL,
	[Description] nvarchar(250) NOT NULL,
	[Hidden] bit NOT NULL,
	[ReadOnly] bit NOT NULL,
	[CreatedDate] datetime NOT NULL,
	[ModifiedDate] datetime NOT NULL,
	[Comments] nvarchar(max) NOT NULL,

	[StartDate] datetime,
	[EndDate] datetime,
	[Url] nvarchar(250),
	[GradeType] int NOT NULL,
	
	CONSTRAINT [PK_Course] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	),

	CONSTRAINT [FK_Course_Semester] FOREIGN KEY
	(
		[SemesterID]
	) 
	REFERENCES [dbo].[Semester]
	(
		[ID]
	)
)

GO


CREATE TABLE [dbo].[CourseGrade]
(
	[CourseID] int NOT NULL,
	[StudentID] int NOT NULL,
	[Grade] int,
	[Comments] nvarchar(max),
	
	CONSTRAINT [PK_CourseGrade] PRIMARY KEY CLUSTERED 
	(
		[StudentID] ASC,
		[CourseID] ASC
	),

	CONSTRAINT [FK_CourseGrade_Course] FOREIGN KEY
	(
		[CourseID]
	) 
	REFERENCES [dbo].[Course]
	(
		[ID]
	),

	CONSTRAINT [FK_CourseGrade_User] FOREIGN KEY
	(
		[StudentID]
	) 
	REFERENCES [dbo].[User]
	(
		[ID]
	)
)

GO

CREATE TABLE [dbo].[UserRole]
(
	[UserID] int NOT NULL,
	[CourseID] int NOT NULL,
	[UserRoleType] int NOT NULL,	
	
	CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED 
	(
		[UserID] ASC,
		[CourseID] ASC
	),

	CONSTRAINT [FK_UserRole_User] FOREIGN KEY
	(
		[UserID]
	) 
	REFERENCES [dbo].[User]
	(
		[ID]
	),

	CONSTRAINT [FK_UserRole_Course] FOREIGN KEY
	(
		[CourseID]
	) 
	REFERENCES [dbo].[Course]
	(
		[ID]
	)
)

GO

CREATE TABLE [dbo].[Assignment]
(
	[ID] int IDENTITY(1,1) NOT NULL,
	[CourseID] int NOT NULL,
	[Name] nvarchar(50) NOT NULL,
	[Description] nvarchar(250) NOT NULL,
	[Hidden] bit NOT NULL,
	[ReadOnly] bit NOT NULL,
	[CreatedDate] datetime NOT NULL,
	[ModifiedDate] datetime NOT NULL,
	[Comments] nvarchar(max) NOT NULL,

	[StartDate] datetime,
	[EndDate] datetime,
	[EndDateSoft] datetime,
	[Url] nvarchar(250),
	[GradeType] int NOT NULL,
	[GradeWeight] float NOT NULL,
	
	CONSTRAINT [PK_Assignment] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	),
	
	CONSTRAINT [FK_Assignment_Course] FOREIGN KEY
	(
		[CourseID]
	) 
	REFERENCES [dbo].[Course]
	(
		[ID]
	),
)

GO


CREATE TABLE [dbo].[AssignmentGrade]
(
	[AssignmentID] int NOT NULL,
	[StudentID] int NOT NULL,
	[Grade] int NOT NULL,
	[Comments] nvarchar(max),
	
	CONSTRAINT [PK_AssignmentGrade] PRIMARY KEY CLUSTERED 
	(
		[StudentID] ASC,
		[AssignmentID] ASC
	),

	CONSTRAINT [FK_AssignmentGrade_Assignment] FOREIGN KEY
	(
		[AssignmentID]
	) 
	REFERENCES [dbo].[Assignment]
	(
		[ID]
	),

	CONSTRAINT [FK_AssignmentGrade_User] FOREIGN KEY
	(
		[StudentID]
	) 
	REFERENCES [dbo].[User]
	(
		[ID]
	)
)

GO


CREATE TABLE [dbo].[Submission]
(
	[ID] int IDENTITY(1,1) NOT NULL,
	[AssignmentID] int NOT NULL,
	[StudentID] int NOT NULL,
	[TeacherID] int NULL,
	[Name] nvarchar(50) NOT NULL,
	[Description] nvarchar(250) NOT NULL,
	[Hidden] bit NOT NULL,
	[ReadOnly] bit NOT NULL,
	[CreatedDate] datetime NOT NULL,
	[ModifiedDate] datetime NOT NULL,
	[Comments] nvarchar(max) NOT NULL,

	[ReplyToSubmissionID] int NULL,
	[ReadDate] datetime NULL,
	
	CONSTRAINT [PK_Submission] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	),

	CONSTRAINT [FK_Submission_Assignment] FOREIGN KEY
	(
		AssignmentID
	)
	REFERENCES [dbo].[Assignment]
	(
		ID
	),

	CONSTRAINT [FK_Submission_Student] FOREIGN KEY
	(
		StudentID
	)
	REFERENCES [dbo].[User]
	(
		ID
	),

	CONSTRAINT [FK_Submission_Teacher] FOREIGN KEY
	(
		TeacherID
	)
	REFERENCES [dbo].[User]
	(
		ID
	)
)

GO