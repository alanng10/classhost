namespace ClassServer.Console;

class ClassWriteCountOperate : ClassWriteOperate
{
    public override bool ExecuteByte(long value)
    {
        ClassWriteArg arg;
        arg = this.Write.Arg;
        arg.Index = arg.Index + 1;
        return true;
    }

    public override bool ExecuteMemberStart()
    {
        ClassWriteArg arg;
        arg = this.Write.Arg;
        arg.Index = arg.Index + sizeof(int);
        return true;
    }

    public override bool ExecuteMemberEnd(int count)
    {
        ClassWriteArg arg;
        arg = this.Write.Arg;
        arg.MemberCount = count;
        return true;
    }
}