namespace ClassServer.Console;

class ClassWrite : Any
{
    public override bool Init()
    {
        base.Init();
        this.CountOperate = new ClassWriteCountOperate();
        this.CountOperate.Write = this;
        this.CountOperate.Init();

        this.SetOperate = new ClassWriteSetOperate();
        this.SetOperate.Write = this;
        this.SetOperate.Init();
        return true;
    }

    public virtual ClassNodeClass NodeClass { get; set; }
    public virtual int Start { get; set; }
    public virtual Data Data { get; set; }
    public virtual ClassWriteArg Arg { get; set; }
    protected virtual ClassWriteOperate Operate { get; set; }
    protected virtual ClassWriteCountOperate CountOperate { get; set; }
    protected virtual ClassWriteSetOperate SetOperate { get; set; }

    public virtual bool Execute()
    {
        ClassWriteArg arg;
        arg = new ClassWriteArg();
        arg.Init();
        this.Arg = arg;

        this.Operate = this.CountOperate;
        this.ResetStageIndex();
        this.ExecuteStage();

        int count;
        count = this.Start + arg.Index;

        Data data;
        data = new Data();
        data.Count = count;
        data.Init();

        arg.Data = data;

        this.Operate = this.SetOperate;
        this.ResetStageIndex();
        this.ExecuteStage();

        this.Data = arg.Data;

        this.Arg = null;
        return true;
    }

    protected virtual bool ResetStageIndex()
    {
        this.Arg.Index = 0;
        return true;
    }

    protected virtual bool ExecuteStage()
    {
        this.ExecuteClass(this.NodeClass);
        return true;
    }

    protected virtual bool ExecuteClass(ClassNodeClass varClass)
    {
        bool b;
        b = (varClass == null);
        
        int aa;
        aa = this.OptionalData(b);

        this.ExecuteByte(aa);

        if (!b)
        {
            string name;
            name = null;
            if (!(varClass.Name == null))
            {
                name = varClass.Name.Value;
            }
            this.ExecuteOptionalString(name);

            string varBase;
            varBase = null;
            if (!(varClass.Base == null))
            {
                varBase = varClass.Base.Value;
            }
            this.ExecuteOptionalString(varBase);

            this.ExecuteMember(varClass.Member.Value);
        }

        return true;
    }

    protected virtual bool ExecuteMember(Array array)
    {
        this.Operate.ExecuteMemberStart();
        
        int kk;
        kk = 0;
        int count;
        count = array.Count;
        int i;
        i = 0;
        while (i < count)
        {
            ClassNodeComp a;
            a = (ClassNodeComp)array.Get(i);

            if (!(a == null))
            {
                this.ExecuteComp(a);

                kk = kk + 1;
            }

            i = i + 1;
        }

        this.Operate.ExecuteMemberEnd(kk);
        return true;
    }

    protected virtual bool ExecuteComp(ClassNodeComp comp)
    {
        if (comp is ClassNodeField)
        {
            this.ExecuteField((ClassNodeField)comp);
        }
        if (comp is ClassNodeMaide)
        {
            this.ExecuteMaide((ClassNodeMaide)comp);
        }
        return true;
    }

    protected virtual bool ExecuteField(ClassNodeField field)
    {
        this.ExecuteByte(0);

        string varClass;
        varClass = null;
        if (!(field.Class == null))
        {
            varClass = field.Class.Value;
        }
        this.ExecuteOptionalString(varClass);

        string name;
        name = null;
        if (!(field.Name == null))
        {
            name = field.Name.Value;
        }
        this.ExecuteOptionalString(name);

        int count;
        count = this.CountData(field.Count);
        this.ExecuteByte(count);
        return true;
    }

    protected virtual bool ExecuteMaide(ClassNodeMaide maide)
    {
        this.ExecuteByte(1);
        
        string varClass;
        varClass = null;
        if (!(maide.Class == null))
        {
            varClass = maide.Class.Value;
        }
        this.ExecuteOptionalString(varClass);

        string name;
        name = null;
        if (!(maide.Name == null))
        {
            name = maide.Name.Value;
        }
        this.ExecuteOptionalString(name);

        int count;
        count = this.CountData(maide.Count);
        this.ExecuteByte(count);
        return true;
    }

    protected virtual bool ExecuteOptionalString(string value)
    {
        bool b;
        b = (value == null);
        
        int aa;
        aa = this.OptionalData(b);

        this.ExecuteByte(aa);

        if (!b)
        {
            this.ExecuteString(value);
        }
        return true;
    }

    protected virtual int CountData(ClassNodeCount count)
    {
        int a;
        a = 0;
        if (count is ClassNodePrudateCount)
        {
            a = 0;
        }
        if (count is ClassNodeProbateCount)
        {
            a = 1;
        }
        if (count is ClassNodePrecateCount)
        {
            a = 2;
        }
        if (count is ClassNodePrivateCount)
        {
            a = 3;
        }
        return a;
    }

    protected virtual int OptionalData(bool b)
    {
        int aa;
        aa = 0;
        if (!b)
        {
            aa = 1;
        }
        return aa;
    }

    protected virtual bool ExecuteString(string value)
    {
        int count;
        count = value.Length;

        this.ExecuteCount(count);

        int i;
        i = 0;
        while (i < count)
        {
            char oc;
            oc = value[i];

            byte ob;
            ob = (byte)oc;

            this.ExecuteByte(ob);

            i = i + 1;
        }
        return true;
    }

    public virtual bool ExecuteCount(int value)
    {
        return this.ExecuteMid((uint)value);
    }

    protected virtual bool ExecuteMid(uint value)
    {
        ulong a;
        a = value;
        return this.ExecuteInt(sizeof(uint), a);
    }

    protected virtual bool ExecuteInt(int count, ulong value)
    {
        int i;
        i = 0;
        while (i < count)
        {
            int shift;
            shift = i * 8;

            ulong k;
            k = value >> shift;
            k = k & 0xff;

            int oo;
            oo = (int)k;

            this.ExecuteByte(oo);

            i = i + 1;
        }
        return true;
    }

    protected virtual bool ExecuteByte(int value)
    {
        this.Operate.ExecuteByte(value);
        return true;
    }
}