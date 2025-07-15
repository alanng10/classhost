namespace ClassHost.Console;

class ClassWriteSetOperate : ClassWriteOperate
{
    public override bool ExecuteByte(long value)
    {
        ClassWrite write;
        write = this.Write;

        ClassWriteArg arg;
        arg = write.Arg;

        long index;
        index = arg.Index;

        Data data;
        data = arg.Data;

        long start;
        start = write.Start;

        long k;
        k = start + index;

        data.Set(k, value);

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