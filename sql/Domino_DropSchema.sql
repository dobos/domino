USE [Domino]

GO

IF OBJECT_ID (N'UserRoleType', N'U') IS NOT NULL
DROP TABLE [dbo].[UserRoleType]

GO

IF OBJECT_ID (N'GradeType', N'U') IS NOT NULL
DROP TABLE [dbo].[GradeType]

GO

IF OBJECT_ID (N'Submission', N'U') IS NOT NULL
DROP TABLE [dbo].[Submission]

GO

IF OBJECT_ID (N'AssignmentGrade', N'U') IS NOT NULL
DROP TABLE [dbo].[AssignmentGrade]

GO

IF OBJECT_ID (N'Assignment', N'U') IS NOT NULL
DROP TABLE [dbo].[Assignment]

GO

IF OBJECT_ID (N'UserRole', N'U') IS NOT NULL
DROP TABLE [dbo].[UserRole]

GO

IF OBJECT_ID (N'User', N'U') IS NOT NULL
DROP TABLE [dbo].[User]

GO

IF OBJECT_ID (N'CourseGrade', N'U') IS NOT NULL
DROP TABLE [dbo].[CourseGrade]

GO

IF OBJECT_ID (N'Course', N'U') IS NOT NULL
DROP TABLE [dbo].[Course]

GO

IF OBJECT_ID (N'Semester', N'U') IS NOT NULL
DROP TABLE [dbo].[Semester]

GO