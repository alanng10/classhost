namespace ClassServer;

class NetworkThreadState : ThreadExecuteState
{
    public virtual Network Network { get; set; }
    public virtual TimeInterval Interval { get; set; }
    public virtual int Stage { get; set; }

    public override bool Execute()
    {
        Network network;
        network = new Network();
        network.Init();

        this.Network = network;

        network.HostName = "localhost";

        NetworkCaseState caseState;
        caseState = new NetworkCaseState();
        caseState.ThreadState = this;
        caseState.Init();

        network.CaseChangeState = caseState;

        NetworkOpenState openState;
        openState = new NetworkOpenState();
        openState.ThreadState = this;
        openState.Init();

        TimeInterval interval;
        interval = new TimeInterval();
        interval.Init();

        this.Interval = interval;

        interval.SingleShot = true;
        interval.Time = 0;
        interval.Elapse.State.AddState(openState);
        
        interval.Start();

        ThreadCurrent current;
        current = new ThreadCurrent();
        current.Init();

        Thread thread;
        thread = current.Thread;

        thread.ExecuteEventLoop();

        network.Final();
        return true;
    }
}