namespace ClassServer.Console;

class NetworkOpenState : State
{
    public virtual Console Console { get; set; }

    public override bool Execute()
    {
        Console aa;
        aa = this.Console;

        Network network;
        network = aa.Network;

        network.Open();
        return true;
    }
}