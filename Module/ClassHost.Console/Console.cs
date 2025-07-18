namespace ClassHost.Console;

class Console : TextAdd
{
    public override bool Init()
    {
        base.Init();
        this.InfraInfra = InfraInfra.This;
        this.ListInfra = ListInfra.This;
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
        this.ClassWrite.Start = sizeof(long);
        return true;
    }

    public virtual String HostName { get; set; }
    public virtual long HostPort { get; set; }
    public virtual long Status { get; set; }
    public virtual Network Network { get; set; }
    public virtual Thread Thread { get; set; }
    protected virtual InfraInfra InfraInfra { get; set; }
    protected virtual ListInfra ListInfra { get; set; }
    protected virtual StorageInfra StorageInfra { get; set; }
    protected virtual TextCodeKindList TextCodeKindList { get; set; }
    protected virtual StorageStatusList StorageStatusList { get; set; }
    protected virtual ConsoleConsole ConsoleConsole { get; set; }
    protected virtual ClassTaskKindList ClassTaskKindList { get; set; }
    protected virtual ClassConsole ClassConsole { get; set; }
    protected virtual ClassSource ClassSource { get; set; }
    protected virtual ClassWrite ClassWrite { get; set; }

    public virtual bool Execute()
    {
        this.ClassConsole.Load();

        ClassTask task;
        task = new ClassTask();
        task.Init();
        task.Kind = this.ClassTaskKindList.Node;
        task.Node = this.S("Class");

        this.ClassConsole.Task = task;

        this.ClassSource = new ClassSource();
        this.ClassSource.Init();
        this.ClassSource.Index = 0;
        this.ClassSource.Name = this.S("Code");

        Array array;
        array = this.ListInfra.ArrayCreate(1);
        array.SetAt(0, this.ClassSource);

        this.ClassConsole.Source = array;

        this.Network = new Network();
        this.Network.Console = this;
        this.Network.Init();

        this.Network.HostName = this.HostName;
        this.Network.HostPort = this.HostPort;

        ThreadThis current;
        current = new ThreadThis();
        current.Init();

        Thread thread;
        thread = current.Thread;

        this.Thread = thread;

        // this.Log("ClassServer.Console:Console.Execute EventLoop Start");

        this.Network.Open();

        long k;
        k = thread.ExecuteMain();

        // this.Log("ClassServer.Console:Console.Execute EventLoop End");

        this.Network.Final();

        if (!(k == 0))
        {
            String ka;
            ka = this.StringInt(k);

            String kk;
            kk = this.AddClear().AddS("Console Exit Status: ").Add(ka).AddResult();

            this.Log(kk);
        }

        this.Status = k;
        return true;
    }

    public virtual bool Log(String text)
    {
        String ka;
        ka = this.AddClear().Add(text).AddLine().AddResult();

        bool k;
        k = false;

        StorageMode mode;
        mode = new StorageMode();
        mode.Init();
        mode.Read = true;
        mode.Write = true;
        mode.Exist = true;

        Storage storage;
        storage = new Storage();
        storage.Init();
        storage.Path = this.S("ClassHost.Console.data/log.txt");
        storage.Mode = mode;
        storage.Open();

        if (storage.Status == this.StorageStatusList.NoError)
        {
            long pos;
            pos = storage.Stream.Count;

            storage.Stream.PosSet(pos);

            if (storage.Status == this.StorageStatusList.NoError)
            {
                TextCodeKind innKind;
                TextCodeKind outKind;
                innKind = this.TextCodeKindList.Utf32;
                outKind = this.TextCodeKindList.Utf8;

                Data data;
                data = this.TextInfra.StringDataCreateString(ka);

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

                storage.Stream.Write(result, range);

                if (storage.Status == this.StorageStatusList.NoError)
                {
                    k = true;
                }
            }
        }

        storage.Close();
        storage.Final();

        return k;
    }

    public virtual Data ExecuteClass(Text sourceText)
    {
        // this.Log("Console.ExecuteClass Start");

        Array text;
        text = this.TextLine(sourceText);

        this.ClassSource.Text = text;

        // this.Log("Console.ExecuteClass 1111");

        this.ClassConsole.ExecuteCreate();

        // this.Log("Console.ExecuteClass 2222");

        ClassNodeResult nodeResult;
        nodeResult = this.ClassConsole.Result.Node;

        ClassTokenResult tokenResult;
        tokenResult = this.ClassConsole.Result.Token;

        this.ClassConsole.Result = null;

        this.ClassSource.Text = null;

        Array rootArray;
        rootArray = nodeResult.Root;

        ClassNodeClass varClass;
        varClass = rootArray.GetAt(0) as ClassNodeClass;

        Array codeArray;
        codeArray = tokenResult.Code;

        ClassTokenCode code;
        code = codeArray.GetAt(0) as ClassTokenCode;

        ClassWrite write;
        write = this.ClassWrite;

        write.NodeClass = varClass;
        write.Error = nodeResult.Error;
        write.TokenCode = code;
        write.SourceText = text;

        // this.Log("Console.ExecuteClass 3333");
        
        write.Execute();

        // this.Log("Console.ExecuteClass 4444");

        Data data;
        data = write.Result;

        write.Result = null;

        write.SourceText = null;
        write.TokenCode = null;
        write.Error = null;
        write.NodeClass = null;

        long k;
        k = data.Count;

        k = k - write.Start;

        this.InfraInfra.DataIntSet(data, 0, k);

        return data;
    }
}