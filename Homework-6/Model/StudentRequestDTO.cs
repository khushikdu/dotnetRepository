using System;

namespace Homework_6.Model
{
    /// <summary>
    /// Represents the data transfer object (DTO) for creating or updating a student.
    /// </summary>
    public class StudentRequestDTO
    {
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
