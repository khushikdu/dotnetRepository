namespace Assignment_3.DTO.ResponseDTO
{
    /// <summary>
    /// Data transfer object for representing movie response data.
    /// </summary>
    public class MovieResponseDTO
    {
        /// <summary>
        /// Gets or sets the ID of the movie.
        /// </summary>
        /// <value>The ID of the movie.</value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the title of the movie.
        /// </summary>
        /// <value>The title of the movie.</value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the price of the movie.
        /// </summary>
        /// <value>The price of the movie.</value>
        public int Price { get; set; }
    }
}
