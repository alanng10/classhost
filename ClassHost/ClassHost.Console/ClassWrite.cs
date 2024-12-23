namespace ClassHost.Console;

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

    public virtual bool ExecuteStage()
    {
        this.ExecuteClass(this.NodeClass);
        this.ExecuteErrorArray(this.Error);
        return true;
    }

    protected virtual bool ExecuteClass(ClassNodeClass varClass)
    {
        bool b;
        b = (varClass == null);
        
        long aa;
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
        long count;
        count = array.Count;
        long i;
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

        String name;
        name = null;
        if (!(varField.Name == null))
        {
            name = varField.Name.Value;
        }
        this.ExecuteOptionalString(name);

        long count;
        count = this.CountData(varField.Count);
        this.ExecuteByte(count);

        this.ExecuteRange(varField.Range);
        return true;
    }

    protected virtual bool ExecuteMaide(ClassNodeMaide varMaide)
    {
        this.ExecuteByte(1);
        
        this.ExecuteClassName(varMaide.Class);

        String name;
        name = null;
        if (!(varMaide.Name == null))
        {
            name = varMaide.Name.Value;
        }
        this.ExecuteOptionalString(name);

        long count;
        count = this.CountData(varMaide.Count);
        this.ExecuteByte(count);

        this.ExecuteRange(varMaide.Range);
        return true;
    }

    protected virtual bool ExecuteClassName(ClassNodeClassName a)
    {
        String varClass;
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
        long index;
        index = this.TokenTextStart(range.Start);
        
        long end;
        end = this.TokenTextEnd(range.End - 1);

        long count;
        count = end - index;

        this.ExecuteIndex((int)index);
        this.ExecuteCount((int)count);
        return true;
    }

    protected virtual long TokenTextStart(long index)
    {
        ClassTokenCode code;
        code = this.TokenCode;

        ClassTokenToken token;
        token = (ClassTokenToken)code.Token.GetAt(index);

        Text line;
        line = (Text)this.SourceText.GetAt(token.Row);

        Range tokenRange;
        tokenRange = token.Range;

        long k;
        k = line.Range.Index + tokenRange.Index;

        return k;
    }

    protected virtual long TokenTextEnd(long index)
    {
        ClassTokenCode code;
        code = this.TokenCode;

        ClassTokenToken token;
        token = (ClassTokenToken)code.Token.GetAt(index);

        Text line;
        line = (Text)this.SourceText.GetAt(token.Row);

        Range tokenRange;
        tokenRange = token.Range;

        long k;
        k = line.Range.Index + tokenRange.Index + tokenRange.Count;
        
        return k;
    }

    protected virtual bool ExecuteOptionalString(String value)
    {
        bool b;
        b = (value == null);
        
        long aa;
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
        count = (int)array.Count;

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

        long tokenCount;
        tokenCount = code.Token.Count;

        long start;
        long end;
        start = range.Start;
        end = range.End;

        long startRow;
        long startCol;
        long endRow;
        long endCol;
        startRow = 0;
        startCol = 0;
        endRow = 0;
        endCol = 0;

        ClassTokenToken token;

        Range tokenRange;

        bool ba;
        ba = (start == tokenCount);
        if (ba)
        {
            bool baa;
            baa = (tokenCount == 0);
            if (baa)
            {
                startRow = 0;
                startCol = 0;
                endRow = 0;
                endCol = 0;
            }
            if (!baa)
            {
                long previous;
                previous = start - 1;

                token = (ClassTokenToken)code.Token.GetAt(previous);

                tokenRange = token.Range;

                startRow = token.Row;
                startCol = tokenRange.Index + tokenRange.Count;
                endRow = startRow;
                endCol = startCol;
            }
        }
        if (!ba)
        {
            token = (ClassTokenToken)code.Token.GetAt(start);

            tokenRange = token.Range;

            startRow = token.Row;
            startCol = tokenRange.Index;

            bool bba;
            bba = (start < end);
            if (bba)
            {
                token = (ClassTokenToken)code.Token.GetAt(end - 1);

                tokenRange = token.Range;

                endRow = token.Row;
                endCol = tokenRange.Index + tokenRange.Count;
            }
            if (!bba)
            {
                endRow = startRow;
                endCol = startCol;
            }
        }

        this.ExecuteIndex((int)startRow);
        this.ExecuteIndex((int)startCol);
        this.ExecuteIndex((int)endRow);
        this.ExecuteIndex((int)endCol);
        return true;
    }

    protected virtual long CountData(ClassNodeCount count)
    {
        long a;
        a = 0;
        if (count is ClassNodePrusateCount)
        {
            a = 0;
        }
        if (count is ClassNodePrecateCount)
        {
            a = 1;
        }
        if (count is ClassNodePronateCount)
        {
            a = 2;
        }
        if (count is ClassNodePrivateCount)
        {
            a = 3;
        }
        return a;
    }

    protected virtual long OptionalData(bool b)
    {
        long aa;
        aa = 0;
        if (!b)
        {
            aa = 1;
        }
        return aa;
    }

    protected virtual bool ExecuteString(String value)
    {
        Console console;
        console = this.Console;
 
        int count;
        count = (int)console.StringCount(value);

        this.ExecuteCount(count);

        int i;
        i = 0;
        while (i < count)
        {
            long oc;
            oc = console.StringChar(value, i);

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

    protected virtual bool ExecuteInt(long count, ulong value)
    {
        long i;
        i = 0;
        while (i < count)
        {
            int shift;
            shift = (int)(i * 8);

            ulong k;
            k = value >> shift;
            k = k & 0xff;

            long oo;
            oo = (long)k;

            this.ExecuteByte(oo);

            i = i + 1;
        }
        return true;
    }

    protected virtual bool ExecuteByte(long value)
    {
        this.Operate.ExecuteByte(value);
        return true;
    }
}