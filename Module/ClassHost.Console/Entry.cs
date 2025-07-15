namespace ClassHost.Console;

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
        hostName = arg.GetAt(0) as String;

        IntParse parse;
        parse = new IntParse();
        parse.Init();

        String ka;
        ka = arg.GetAt(1) as String;

        Text k;
        k = textInfra.TextCreateStringData(ka, null);

        long nn;
        nn = parse.Execute(k, 10, null);

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