namespace ClassServer.Console;

class ClassWriteOperate : Any
{
    public virtual ClassWrite Write { get; set; }

    public virtual bool ExecuteByte(int value)
    {
        return false;
    }
}