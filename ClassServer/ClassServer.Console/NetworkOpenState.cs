namespace ClassServer;

class NetworkOpenState : State
{
    public virtual NetworkThreadState ThreadState { get; set; }

    public override bool Execute()
    {
        NetworkThreadState aa;
        aa = this.ThreadState;

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