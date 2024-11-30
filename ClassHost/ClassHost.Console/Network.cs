namespace ClassServer.Console;

class Network : NetworkNetwork
{
    public override bool Init()
    {
        base.Init();
        this.InfraInfra = InfraInfra.This;
        this.ListInfra = ListInfra.This;
        this.TextInfra = TextInfra.This;
        this.ConsoleConsole = ConsoleConsole.This;

        this.StringComp = StringComp.This;

        this.Range = new Range();
        this.Range.Init();

        this.Count = -1;

        this.ProtoCase = -1;

        this.CaseData = new Data();
        this.CaseData.Count = 1;
        this.CaseData.Init();

        this.CaseRange = new Range();
        this.CaseRange.Init();
        this.CaseRange.Count = this.CaseData.Count;

        this.CountData = new Data();
        this.CountData.Count = sizeof(int);
        this.CountData.Init();

        this.CountRange = new Range();
        this.CountRange.Init();
        this.CountRange.Count = this.CountData.Count;
        return true;
    }

    public virtual Console Console { get; set; }
    protected virtual InfraInfra InfraInfra { get; set; }
    protected virtual ListInfra ListInfra { get; set; }
    protected virtual TextInfra TextInfra { get; set; }
    protected virtual ConsoleConsole ConsoleConsole { get; set; }
    protected virtual StringComp StringComp { get; set; }
    protected virtual Range Range { get; set; }

    private long ProtoCase { get; set; }
    private long Count { get; set; }
    private Data CaseData { get; set; }
    private Range CaseRange { get; set; }
    private Data CountData { get; set; }
    private Range CountRange { get; set; }
    protected virtual Data Data { get; set; }
    protected virtual long Index { get; set; }
    
    public override bool StatusEvent()
    {
        NetworkStatusList statusList;
        statusList = this.NetworkStatusList;

        Console console;
        console = this.Console;

        NetworkStatus status;
        status = this.Status;

        if (!(status == statusList.NoError))
        {
            this.Close();
            console.Thread.Exit(100 + status.Index);
        }
        return true;
    }

    public override bool CaseEvent()
    {
        NetworkCaseList caseList;
        caseList = this.NetworkCaseList;

        NetworkCase cc;
        cc = this.Case;

        if (cc == caseList.Connected)
        {
            // this.Console.Log("ClassServer.Console:NetworkCaseState.Execute Connected");
        }

        if (cc == caseList.Unconnected)
        {
            // this.Console.Log("ClassServer.Console:NetworkCaseState.Execute Unconnected");
        }

        return true;
    }

    public override bool DataEvent()
    {
        // this.Console.Log("Network read Start");

        Console console;
        console = this.Console;

        long ka;
        ka = this.ReadyCount;

        if (this.ProtoCase == -1)
        {
            if (ka < 1)
            {
                return true;
            }

            this.Stream.Read(this.CaseData, this.CaseRange);

            ka = ka - this.CaseRange.Count;

            this.ProtoCase = this.CaseData.Get(0);
        }

        if (this.ProtoCase == 0)
        {
            ka = this.CountGet(ka, 1);
        }

        if (this.ProtoCase == 1)
        {
            if (ka < this.Count)
            {
                return true;
            }

            // this.Console.Log("Read Data Count: " + dataCount);

            Data data;
            data = new Data();
            data.Count = this.Count;
            data.Init();

            Range range;
            range = this.Range;

            range.Count = this.Count;

            // this.Console.Log("Read Before 1111");

            this.Stream.Read(data, range);

            // this.Console.Log("Read After 1111");

            long charCount;
            charCount = data.Count / sizeof(ushort);

            Data textData;
            textData = this.CreateTextData(data, 0, charCount);

            long textCount;
            textCount = textData.Count / sizeof(uint);

            Text text;
            text = new Text();
            text.Init();
            text.Data = textData;
            text.Range = new Range();
            text.Range.Init();
            text.Range.Count = textCount;

            // this.Console.Log("Read Text: " + text);

            // this.Console.Log("ExecuteClass Before");

            data = this.Console.ExecuteClass(text);

            // this.Console.Log("ExecuteClass After");

            // this.Console.Log("Write Data Count: " + data.Count);

            range.Count = data.Count;

            // this.Console.Log("Write Before 1111");

            this.Stream.Write(data, range);

            // this.Console.Log("Write After 1111");

            this.Count = -1;

            this.ProtoCase = -1;
        }

        if (this.ProtoCase == 10)
        {
            ka = this.CountGet(ka, 11);
        }

        if (this.ProtoCase == 11)
        {
            Data data;
            data = new Data();
            data.Count = this.Count;
            data.Init();

            Range range;
            range = this.Range;

            range.Count = this.Count;

            this.Stream.Read(data, range);

            this.Data = data;
            this.Index = 0;

            Array pathArray;
            pathArray = this.ReadStringArray();
        }

        // this.Console.Log("Network read End");
        return true;
    }

    protected virtual Array ReadStringArray()
    {
        long count;
        count = this.ReadCount();

        if (count == -1)
        {
            return null;
        }

        Array array;
        array = this.ListInfra.ArrayCreate(count);

        long i;
        i = 0;
        while (i < count)
        {
            String a;
            a = this.ReadString();

            if (a == null)
            {
                return null;
            }

            array.SetAt(i, a);

            i = i + 1;
        }

        return array;
    }

    protected virtual String ReadString()
    {
        long count;
        count = this.ReadCount();

        if (count == -1)
        {
            return null;
        }

        Data data;
        data = this.CreateTextData(this.Data, this.Index, count);

        String a;
        a = this.StringComp.CreateData(data, null);

        this.Index = this.Index + count * sizeof(ushort);

        return a;
    }

    protected virtual long ReadCount()
    {
        return this.ReadInt();
    }

    protected virtual long ReadInt()
    {
        long ka;
        ka = sizeof(uint);

        if (!this.InfraInfra.ValidRange(this.Data.Count, this.Index, ka))
        {
            return -1;
        }

        uint k;
        k = this.InfraInfra.DataMidGet(this.Data, this.Index);
        
        int kh;
        kh = (int)k;

        long a;
        a = kh;

        this.Index = this.Index + ka;

        return a;
    }

    protected virtual long CountGet(long dataCount, long nextCase)
    {
        int kk;
        kk = sizeof(int);

        if (dataCount < kk)
        {
            return dataCount;
        }

        this.Stream.Read(this.CountData, this.CountRange);

        uint u;
        u = this.InfraInfra.DataMidGet(this.CountData, 0);

        int ke;
        ke = (int)u;

        if (ke < 0)
        {
            this.Console.Log(this.TextInfra.S("Network received count invalid"));
        }

        // this.Console.Log("Network received data count: " + ke.ToString());

        this.Count = ke;

        dataCount = dataCount - kk;

        this.ProtoCase = nextCase;

        return dataCount;
    }

    protected virtual Data CreateTextData(Data data, long dataIndex, long charCount)
    {
        InfraInfra infraInfra;
        infraInfra = this.InfraInfra;

        long count;
        count = charCount;

        Data k;
        k = new Data();
        k.Count = count * sizeof(uint);
        k.Init();

        long i;
        i = 0;
        while (i < count)
        {
            long indexA;
            long indexB;
            indexA = dataIndex + i * sizeof(ushort);
            indexB = i * sizeof(uint);

            ushort kk;
            kk = infraInfra.DataShortGet(data, indexA);

            uint na;
            na = kk;

            infraInfra.DataCharSet(k, indexB, na);

            i = i + 1;
        }

        Data a;
        a = k;
        return a;
    }
}