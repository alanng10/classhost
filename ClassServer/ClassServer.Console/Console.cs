namespace ClassServer.Console;

public class Console : Any
{
    public override bool Init()
    {
        base.Init();
        this.ClassConsole = new ClassConsole();
        this.ClassConsole.Init();
        return true;
    }

    public virtual int Status { get; set; }

    public virtual ClassConsole ClassConsole { get; set; }
    public virtual Network Network { get; set; }
    public virtual TimeInterval Interval { get; set; }
    public virtual int Stage { get; set; }

    public virtual bool Execute()
    {
        this.ClassConsole.Load();

        Network network;
        network = new Network();
        network.Init();

        this.Network = network;

        network.HostName = "localhost";

        NetworkCaseState caseState;
        caseState = new NetworkCaseState();
        caseState.Console = this;
        caseState.Init();

        network.CaseChangeState = caseState;

        NetworkOpenState openState;
        openState = new NetworkOpenState();
        openState.Console = this;
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