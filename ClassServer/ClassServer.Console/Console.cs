namespace ClassServer.Console;

class Console : Any
{
    public override bool Init()
    {
        base.Init();
        this.InfraInfra = InfraInfra.This;
        this.ListInfra = ListInfra.This;
        this.TextInfra = TextInfra.This;
        this.StorageInfra = StorageInfra.This;
        this.TextEncodeKindList = TextEncodeKindList.This;
        this.StorageStatusList = StorageStatusList.This;
        this.ClassInfra = ClassInfra.This;
        this.ConsoleConsole = ConsoleConsole.This;
        this.ClassTaskKindList = ClassTaskKindList.This;

        this.ClassConsole = new ClassConsole();
        this.ClassConsole.Init();

        this.ClassWrite = new ClassWrite();
        this.ClassWrite.Console = this;
        this.ClassWrite.Init();
        this.ClassWrite.Start = sizeof(int);

        this.TextNewLine = this.TextInfra.TextCreateStringData(this.ClassInfra.NewLine, null);

        IntCompare charCompare;
        charCompare = new IntCompare();
        charCompare.Init();
        this.TextCompare = new TextCompare();
        this.TextCompare.CharCompare = charCompare;
        this.TextCompare.Init();
        return true;
    }

    public virtual string HostName { get; set; }
    public virtual int HostPort { get; set; }
    public virtual int Status { get; set; }

    public virtual Network Network { get; set; }
    public virtual Thread Thread { get; set; }
    public virtual TimeInterval Interval { get; set; }
    protected virtual InfraInfra InfraInfra { get; set; }
    protected virtual ListInfra ListInfra { get; set; }
    protected virtual TextInfra TextInfra { get; set; }
    protected virtual StorageInfra StorageInfra { get; set; }
    protected virtual TextEncodeKindList TextEncodeKindList { get; set; }
    protected virtual StorageStatusList StorageStatusList { get; set; }
    protected virtual ClassInfra ClassInfra { get; set; }
    protected virtual ConsoleConsole ConsoleConsole { get; set; }
    protected virtual ClassTaskKindList ClassTaskKindList { get; set; }
    protected virtual ClassConsole ClassConsole { get; set; }
    protected virtual ClassSource ClassSource { get; set; }
    protected virtual ClassWrite ClassWrite { get; set; }
    protected virtual Text TextNewLine { get; set; }
    protected virtual TextCompare TextCompare { get; set; }

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

        network.HostName = this.HostName;
        network.HostPort = this.HostPort;

        NetworkStatusState statusState;
        statusState = new NetworkStatusState();
        statusState.Console = this;
        statusState.Init();

        NetworkCaseState caseState;
        caseState = new NetworkCaseState();
        caseState.Console = this;
        caseState.Init();

        NetworkReadyState readyState;
        readyState = new NetworkReadyState();
        readyState.Console = this;
        readyState.Init();

        network.StatusChangeState = statusState;
        network.CaseChangeState = caseState;
        network.ReadyReadState = readyState;

        NetworkOpenState openState;
        openState = new NetworkOpenState();
        openState.Console = this;
        openState.Init();

        TimeInterval interval;
        interval = new TimeInterval();
        interval.Init();

        this.Interval = interval;

        interval.Single = true;
        interval.Time = 0;
        interval.Elapse.State.AddState(openState);

        interval.Start();

        ThreadCurrent current;
        current = new ThreadCurrent();
        current.Init();

        Thread thread;
        thread = current.Thread;

        this.Thread = thread;

        // this.Log("ClassServer.Console:Console.Execute EventLoop Start");

        int o;
        o = thread.ExecuteEventLoop();

        // this.Log("ClassServer.Console:Console.Execute EventLoop End");

        network.Final();

        if (!(o == 0))
        {
            string k;
            k = o.ToString();

            this.Log("Console Exit Status: " + k);
        }
        
        this.Status = o;
        return true;
    }

    public virtual bool Log(string text)
    {
        string aa;
        aa = text + "\n";

        StorageStatusList statusList;
        statusList = this.StorageStatusList;

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
        a.Path = "ClassServer.Console.data/log.txt";
        a.Mode = mode;
        a.Open();

        if (a.Status == statusList.NoError)
        {
            long kn;
            kn = a.Stream.Count;

            a.Stream.PosSet(kn);

            if (a.Status == statusList.NoError)
            {
                TextEncode encode;
                encode = new TextEncode();
                encode.Kind = this.TextEncodeKindList.Utf8;
                encode.Init();

                Text o;
                o = this.TextInfra.TextCreateString(aa, null);
                int kk;
                kk = o.Range.Count;
                long ka;
                ka = encode.DataCountMax(kk);

                Data data;
                data = new Data();
                data.Count = ka;
                data.Init();

                long kb;
                kb = encode.Data(data, 0, o);

                encode.Final();

                long count;
                count = kb;
                DataRange range;
                range = new DataRange();
                range.Init();
                range.Count = count;

                a.Stream.Write(data, range);

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

    public virtual Data ExecuteClass(string sourceString)
    {
        // this.Log("Console.ExecueClass Start");

        Text textA;
        textA = this.TextInfra.TextCreateStringData(sourceString, null);

        Array text;
        text = this.TextInfra.TextArraySplit(textA, this.TextNewLine, this.TextCompare);

        this.ClassSource.Text = text;

        this.ClassConsole.ExecuteCreate();

        ClassNodeResult result;
        result = this.ClassConsole.Result.Node;

        ClassTokenResult tokenResult;
        tokenResult = this.ClassConsole.Result.Token;
        
        this.ClassConsole.Result = null;

        this.ClassSource.Text = null;

        Array aa;
        aa = result.Root;

        ClassNodeClass varClass;
        varClass = (ClassNodeClass)aa.Get(0);

        Array ab;
        ab = tokenResult.Code;

        ClassTokenCode code;
        code = (ClassTokenCode)ab.Get(0);

        ClassWrite write;
        write = this.ClassWrite;

        write.NodeClass = varClass;
        write.TokenCode = code;
        write.SourceText = text;

        write.Execute();

        Data data;
        data = write.Data;

        write.Data = null;

        write.SourceText = null;
        write.TokenCode = null;
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

        uint u;
        u = (uint)k;

        this.InfraInfra.DataMidSet(data, 0, u);

        // this.Log("Console.ExecueClass End");
        return data;
    }
}