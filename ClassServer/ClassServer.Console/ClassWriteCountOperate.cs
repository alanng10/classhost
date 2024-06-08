namespace ClassServer.Console;

class ClassWriteCountOperate : ClassWriteOperate
{
    public override bool ExecuteByte(int value)
    {
        ClassWriteArg arg;
        arg = this.Write.Arg;
        arg.Index = arg.Index + 1;
        return true;
    }
}