using System.Data;

namespace BO
{
    /// <summary>
    /// Represents a BO engineer entity.
    /// </summary>
    public class Engineer
    {
        public int Id { get; init; } // Unique identifier for the engineer.
        public string? Name { get; set; } // Name of the engineer.
        public bool IsActive { get; set; } // Indicates whether the engineer is active or not.
        public string? Email { get; set; } // Email address of the engineer.
        public EngineerExperience? Level { get; init; } // Experience level of the engineer.
        public double? Cost { get; set; } // Cost associated with the engineer.
        public TaskInEngineer? Task { get; set; } // Task assigned to the engineer.

        /// <summary>
        /// Returns a string representation of the Engineer object.
        /// </summary>
        /// <returns>A string representing the Engineer object.</returns>
        public override string ToString() => this.ToStringProperty();
    }
}