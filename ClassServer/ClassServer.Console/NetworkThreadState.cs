namespace ClassServer.Console;

class NetworkThreadState : ThreadExecuteState
{

    public virtual ClassConsole ClassConsole { get; set; }

    public override bool Execute()
    {

        return true;
    }
}