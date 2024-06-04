﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Assignment_3.Model
{
    /// <summary>
    /// Represents a movie entity.
    /// </summary>
    public class Movie
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Title { get; set; }

        public int Price { get; set; }

        public ICollection<Rental> Rentals { get; set; } = new List<Rental>();
    }
}
