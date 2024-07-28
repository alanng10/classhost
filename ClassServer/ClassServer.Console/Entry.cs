namespace ClassServer.Console;

public class Entry : EntryEntry
{
    protected override int ExecuteMain()
    {
        TextInfra textInfra;
        textInfra = TextInfra.This;

        Array arg;
        arg = this.Arg;

        if (arg.Count < 2)
        {
            return 310;
        }

        string hostName;
        hostName = (string)arg.GetAt(0);

        IntParse parse;
        parse = new IntParse();
        parse.Init();

        string ka;
        ka = (string)arg.GetAt(1);

        Text k;
        k = textInfra.TextCreateStringData(ka, null);

        long nn;
        nn = parse.Execute(k, 10, false);

        if (nn == -1)
        {
            return 311;
        }

        int hostPort;
        hostPort = (int)nn;

        Console a;
        a = new Console();
        a.Init();

        a.HostName = hostName;
        a.HostPort = hostPort;
        a.Execute();

        int o;
        o = a.Status + 400;
        return o;
    }
}