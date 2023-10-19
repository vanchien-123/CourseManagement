using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Permission
{
    public static class Permissions
    {
        public static List<string> GeneratePermissionsForModule(string module)
        {
            return new List<string>()
        {
            $"Permissions.{module}.Create",
            $"Permissions.{module}.GetAll",
            $"Permissions.{module}.Edit",
            $"Permissions.{module}.Delete",
        };
        }
        public static class Holiday
        {
            public const string List = "Permissions.Holiday.GetAll";
            public const string Create = "Permissions.Holiday.Create";
            public const string Edit = "Permissions.Holiday.UpdateAsync";
            public const string Delete = "Permissions.Holiday.DeleteAsync";
        }

        public static class Classroom
        {
            public const string List = "Permissions.Classroom.GetAll";
            public const string Create = "Permissions.Classroom.Create";
            public const string Edit = "Permissions.Classroom.UpdateAsync";
            public const string Delete = "Permissions.Classroom.DeleteAsync";
        }

        public static class ApplicationUser
        {
            public const string List = "Permissions.ApplicationUser.GetAll";
            public const string Create = "Permissions.ApplicationUser.Create";
            public const string Edit = "Permissions.ApplicationUser.UpdateAsync";
            public const string Delete = "Permissions.ApplicationUser.DeleteAsync";
        }

        public static class Combination
        {
            public const string List = "Permissions.Combination.GetAll";
            public const string Create = "Permissions.Combination.Create";
            public const string Edit = "Permissions.Combination.UpdateAsync";
            public const string Delete = "Permissions.Combination.DeleteAsync";
        }

        public static class Course
        {
            public const string List = "Permissions.Course.GetAll";
            public const string Create = "Permissions.Course.Create";
            public const string Edit = "Permissions.Course.UpdateAsync";
            public const string Delete = "Permissions.Course.DeleteAsync";
        }

        public static class Instructor
        {
            public const string List = "Permissions.Instructor.GetAll";
            public const string Create = "Permissions.Instructor.Create";
            public const string Edit = "Permissions.Instructor.UpdateAsync";
            public const string Delete = "Permissions.Instructor.DeleteAsync";
        }

        public static class Point
        {
            public const string List = "Permissions.Point.GetAll";
            public const string Create = "Permissions.Point.Create";
            public const string Edit = "Permissions.Point.UpdateAsync";
            public const string Delete = "Permissions.Point.DeleteAsync";
        }

        public static class Schedule
        {
            public const string List = "Permissions.Schedule.GetAll";
            public const string Create = "Permissions.Schedule.Create";
            public const string Edit = "Permissions.Schedule.UpdateAsync";
            public const string Delete = "Permissions.Schedule.DeleteAsync";
        }

        public static class Student
        {
            public const string List = "Permissions.Student.GetAll";
            public const string Create = "Permissions.Student.Create";
            public const string Edit = "Permissions.Student.UpdateAsync";
            public const string Delete = "Permissions.Student.DeleteAsync";
        }

        public static class Subject
        {
            public const string List = "Permissions.Subject.GetAll";
            public const string Create = "Permissions.Subject.Create";
            public const string Edit = "Permissions.Subject.UpdateAsync";
            public const string Delete = "Permissions.Subject.DeleteAsync";
        }

        public static class TypePoint
        {
            public const string List = "Permissions.TypePoint.GetAll";
            public const string Create = "Permissions.TypePoint.Create";
            public const string Edit = "Permissions.TypePoint.UpdateAsync";
            public const string Delete = "Permissions.TypePoint.DeleteAsync";
        }

        public static class Tuition
        {
            public const string List = "Permissions.Tuition.GetAll";
            public const string Create = "Permissions.Tuition.Create";
            public const string Edit = "Permissions.Tuition.UpdateAsync";
            public const string Delete = "Permissions.Tuition.DeleteAsync";
        }
    }
}
