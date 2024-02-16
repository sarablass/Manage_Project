namespace BO
{
    /// <summary>
    /// Represents an engineer associated with a task.
    /// </summary>
    public class EngineerInTask
    {
        public int Id { get; init; } // Unique identifier for the engineer.
        public string? Name { get; set; } // Name of the engineer.

        /// <summary>
        /// Returns a string representation of the EngineerInTask object.
        /// </summary>
        /// <returns>A string representing the EngineerInTask object.</returns>
        public override string ToString() => this.ToStringProperty();
    }
}