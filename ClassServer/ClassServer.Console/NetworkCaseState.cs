namespace ClassServer;

class NetworkCaseState : State
{
    public override bool Init()
    {
        base.Init();
        this.NetworkCaseList = NetworkCaseList.This;
        return true;
    }

    public virtual NetworkThreadState ThreadState { get; set; }
    protected virtual NetworkCaseList NetworkCaseList { get; set; }

    public override bool Execute()
    {
        NetworkCaseList caseList;
        caseList = this.NetworkCaseList;

        NetworkThreadState threadState;
        threadState = this.ThreadState;

        Network network;
        network = threadState.Network;

        int stage;
        stage = threadState.Stage;
        
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
                threadState.Stage = 1;

                threadState.Interval.Start();
            }
        }

        if (stage == 1)
        {
            
        }
        
        return true;
    }
}