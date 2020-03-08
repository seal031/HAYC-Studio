using Quartz;

public class JobWorker : IInterruptableJob
{
    private bool _stop;
    public static IdoJobWorker worker;

    public void Interrupt()
    {
        _stop = true;
    }

    void IJob.Execute(IJobExecutionContext context)
    {
        if (!_stop)
        {
            if (worker == null)
            {
                throw new System.Exception("worker不能为null");
            }
            worker.doJobWork();
        }
    }
}


public interface IdoJobWorker
{
    void doJobWork();
}