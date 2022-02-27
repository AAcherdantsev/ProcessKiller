namespace ProcessKiller.TaskInterrupter;

public class ConsoleTaskInterrupter : AbstractTaskInterrupter
{
    public override void Observe()
    {
        Task taskForCancelling = Task.Factory.StartNew(() =>
        {
            while (true)
            {
                if (Console.ReadKey(true).Key == Constants.ButtonForInterruption)
                {
                    this.CallEvent();
                    break;
                }
            }
        }, TaskCreationOptions.AttachedToParent);
    }
}