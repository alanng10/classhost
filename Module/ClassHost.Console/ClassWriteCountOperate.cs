namespace ClassHost.Console;

class ClassWriteCountOperate : ClassWriteOperate
{
    public override bool ExecuteByte(long value)
    {
        ClassWriteArg arg;
        arg = this.Write.Arg;
        arg.Index = arg.Index + 1;
        return true;
    }

    public override bool ExecuteInt(long value)
    {
        ClassWriteArg arg;
        arg = this.Write.Arg;
        arg.Index = arg.Index + sizeof(long);
        return true;
    }

    public override bool ExecutePartStart()
    {
        ClassWriteArg arg;
        arg = this.Write.Arg;
        arg.Index = arg.Index + sizeof(long);
        return true;
    }

    public override bool ExecutePartEnd(long count)
    {
        ClassWriteArg arg;
        arg = this.Write.Arg;
        arg.PartCount = count;
        return true;
    }
}