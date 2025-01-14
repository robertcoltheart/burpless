namespace Burpless;

/// <summary>
/// The behavioral step to execute
/// </summary>
public enum StepType
{
    /// <summary>
    /// Describes the state of the system before the behavior begins.
    /// </summary>
    Given,

    /// <summary>
    /// Describes the behavior that is under test.
    /// </summary>
    When,

    /// <summary>
    /// Describes the changes that are expected due to the behavior.
    /// </summary>
    Then
}
