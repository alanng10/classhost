namespace ClassServer.Console;

class Network : NetworkNetwork
{
    public virtual Console Console { get; set; }
    
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
}