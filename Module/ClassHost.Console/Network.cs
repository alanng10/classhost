namespace ClassHost.Console;

class Network : NetworkNetwork
{
    public override bool Init()
    {
        base.Init();
        this.InfraInfra = InfraInfra.This;
        this.TextInfra = TextInfra.This;
        this.StringComp = StringComp.This;
        this.ConsoleConsole = ConsoleConsole.This;

        this.Range = new Range();
        this.Range.Init();

        this.ProtoCase = -1;

        this.CaseData = new Data();
        this.CaseData.Count = 1;
        this.CaseData.Init();

        this.CaseRange = new Range();
        this.CaseRange.Init();
        this.CaseRange.Count = this.CaseData.Count;

        this.CountData = new Data();
        this.CountData.Count = sizeof(long);
        this.CountData.Init();

        this.CountRange = new Range();
        this.CountRange.Init();
        this.CountRange.Count = this.CountData.Count;
        return true;
    }

    public virtual Console Console { get; set; }
    protected virtual InfraInfra InfraInfra { get; set; }
    protected virtual TextInfra TextInfra { get; set; }
    protected virtual StringComp StringComp { get; set; }
    protected virtual ConsoleConsole ConsoleConsole { get; set; }
    protected virtual Range Range { get; set; }
    protected virtual long ProtoCase { get; set; }
    protected virtual Data CaseData { get; set; }
    protected virtual Range CaseRange { get; set; }
    protected virtual Data CountData { get; set; }
    protected virtual Range CountRange { get; set; }

    public override bool StatusEvent()
    {
        if (!(this.Status == this.NetworkStatusList.NoError))
        {
            this.Close();
            this.Console.Thread.Exit(100 + this.Status.Index);
        }
        return true;
    }

    public override bool CaseEvent()
    {
        if (this.Case == this.NetworkCaseList.Connected)
        {
            // this.Console.Log("ClassServer.Console:NetworkCaseState.Execute Connected");
        }

        if (this.Case == this.NetworkCaseList.Unconnected)
        {
            // this.Console.Log("ClassServer.Console:NetworkCaseState.Execute Unconnected");
        }

        return true;
    }

    public override bool DataEvent()
    {
        // this.Console.Log("Network read Start");

        long readyCount;
        readyCount = this.ReadyCount;

        if (this.ProtoCase == -1)
        {
            if (readyCount < this.CaseRange.Count)
            {
                return true;
            }

            this.Stream.Read(this.CaseData, this.CaseRange);

            readyCount = readyCount - this.CaseRange.Count;

            this.ProtoCase = this.CaseData.Get(0);
        }

        if (this.ProtoCase == 0)
        {
            long kk;
            kk = sizeof(long);

            if (readyCount < kk)
            {
                return true;
            }

            this.Stream.Read(this.CountData, this.CountRange);

            long ke;
            ke = this.InfraInfra.DataIntGet(this.CountData, 0);

            long dataCount;
            dataCount = ke;

            readyCount = readyCount - kk;

            if (readyCount < dataCount)
            {
                return true;
            }

            // this.Console.Log("Read Data Count: " + dataCount);

            Data data;
            data = new Data();
            data.Count = dataCount;
            data.Init();

            Range range;
            range = this.Range;

            range.Count = data.Count;

            // this.Console.Log("Read Before 1111");

            this.Stream.Read(data, range);

            // this.Console.Log("Read After 1111");

            long textCount;
            textCount = data.Count / sizeof(int);

            Text text;
            text = new Text();
            text.Init();
            text.Data = data;
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

            this.ProtoCase = -1;
        }

        // this.Console.Log("Network read End");
        return true;
    }
}