using ProcessKiller;

ProcessKiller.Logger.ConsoleLogger logger = new();

ProcessKillerParameters? parameters;
Validator validator = new(logger);

ValidationStatus status = validator.TryParseParameters(args, out parameters);

if (status == ValidationStatus.Success)
{
    ProcessKiller.ProcessKiller processKiller =
        new(parameters, logger, new ProcessKiller.TaskInterrupter.ConsoleTaskInterrupter());

    await processKiller.RunAsync();
}
else
{
    validator.ShowErrorMessage(status);
}