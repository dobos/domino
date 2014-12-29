USE [Domino]

GO


IF OBJECT_ID (N'UserRoleType', N'U') IS NOT NULL
DROP TABLE [dbo].[UserRoleType]

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


IF OBJECT_ID (N'GradeType', N'U') IS NOT NULL
DROP TABLE [dbo].[GradeType]

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


IF OBJECT_ID (N'User', N'U') IS NOT NULL
DROP TABLE [dbo].[User]

GO

CREATE TABLE [dbo].[User]
(
	[ID] int IDENTITY(1,1) NOT NULL,
	[Name] nvarchar(250) NOT NULL,
	[Visible] bit NOT NULL,
	[Enabled] bit NOT NULL,
	[Email] nvarchar(150) NOT NULL,
	[Username] nvarchar(50) NOT NULL,
	[ActivationCode] nvarchar(50),
	[PasswordHash] varchar(1024),
	
	CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)
)

CREATE TABLE [dbo].[UserRole]
(
	[UserID] int NOT NULL,
	[CourseID] int NOT NULL,
	[RoleID] int NOT NULL,
	
	CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED 
	(
		[UserID] ASC,
		[CourseID] ASC
	)
)

CREATE TABLE [dbo].[Semester]
(
	[ID] int NOT NULL,
	[Name] nvarchar(250) NOT NULL,
	[Visible] bit NOT NULL,
	[Enabled] bit NOT NULL,
	[StartDate] datetime,
	[EndDate] datetime,
	
	CONSTRAINT [PK_Semester] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)
)

CREATE TABLE [dbo].[Course]
(
	[ID] int NOT NULL,
	[SemesterID] int NOT NULL,
	[Name] nvarchar(250) NOT NULL,
	[Visible] bit NOT NULL,
	[Enabled] bit NOT NULL,
	[StartDate] datetime,
	[EndDate] datetime,
	[Url] nvarchar(250),
	[HtmlPage] nvarchar(max),
	[GradeType] int NOT NULL,
	
	CONSTRAINT [PK_Course] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)
)

CREATE TABLE [dbo].[CourseGrade]
(
	[CourseID] int NOT NULL,
	[StudentID] int NOT NULL,
	[Grade] int,
	
	CONSTRAINT [PK_CourseGrade] PRIMARY KEY CLUSTERED 
	(
		[CourseID] ASC,
		[StudentID] ASC
	)
)

CREATE TABLE [dbo].[Assignment]
(
	[ID] int NOT NULL,
	[SemesterID] int NOT NULL,
	[CourseID] int NOT NULL,
	[Ordinal] int NOT NULL,
	[Name] nvarchar(250) NOT NULL,
	[Visible] bit NOT NULL,
	[Enabled] bit NOT NULL,	
	[StartDate] datetime,
	[DueDateSoft] datetime,
	[DueDateHard] datetime,
	[Url] nvarchar(250),
	[HtmlPage] nvarchar(max),
	[GradeType] int NOT NULL,
	[GradeWeight] int NOT NULL,
	
	CONSTRAINT [PK_Assignment] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)
)

CREATE TABLE [dbo].[AssignmentGrade]
(
	[CourseID] int NOT NULL,
	[AssignmentID] int NOT NULL,
	[StudentID] int NOT NULL,
	[Grade] int NOT NULL,
	
	CONSTRAINT [PK_AssignmentGrade] PRIMARY KEY CLUSTERED 
	(
		[CourseID] ASC,
		[AssignmentID] ASC,
		[StudentID] ASC
	)
)

CREATE TABLE [dbo].[Submission]
(
	[ID] int NOT NULL,
	[CourseID] int NOT NULL,
	[AssignmentID] int NOT NULL,
	[StudentID] int NOT NULL,
	[UserID] int NOT NULL,
	[Date] datetime NOT NULL,
	[GitCommitHash] varchar(50) NOT NULL,
	[Comments] nvarchar(max) NOT NULL,
	
	CONSTRAINT [PK_Submission] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)
)