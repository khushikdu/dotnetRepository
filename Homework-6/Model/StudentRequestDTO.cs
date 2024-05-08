using System;
using System.ComponentModel.DataAnnotations;


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
        [Required(ErrorMessage = "Name is a required field")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 to 50 characters")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the age of the student.
        /// </summary>
        [Required(ErrorMessage = "Age is a required field.")]
        [Range(13, 25, ErrorMessage = "Age must be between 13 and 25.")]
        public int Age { get; set; }

        /// <summary>
        /// Gets or sets the grade of the student.
        /// </summary>
        public string Grade { get; set; }
    }
}
