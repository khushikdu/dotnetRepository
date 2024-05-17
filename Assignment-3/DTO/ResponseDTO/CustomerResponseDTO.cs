namespace Assignment_3.DTO.ResponseDTO
{
    /// <summary>
    /// Data transfer object for representing customer response data.
    /// </summary>
    public class CustomerResponseDTO
    {
        /// <summary>
        /// Gets or sets the ID of the customer.
        /// </summary>
        /// <value>The ID of the customer.</value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the username of the customer.
        /// </summary>
        /// <value>The username of the customer.</value>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the email address of the customer.
        /// </summary>
        /// <value>The email address of the customer.</value>
        public string Email { get; set; }
    }
}
