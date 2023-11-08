namespace DO;

/// <summary>
/// Engineer entity represents a engineer with all its props.
/// </summary>
/// <param name="Id">Personal unique ID of engineer (as in national id card)</param>
/// <param name="Name">Private name of the engineer</param>
/// <param name="Email">The personal email address of the engineer</param>
/// <param name="Level">Engineer level</param>
/// <param name="Cost">Hourly cost of the engineer</param>
public record Engineer
(
    int Id, 
    string Name, 
    string Email,
    EngineerExperience? Level,
    double? Cost
    );

