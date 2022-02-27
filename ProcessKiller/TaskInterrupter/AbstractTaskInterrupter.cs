namespace ProcessKiller.TaskInterrupter;

public abstract class AbstractTaskInterrupter
{
    public delegate void TaskInterrupterHandler();

    public event TaskInterrupterHandler? TaskWasAborted;

    protected void CallEvent()
    {
        this.TaskWasAborted?.Invoke();
    }

    public abstract void Observe();
}