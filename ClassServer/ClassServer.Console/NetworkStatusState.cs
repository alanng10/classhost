namespace ClassServer.Console;

class NetworkStatusState : State
{
    public override bool Init()
    {
        base.Init();
        this.NetworkStatusList = NetworkStatusList.This;
        return true;
    }

    public virtual Console Console { get; set; }
    protected virtual NetworkStatusList NetworkStatusList { get; set; }

    public override bool Execute()
    {
        NetworkStatusList statusList;
        statusList = this.NetworkStatusList;

        Console console;
        console = this.Console;

        Network network;
        network = console.Network;

        NetworkStatus status;
        status = network.Status;


        if (!(status == statusList.NoError))
        {
            network.Close();
            this.Console.Thread.ExitEventLoop(100 + status.Index);
        }
        return true;
    }
}