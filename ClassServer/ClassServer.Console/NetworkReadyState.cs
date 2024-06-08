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

        this.DataRange = new DataRange();
        this.DataRange.Init();

        this.DataCount = -1;

        this.CountData = new Data();
        this.CountData.Count = sizeof(int);
        this.CountData.Init();

        this.CountRange = new DataRange();
        this.CountRange.Init();
        this.CountRange.Count = this.CountData.Count;
        return true;
    }

    public virtual Console Console { get; set; }
    protected virtual InfraInfra InfraInfra { get; set; }
    protected virtual ConsoleConsole ConsoleConsole { get; set; }
    protected virtual StringCreate StringCreate { get; set; }
    protected virtual DataRange DataRange { get; set; }

    private int DataCount { get; set; }
    private Data CountData { get; set; }
    private DataRange CountRange { get; set; }

    public override bool Execute()
    {
        Console console;
        console = this.Console;

        Network network;
        network = console.Network;

        int stage;
        stage = console.Stage;
        
        NetworkCase cc;
        cc = network.Case;

        if (stage == 0)
        {
            return true;
        }

        long ka;
        ka = network.ReadyCount;
        
        int dataCount;
        dataCount = this.DataCount;

        bool b;
        b = (dataCount == -1);
        
        if (b)
        {
            if (ka < sizeof(int))
            {
                return true;
            }

            network.Stream.Read(this.CountData, this.CountRange);

            uint u;
            u = this.InfraInfra.DataMidGet(this.CountData, 0);
            
            int ke;
            ke = (int)u;
            if (ke < 0)
            {
                this.ConsoleConsole.Err.Write("Network received data count invalid");
                return true;
            }

            this.DataCount = ke;
        }

        if (!b)
        {
            if (ka < dataCount)
            {
                return true;
            }

            Data data;
            data = new Data();
            data.Count = dataCount;
            data.Init();

            this.DataRange.Count = dataCount;

            network.Stream.Read(data, this.DataRange);

            string text;
            text = this.StringCreate.Data(data, null);
        }
        

        return true;
    }
}