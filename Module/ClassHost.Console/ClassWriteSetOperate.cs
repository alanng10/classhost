namespace ClassHost.Console;

class ClassWriteSetOperate : ClassWriteOperate
{
    public override bool ExecuteByte(long value)
    {
        ClassWriteArg arg;
        arg = this.Write.Arg;

        long index;
        index = arg.Index;

        arg.Data.Set(index, value);

        arg.Index = index + 1;
        return true;
    }

    public override bool ExecuteMemberStart()
    {
        ClassWriteArg arg;
        arg = this.Write.Arg;
        
        this.Write.ExecuteCount(arg.MemberCount);
        return true;
    }
}