namespace ClassServer.Console;

public class Entry : EntryEntry
{
    protected override int ExecuteMain()
    {
        Console a;
        a = new Console();
        a.Init();

        a.Execute();

        int o;
        o = a.Status;
        return o;
    }
}