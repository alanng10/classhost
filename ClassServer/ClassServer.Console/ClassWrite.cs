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

    public virtual Console Console { get; set; }
    public virtual ClassNodeClass NodeClass { get; set; }
    public virtual Array Error { get; set; }
    public virtual ClassTokenCode TokenCode { get; set; }
    public virtual Array SourceText { get; set; }
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
            ClassNodeClassName kaa;
            
            kaa = varClass.Name;
            this.ExecuteClassName(kaa);

            if (!(kaa == null))
            {
                this.ExecuteRange(kaa.Range);
            }
            
            kaa = varClass.Base;
            this.ExecuteClassName(kaa);

            if (!(kaa == null))
            {
                this.ExecuteRange(kaa.Range);
            }

            this.ExecuteMember(varClass.Part.Value);

            this.ExecuteRange(varClass.Range);
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
            a = (ClassNodeComp)array.GetAt(i);

            // bool ba;
            // ba = (a == null);
            // this.Console.Log("ClassWrite.ExecuteMember ClassNodeComp is null: " + ba.ToString().ToLower());

            if (!(a == null))
            {
                this.ExecuteComp(a);

                kk = kk + 1;
            }

            i = i + 1;
        }

        // this.Console.Log("ClassWrite.ExecuteMember array count: " + count + ", comp count: " + kk);

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

    protected virtual bool ExecuteField(ClassNodeField varField)
    {
        this.ExecuteByte(0);

        this.ExecuteClassName(varField.Class);

        string name;
        name = null;
        if (!(varField.Name == null))
        {
            name = varField.Name.Value;
        }
        this.ExecuteOptionalString(name);

        int count;
        count = this.CountData(varField.Count);
        this.ExecuteByte(count);

        this.ExecuteRange(varField.Range);
        return true;
    }

    protected virtual bool ExecuteMaide(ClassNodeMaide varMaide)
    {
        this.ExecuteByte(1);
        
        this.ExecuteClassName(varMaide.Class);

        string name;
        name = null;
        if (!(varMaide.Name == null))
        {
            name = varMaide.Name.Value;
        }
        this.ExecuteOptionalString(name);

        int count;
        count = this.CountData(varMaide.Count);
        this.ExecuteByte(count);

        this.ExecuteRange(varMaide.Range);
        return true;
    }

    protected virtual bool ExecuteClassName(ClassNodeClassName a)
    {
        string varClass;
        varClass = null;
        if (!(a == null))
        {
            varClass = a.Value;
        }
        this.ExecuteOptionalString(varClass);
        return true;
    }

    protected virtual bool ExecuteRange(ClassInfraRange range)
    {
        int index;
        index = this.TokenTextStart(range.Start);
        
        int end;
        end = this.TokenTextEnd(range.End - 1);

        int count;
        count = end - index;

        this.ExecuteIndex(index);
        this.ExecuteCount(count);
        return true;
    }

    protected virtual int TokenTextStart(int index)
    {
        ClassTokenCode code;
        code = this.TokenCode;

        ClassTokenToken token;
        token = (ClassTokenToken)code.Token.GetAt(index);

        Text line;
        line = (Text)this.SourceText.GetAt(token.Row);

        Range tokenRange;
        tokenRange = token.Range;

        int k;
        k = line.Range.Index + tokenRange.Index;

        return k;
    }

    protected virtual int TokenTextEnd(int index)
    {
        ClassTokenCode code;
        code = this.TokenCode;

        ClassTokenToken token;
        token = (ClassTokenToken)code.Token.GetAt(index);

        Text line;
        line = (Text)this.SourceText.GetAt(token.Row);

        Range tokenRange;
        tokenRange = token.Range;

        int k;
        k = line.Range.Index + tokenRange.Index + tokenRange.Count;
        
        return k;
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

    protected virtual bool ExecuteErrorArray(Array array)
    {
        int count;
        count = array.Count;

        this.ExecuteCount(count);

        int i;
        i = 0;
        while (i < count)
        {
            ClassError a;
            a = (ClassError)array.GetAt(i);

            this.ExecuteError(a);

            i = i + 1;
        }
        return true;
    }

    protected virtual bool ExecuteError(ClassError error)
    {
        this.ExecuteString(error.Kind.Text);
        this.ExecuteErrorRange(error.Range);
        return true;
    }

    protected virtual bool ExecuteErrorRange(ClassInfraRange range)
    {
        ClassTokenCode code;
        code = this.TokenCode;

        int tokenCount;
        tokenCount = code.Token.Count;

        int start;
        int end;
        start = range.Start;
        end = range.End;

        int index;
        int count;
        index = 0;
        count = 0;

        bool ba;
        ba = (start == tokenCount);
        if (ba)
        {
            bool baa;
            baa = (tokenCount == 0);
            if (baa)
            {
                index = 0;
                count = 0;
            }
            if (!baa)
            {
                int previous;
                previous = start - 1;

                int tokenEnd;
                tokenEnd = this.TokenTextEnd(previous);

                index = tokenEnd;
                count = 0;
            }
        }
        if (!ba)
        {
            index = this.TokenTextStart(start);

            bool bba;
            bba = (start < end);
            if (bba)
            {
                int k;
                k = this.TokenTextEnd(end - 1);

                count = k - index;
            }
            if (!bba)
            {
                count = 0;
            }
        }

        this.ExecuteIndex(index);
        this.ExecuteCount(count);
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

    public virtual bool ExecuteIndex(int value)
    {
        return this.ExecuteSMid(value);
    }

    public virtual bool ExecuteCount(int value)
    {
        return this.ExecuteSMid(value);
    }

    public virtual bool ExecuteSMid(int value)
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