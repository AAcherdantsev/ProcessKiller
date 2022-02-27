namespace ProcessKiller;

public class ProcessKillerParameters
{
    public string ProcessName { get; private set; }

    public float LifeTime { get; private set; }

    public float CheckFrequency { get; private set; }

    public ProcessKillerParameters(string processName, float lifeTime, float checkFrequency)
    {
        this.ProcessName = processName;
        this.LifeTime = lifeTime;
        this.CheckFrequency = checkFrequency;
    }
}