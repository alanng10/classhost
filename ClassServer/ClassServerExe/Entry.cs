namespace ClassServerExe;

class Entry
{
    [STAThread]
    static int Main(string[] arg)
    {
        EntryEntry entry;
        entry = new ModuleEntry();
        entry.ArgSet(arg);
        entry.Init();
        int o;
        o = entry.Execute();
        return o;
    }
}