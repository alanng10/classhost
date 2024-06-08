namespace ClassServer.Console;

class NetworkOpenState : State
{
    public virtual Console Console { get; set; }

    public override bool Execute()
    {
        Console aa;
        aa = this.Console;

        int stage;
        stage = aa.Stage;

        Network network;
        network = aa.Network;

        int k;
        k = 0;

        if (stage == 0)
        {
            k = 58501;    
        }

        if (stage == 1)
        {
            k = 58500;
        }

        network.ServerPort = k;

        network.Open();
        return true;
    }
}