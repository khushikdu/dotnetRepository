using System;

namespace Homework_6.Model
{
    /// <summary>
    /// Represents a student in the system.
    /// </summary>
    public class Student
    {
        /// <summary>
        /// Gets or sets the unique identifier of the student.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the student.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the age of the student.
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Gets or sets the grade of the student.
        /// </summary>
        public string Grade { get; set; }
    }
}
