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

    private ClassConsole ClassConsole { get; set; }

    public virtual bool Execute()
    {
        this.ClassConsole.Load();



        return true;
    }

    private bool NetworkStart()
    {
        NetworkThreadState state;
        state = new NetworkThreadState();
        state.Init();

        Thread thread;
        thread = new Thread();
        thread.Init();
        
        thread.ExecuteState = state;

        thread.Execute();

        return true;
    }
}