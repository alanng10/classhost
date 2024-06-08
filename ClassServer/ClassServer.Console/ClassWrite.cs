namespace ClassServer.Console;

class ClassWrite : Any
{
    public virtual int Start { get; set; }
    public virtual Data Data { get; set; }
    public virtual ClassWriteArg Arg { get; set; }

    public virtual bool Execute()
    {
        ClassWriteArg arg;
        arg = new ClassWriteArg();
        arg.Init();
        this.Arg = arg;

        


        this.Data = arg.Data;

        this.Arg = null;
        return true;
    }
}