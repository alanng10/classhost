namespace ClassServer.Console;

public class Entry : EntryEntry
{
    protected override long ExecuteMain()
    {
        TextInfra textInfra;
        textInfra = TextInfra.This;

        Array arg;
        arg = this.Arg;

        if (arg.Count < 2)
        {
            return 310;
        }

        String hostName;
        hostName = (String)arg.GetAt(0);

        IntParse parse;
        parse = new IntParse();
        parse.Init();

        String ka;
        ka = (String)arg.GetAt(1);

        Text k;
        k = textInfra.TextCreateStringData(ka, null);

        long nn;
        nn = parse.Execute(k, 10, false, null);

        if (nn == -1)
        {
            return 311;
        }

        long hostPort;
        hostPort = nn;

        Console a;
        a = new Console();
        a.Init();

        a.HostName = hostName;
        a.HostPort = hostPort;
        a.Execute();

        long o;
        o = a.Status + 400;
        return o;
    }
}