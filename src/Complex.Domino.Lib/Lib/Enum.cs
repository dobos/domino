﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex.Domino.Lib
{
    public enum UserRoleType : int
    {
        Unknown = -1,
        Admin = 1,
        Teacher = 2,
        Student = 3
    }

    public enum GradeType : int
    {
        Unknown = -1,
        Signature = 1,
        Grade = 2,
        Points = 3
    }

    public enum SubmissionDirection
    {
        Unknown = -1,
        StudentToTeacher = 1,
        TeacherToStudent = 2,
    }
}
