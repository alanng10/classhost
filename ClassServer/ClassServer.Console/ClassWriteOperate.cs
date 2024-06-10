namespace ClassServer.Console;

class ClassWriteOperate : Any
{
    public virtual ClassWrite Write { get; set; }

    public virtual bool ExecuteByte(int value)
    {
        return false;
    }

    public virtual bool ExecuteMemberStart()
    {
        return false;
    }

    public virtual bool ExecuteMemberEnd(int count)
    {
        return false;
    }
}