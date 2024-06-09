namespace Assignment_5.Model
{
    public class SecretDto
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class DeleteSecretDto
    {
        public string Name { get; set; }
        public int? WaitDurationSeconds { get; set; }
    }
}
