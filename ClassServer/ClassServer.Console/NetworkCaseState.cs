namespace ClassServer.Console;

class NetworkCaseState : State
{
    public override bool Init()
    {
        base.Init();
        this.NetworkCaseList = NetworkCaseList.This;
        return true;
    }

    public virtual Console Console { get; set; }
    protected virtual NetworkCaseList NetworkCaseList { get; set; }

    public override bool Execute()
    {
        NetworkCaseList caseList;
        caseList = this.NetworkCaseList;

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
            if (cc == caseList.Connected)
            {
                network.Close();
            }

            if (cc == caseList.Unconnected)
            {
                console.Stage = 1;

                console.Interval.Start();
            }
        }

        if (stage == 1)
        {
            
        }
        
        return true;
    }
}