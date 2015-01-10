USE [Domino]

-- Create contants

INSERT [dbo].[UserRoleType]
VALUES
	(1, 'admin'),
	(2, 'teacher'),
	(3, 'student')
	
INSERT [dbo].[GradeType]
VALUES
	(1, 'al·Ìr·s'),
	(2, 'oszt·lyzat'),
	(3, 'pont')
	

-- Create dummy semester for admin

SET IDENTITY_INSERT [dbo].[Semester] ON

INSERT [dbo].[Semester]
	(ID, Name, Visible, Enabled, StartDate, EndDate)
VALUES
	(-1, 'admin', 0, 0, '2015-01-01', '2015-01-01')

SET IDENTITY_INSERT [dbo].[Semester] OFF

-- Create dummy course for admin

SET IDENTITY_INSERT [dbo].[Course] ON

INSERT [dbo].[Course]
	(ID, SemesterID, Name, Visible, Enabled, StartDate, EndDate, Url, HtmlPage, GradeType)
VALUES
	(-1, -1, 'admin', 0, 0, '2015-01-01', '2015-01-01', NULL, NULL, -1)

SET IDENTITY_INSERT [dbo].[Course] OFF

GO
	
-- Create admin user
	
SET IDENTITY_INSERT [dbo].[User] ON
	
INSERT [dbo].[User]
	(ID, Name, Visible, Enabled, Email, Username, ActivationCode, PasswordHash)
VALUES
	(1, 'Administrator', 1, 1, 'admin@domino.org', 'admin', NULL, '43phqQejhFkrk7ICvVjZlJFbhsgWcGHDF6M1r7ln5WujqVS3Cyautp1SfhO1glr1KrGIskraIKe9sxuhtHW03A==')

SET IDENTITY_INSERT [dbo].[User] OFF

	
INSERT [dbo].[UserRole]
VALUES
	(1, -1, 1)
	
