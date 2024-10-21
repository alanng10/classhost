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

    public override bool CaseEvent()
    {
        NetworkCaseList caseList;
        caseList = this.NetworkCaseList;

        NetworkCase cc;
        cc = this.Case;

        if (cc == caseList.Connected)
        {
            // this.Console.Log("ClassServer.Console:NetworkCaseState.Execute Connected");
        }

        if (cc == caseList.Unconnected)
        {
            // this.Console.Log("ClassServer.Console:NetworkCaseState.Execute Unconnected");
        }

        return true;
    }
}