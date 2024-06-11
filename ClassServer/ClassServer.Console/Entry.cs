namespace ClassServer.Console;

public class Entry : EntryEntry
{
    protected override int ExecuteMain()
    {
        string hostName;
        hostName = (string)this.Arg.Get(0);

        Console a;
        a = new Console();
        a.Init();

        a.HostName = hostName;
        a.Execute();

        int o;
        o = a.Status;
        return o;
    }
}