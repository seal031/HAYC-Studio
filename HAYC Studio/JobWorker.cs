using Quartz;

[DisallowConcurrentExecution]
public class JobWorker : IInterruptableJob
{
    private bool _stop;
    public static IQuartz form;

    public void Interrupt()
    {
        _stop = true;
    }

    void IJob.Execute(IJobExecutionContext context)
    {
        if (!_stop)
        {
            form.cyclicWork();
        }
    }
}

public interface IQuartz
{
    void cyclicWork();
}
