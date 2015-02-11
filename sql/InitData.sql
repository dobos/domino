USE [Domino]

GO

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
	(ID, Name, Description, Hidden, ReadOnly, CreatedDate, ModifiedDate, Comments, StartDate, EndDate)
VALUES
	(-1, 'admin', 'admin', 1, 1, GETDATE(), GETDATE(), '', '2015-01-01', '2015-01-01')

SET IDENTITY_INSERT [dbo].[Semester] OFF

-- Create dummy course for admin

SET IDENTITY_INSERT [dbo].[Course] ON

INSERT [dbo].[Course]
	(ID, SemesterID, Name, Description, Hidden, ReadOnly, CreatedDate, ModifiedDate, Comments, StartDate, EndDate, Url,  GradeType)
VALUES
	(-1, -1, 'admin', 'admin', 1, 1, GETDATE(), GETDATE(), '', '2015-01-01', '2015-01-01', NULL, -1)

SET IDENTITY_INSERT [dbo].[Course] OFF

GO
	
-- Create admin user
	
SET IDENTITY_INSERT [dbo].[User] ON
	
INSERT [dbo].[User]
	(ID, Name, Description, Hidden, ReadOnly, Enabled, CreatedDate, ModifiedDate, Comments, Email, ActivationCode, PasswordHash)
VALUES
	(1, 'admin', 'Administrator', 0, 0, 1, GETDATE(), GETDATE(), '', 'admin@domino.org', NULL, '43phqQejhFkrk7ICvVjZlJFbhsgWcGHDF6M1r7ln5WujqVS3Cyautp1SfhO1glr1KrGIskraIKe9sxuhtHW03A==')

SET IDENTITY_INSERT [dbo].[User] OFF

	
INSERT [dbo].[UserRole]
VALUES
	(1, -1, 1)
	
