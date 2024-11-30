namespace ClassHost.Console;

class Console : ClassBase
{
    public override bool Init()
    {
        base.Init();
        this.StorageInfra = StorageInfra.This;
        this.TextCodeKindList = TextCodeKindList.This;
        this.StorageStatusList = StorageStatusList.This;
        this.ConsoleConsole = ConsoleConsole.This;
        this.ClassTaskKindList = ClassTaskKindList.This;

        this.ClassConsole = new ClassConsole();
        this.ClassConsole.Init();

        this.ClassWrite = new ClassWrite();
        this.ClassWrite.Console = this;
        this.ClassWrite.Init();
        this.ClassWrite.Start = sizeof(int);
        return true;
    }

    public virtual String HostName { get; set; }
    public virtual long HostPort { get; set; }
    public virtual long Status { get; set; }

    public virtual Network Network { get; set; }
    public virtual Thread Thread { get; set; }
    protected virtual StorageInfra StorageInfra { get; set; }
    protected virtual TextCodeKindList TextCodeKindList { get; set; }
    protected virtual StorageStatusList StorageStatusList { get; set; }
    protected virtual ConsoleConsole ConsoleConsole { get; set; }
    protected virtual ClassTaskKindList ClassTaskKindList { get; set; }
    protected virtual ClassConsole ClassConsole { get; set; }
    protected virtual ClassSource ClassSource { get; set; }
    protected virtual ClassWrite ClassWrite { get; set; }
    protected virtual Array ClassConsoleList { get; set; }

    public virtual bool Execute()
    {
        this.ClassConsole.Load();

        ClassTask task;
        task = new ClassTask();
        task.Init();
        task.Kind = this.ClassTaskKindList.Node;
        task.Node = this.S("Class");

        this.ClassConsole.Task = task;

        ClassSource source;
        source = new ClassSource();
        source.Init();
        source.Index = 0;
        source.Name = this.S("Code");

        this.ClassSource = source;

        Array array;
        array = this.ListInfra.ArrayCreate(1);
        array.SetAt(0, source);
        
        this.ClassConsole.Source = array;

        Network network;
        network = new Network();
        network.Console = this;
        network.Init();

        this.Network = network;

        network.HostName = this.HostName;
        network.HostPort = this.HostPort;

        ThreadThis current;
        current = new ThreadThis();
        current.Init();

        Thread thread;
        thread = current.Thread;

        this.Thread = thread;

        // this.Log("ClassServer.Console:Console.Execute EventLoop Start");

        network.Open();

        long o;
        o = thread.ExecuteMain();

        // this.Log("ClassServer.Console:Console.Execute EventLoop End");

        network.Final();

        if (!(o == 0))
        {
            String k;
            k = this.StringInt(o);

            String kk;
            kk = this.AddClear().AddS("Console Exit Status: ").Add(k).AddResult();

            this.Log(kk);
        }
        
        this.Status = o;
        return true;
    }

    public virtual bool Log(String text)
    {
        TextCodeKindList codeKindList;
        codeKindList = this.TextCodeKindList;

        StorageStatusList statusList;
        statusList = this.StorageStatusList;

        String aa;
        aa = this.AddClear().Add(text).AddLine().AddResult();

        bool oo;
        oo = false;
        
        StorageMode mode;
        mode = new StorageMode();
        mode.Init();
        mode.Read = true;
        mode.Write = true;
        mode.Exist = true;

        Storage a;
        a = new Storage();
        a.Init();
        a.Path = this.S("ClassServer.Console.data/log.txt");
        a.Mode = mode;
        a.Open();

        if (a.Status == statusList.NoError)
        {
            long kn;
            kn = a.Stream.Count;

            a.Stream.PosSet(kn);

            if (a.Status == statusList.NoError)
            {
                TextCodeKind innKind;
                TextCodeKind outKind;
                innKind = codeKindList.Utf32;
                outKind = codeKindList.Utf8;

                Data data;
                data = this.TextInfra.StringDataCreateString(aa);
                
                Range dataRange;
                dataRange = new Range();
                dataRange.Init();
                dataRange.Count = data.Count;

                Data result;
                result = this.TextInfra.Code(innKind, outKind, data, dataRange);                

                Range range;
                range = new Range();
                range.Init();
                range.Count = result.Count;

                a.Stream.Write(result, range);

                if (a.Status == statusList.NoError)
                {
                    oo = true;
                }
            }
        }

        a.Close();
        a.Final();

        return oo;
    }

    public virtual bool ExecuteClassFold(Array pathArray)
    {
        

        return false;
    }

    public virtual Data ExecuteClass(Text sourceText)
    {
        // this.Log("Console.ExecuteClass Start");

        Array text;
        text = this.TextLimit(sourceText, this.TB(this.TextInfra.NewLine));

        this.ClassSource.Text = text;

        // this.Log("Console.ExecuteClass 1111");

        this.ClassConsole.ExecuteCreate();

        // this.Log("Console.ExecuteClass 2222");

        ClassNodeResult result;
        result = this.ClassConsole.Result.Node;

        ClassTokenResult tokenResult;
        tokenResult = this.ClassConsole.Result.Token;
        
        this.ClassConsole.Result = null;

        this.ClassSource.Text = null;

        Array aa;
        aa = result.Root;

        ClassNodeClass varClass;
        varClass = (ClassNodeClass)aa.GetAt(0);

        Array ab;
        ab = tokenResult.Code;

        ClassTokenCode code;
        code = (ClassTokenCode)ab.GetAt(0);

        ClassWrite write;
        write = this.ClassWrite;

        write.NodeClass = varClass;
        write.Error = result.Error;
        write.TokenCode = code;
        write.SourceText = text;

        // this.Log("Console.ExecuteClass 3333");
        
        write.Execute();

        // this.Log("Console.ExecuteClass 4444");

        Data data;
        data = write.Data;

        write.Data = null;

        write.SourceText = null;
        write.TokenCode = null;
        write.Error = null;
        write.NodeClass = null;

        int k;
        k = (int)data.Count;
        
        k = k - write.Start;

        // this.Log("Out Data count: " + k);

        // this.Log("Out Data Start");

        // int start;
        // start = write.Start;
        // int count;
        // count = k;
        // int i;
        // i = 0;
        // while (i < count)
        // {
        //     int oo;
        //     oo = data.Get(start + i);

        //     byte ob;
        //     ob = (byte)oo;

        //     string kkk;
        //     kkk = ob.ToString("x2");

        //     this.Log(kkk);
            
        //     i = i + 1;
        // }

        // this.Log("Out Data End");

        long n;
        n = k;

        this.InfraInfra.DataMidSet(data, 0, n);

        // this.Log("Console.ExecueClass End");
        return data;
    }
}