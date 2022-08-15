using System.Diagnostics;

using ProcessKiller.Logger;
using ProcessKiller.TaskInterrupter;

namespace ProcessKiller;

public class ProcessKiller
{
    private readonly ILogger logger;
    private readonly CancellationTokenSource ctokenSource;
    private readonly AbstractTaskInterrupter taskInterrupter;
    private readonly ProcessKillerParameters parameters;

    public ProcessKiller(ProcessKillerParameters parameters, ILogger logger, AbstractTaskInterrupter taskInterrupter)
    {
        this.logger = logger;
        this.parameters = parameters;
        this.taskInterrupter = taskInterrupter;

        this.ctokenSource = new();
        this.taskInterrupter.TaskWasAborted += () => this.ctokenSource.Cancel();
    }

    public async Task RunAsync()
    {
        try
        {
            this.taskInterrupter.Observe();

            while (!this.ctokenSource.Token.IsCancellationRequested)
            {
                this.Check();

                await Task.Delay(TimeSpan.FromSeconds(this.parameters.CheckFrequency), this.ctokenSource.Token);
            }
        }
        catch (TaskCanceledException) { }
    }

    private void Check()
    {
        Parallel.ForEach<Process>(Process.GetProcesses().AsParallel().Where(x => x.ProcessName == this.parameters.ProcessName), (process) =>
        {
            try
            {
                if ((DateTime.Now - process.StartTime).TotalSeconds >= this.parameters.LifeTime)
                {
                    process.Kill();
                    this.logger.WriteMessage($"[{DateTime.Now}] {Constants.MessageProcessWasKilled} ({process.ProcessName} {process.Id})");
                }
            }
            catch { };
        });
    }
}
