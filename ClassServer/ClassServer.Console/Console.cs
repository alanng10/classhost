namespace ClassServer.Console;

class Console : Any
{
    public override bool Init()
    {
        base.Init();
        this.ListInfra = ListInfra.This;
        this.ClassInfra = ClassInfra.This;
        this.ConsoleConsole = ConsoleConsole.This;
        this.ClassTaskKindList = ClassTaskKindList.This;

        this.ClassConsole = new ClassConsole();
        this.ClassConsole.Init();

        this.ClassWrite = new ClassWrite();
        this.ClassWrite.Init();
        this.ClassWrite.Start = sizeof(int);
        return true;
    }

    public virtual int Status { get; set; }

    public virtual Network Network { get; set; }
    public virtual TimeInterval Interval { get; set; }
    public virtual int Stage { get; set; }
    protected virtual ListInfra ListInfra { get; set; }
    protected virtual ClassInfra ClassInfra { get; set; }
    protected virtual ConsoleConsole ConsoleConsole { get; set; }
    protected virtual ClassTaskKindList ClassTaskKindList { get; set; }
    protected virtual ClassConsole ClassConsole { get; set; }
    protected virtual ClassSource ClassSource { get; set; }
    protected virtual ClassWrite ClassWrite { get; set; }

    public virtual bool Execute()
    {
        this.ClassConsole.Load();

        ClassTask task;
        task = new ClassTask();
        task.Init();
        task.Kind = this.ClassTaskKindList.Node;
        task.Node = "Class";

        this.ClassConsole.Task = task;

        ClassSource source;
        source = new ClassSource();
        source.Init();
        source.Index = 0;
        source.Name = "Code";

        this.ClassSource = source;

        Array array;
        array = this.ListInfra.ArrayCreate(1);
        array.Set(0, source);
        
        this.ClassConsole.Source = array;

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

    public virtual Data ExecuteClass(string sourceString)
    {
        Array text;
        text = this.ClassInfra.TextCreate(sourceString);

        this.ClassSource.Text = text;

        this.ClassConsole.ExecuteCreate();

        ClassNodeResult result;
        result = this.ClassConsole.Result.Node;

        this.ClassConsole.Result = null;

        this.ClassSource.Text = null;

        Array aa;
        aa = result.Root;

        ClassNodeClass varClass;
        varClass = (ClassNodeClass)aa.Get(0);

        ClassWrite write;
        write = this.ClassWrite;

        write.NodeClass = varClass;

        write.Execute();

        Data data;
        data = write.Data;

        write.Data = null;
        write.NodeClass = null;
        return data;
    }
}