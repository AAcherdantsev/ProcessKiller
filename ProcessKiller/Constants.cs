namespace ProcessKiller;

public static class Constants
{
    public const int NumberOfAgruments = 3;
    public const int ProccesNameIndexInAgr = 0;
    public const int LifeTimeIndexInAgr = 1;
    public const int CheckFrequencyIndexInAgr = 2;

    public const ConsoleKey ButtonForInterruption = ConsoleKey.Q;

    public const string ProcessName = "ProcessKiller.exe";

    #region Messages

    public const string MessageProcessWasKilled = "The Process was Killed";
    public const string MessageInvalidNumberOfArguments = "ERROR: Invalid Number of Arguments";
    public const string MessageIncorrectСheckFrequency = "ERROR: Invalid Check Frequency";
    public const string MessageIncorrectLifetime = "ERROR: Incorrect Lifetime";
    public const string MessageWithSyntaxHint = $"Please use the syntax: {ProcessName} [process name] [lifetime] [check frequency]. For example: {ProcessName} notepad 5 1 ";

    #endregion
}