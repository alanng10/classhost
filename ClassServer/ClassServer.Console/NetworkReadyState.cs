namespace ClassServer.Console;

class NetworkReadyState : State
{
    public override bool Init()
    {
        base.Init();
        this.InfraInfra = InfraInfra.This;
        this.ConsoleConsole = ConsoleConsole.This;

        this.StringCreate = new StringCreate();
        this.StringCreate.Init();

        this.RangeInt = new RangeInt();
        this.RangeInt.Init();

        this.DataCount = -1;

        this.CountData = new Data();
        this.CountData.Count = sizeof(int);
        this.CountData.Init();

        this.CountRange = new RangeInt();
        this.CountRange.Init();
        this.CountRange.Count = this.CountData.Count;
        return true;
    }

    public virtual Console Console { get; set; }
    protected virtual InfraInfra InfraInfra { get; set; }
    protected virtual ConsoleConsole ConsoleConsole { get; set; }
    protected virtual StringCreate StringCreate { get; set; }
    protected virtual RangeInt RangeInt { get; set; }

    private int DataCount { get; set; }
    private Data CountData { get; set; }
    private RangeInt CountRange { get; set; }

    public override bool Execute()
    {
        // this.Console.Log("Network read Start");

        Console console;
        console = this.Console;

        Network network;
        network = console.Network;

        Stream stream;
        stream = network.Stream;

        long ka;
        ka = network.ReadyCount;
        
        int dataCount;
        dataCount = this.DataCount;

        bool b;
        b = (dataCount == -1);
        
        if (b)
        {
            int kk;
            kk = sizeof(int);

            if (ka < kk)
            {
                return true;
            }

            stream.Read(this.CountData, this.CountRange);
            
            uint u;
            u = this.InfraInfra.DataMidGet(this.CountData, 0);
            
            int ke;
            ke = (int)u;
            
            if (ke < 0)
            {
                this.Console.Log("Network received data count invalid");
                return true;
            }

            // this.Console.Log("Network received data count: " + ke.ToString());

            this.DataCount = ke;

            dataCount = ke;

            ka = ka - kk;

            b = false;
        }

        if (!b)
        {
            if (ka < dataCount)
            {
                return true;
            }

            // this.Console.Log("Read Data Count: " + dataCount);

            Data data;
            data = new Data();
            data.Count = dataCount;
            data.Init();

            RangeInt range;
            range = this.RangeInt;

            range.Count = dataCount;

            // this.Console.Log("Read Before 1111");

            stream.Read(data, range);

            // this.Console.Log("Read After 1111");

            string text;
            text = this.StringCreate.Data(data, null);

            // this.Console.Log("Read Text: " + text);

            // this.Console.Log("ExecuteClass Before");

            data = this.Console.ExecuteClass(text);

            // this.Console.Log("ExecuteClass After");

            // this.Console.Log("Write Data Count: " + data.Count);

            range.Count = data.Count;

            // this.Console.Log("Write Before 1111");

            stream.Write(data, range);

            // this.Console.Log("Write After 1111");

            this.DataCount = -1;
        }

        // this.Console.Log("Network read End");
        return true;
    }
}