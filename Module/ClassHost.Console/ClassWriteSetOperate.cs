namespace ClassHost.Console;

class ClassWriteSetOperate : ClassWriteOperate
{
    public override bool Init()
    {
        base.Init();
        this.InfraInfra = InfraInfra.This;
        return true;
    }

    protected virtual InfraInfra InfraInfra { get; set; }

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

    public override bool ExecuteInt(long value)
    {
        ClassWriteArg arg;
        arg = this.Write.Arg;

        long index;
        index = arg.Index;

        this.InfraInfra.DataIntSet(arg.Data, index, value);

        arg.Index = index + sizeof(long);
        return true;
    }

    public override bool ExecutePartStart()
    {
        ClassWriteArg arg;
        arg = this.Write.Arg;

        this.Write.ExecuteCount(arg.PartCount);
        return true;
    }
}