namespace ClassHost.Console;

class ClassWriteOperate : Any
{
    public virtual ClassWrite Write { get; set; }

    public virtual bool ExecuteByte(long value)
    {
        return false;
    }

    public virtual bool ExecuteInt(long value)
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