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

        NetworkCase cc;
        cc = network.Case;
        
        if (cc == caseList.Connected)
        {
            
        }

        if (cc == caseList.Unconnected)
        {
            
        }
        
        return true;
    }
}