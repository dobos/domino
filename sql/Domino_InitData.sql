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
	
