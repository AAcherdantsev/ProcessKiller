using ProcessKiller.Logger;

namespace ProcessKiller;

public enum ValidationStatus : byte
{
    Success,
    InvalidNumberOfArguments,
    IncorrectСheckFrequency,
    IncorrectLifetime,
}

public class Validator
{
    private readonly ILogger logger;

    public Validator(ILogger logger)
    {
        this.logger = logger;
    }

    public ValidationStatus TryParseParameters(string[] args, out ProcessKillerParameters? parameters)
    {
        ValidationStatus status = this.Validate(args);

        if (status == ValidationStatus.Success)
        {
            parameters = new(args[Constants.ProccesNameIndexInAgr],
                float.Parse(args[Constants.LifeTimeIndexInAgr]),
                float.Parse(args[Constants.CheckFrequencyIndexInAgr]));
        }
        else
        {
            parameters = null;
        }

        return status;
    }

    public ValidationStatus Validate(string[] agrs)
    {
        if (agrs.Length != Constants.NumberOfAgruments)
        {
            return ValidationStatus.InvalidNumberOfArguments;
        }

        if (float.TryParse(agrs[Constants.LifeTimeIndexInAgr], out float lifeTime))
        {
            if (lifeTime < 0)
            {
                return ValidationStatus.IncorrectLifetime;
            }
        }
        else
        {
            return ValidationStatus.IncorrectLifetime;
        }

        if (float.TryParse(agrs[Constants.CheckFrequencyIndexInAgr], out float checkFrequency))
        {
            if (checkFrequency <= 0)
            {
                return ValidationStatus.IncorrectСheckFrequency;
            }
        }
        else
        {
            return ValidationStatus.IncorrectСheckFrequency;
        }

        return ValidationStatus.Success;
    }

    public void ShowErrorMessage(ValidationStatus status)
    {
        if (status != ValidationStatus.Success)
        {
            switch (status)
            {
                case ValidationStatus.IncorrectLifetime:
                    this.logger.WriteError($"{Constants.MessageIncorrectLifetime}");
                    break;

                case ValidationStatus.IncorrectСheckFrequency:
                    this.logger.WriteError($"{Constants.MessageIncorrectСheckFrequency}");
                    break;

                case ValidationStatus.InvalidNumberOfArguments:
                    this.logger.WriteError($"{Constants.MessageInvalidNumberOfArguments}");
                    break;
            }

            this.logger.WriteMessage(
                Constants.MessageWithSyntaxHint.Replace(Constants.ProcessName, Path.GetFileName(Environment.ProcessPath)));
        }
    }
}