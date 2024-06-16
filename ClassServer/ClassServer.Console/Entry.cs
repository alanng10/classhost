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
            return 10;
        }

        string hostName;
        hostName = (string)arg.Get(0);

        IntParse parse;
        parse = new IntParse();
        parse.Init();

        string ka;
        ka = (string)arg.Get(1);

        Text k;
        k = textInfra.TextCreateStringData(ka, null);

        long nn;
        nn = parse.Execute(k, 10, false);

        if (nn == -1)
        {
            return 11;
        }

        int serverPort;
        serverPort = (int)nn;

        Console a;
        a = new Console();
        a.Init();

        a.HostName = hostName;
        a.ServerPort = serverPort;
        a.Execute();

        int o;
        o = a.Status + 100;
        return o;
    }
}