namespace ClassHost.Console;

class ClassWrite : Any
{
    public override bool Init()
    {
        base.Init();
        this.CountOperate = this.CreateCountOperate();
        this.SetOperate = this.CreateSetOperate();
        return true;
    }

    protected virtual ClassWriteCountOperate CreateCountOperate()
    {
        ClassWriteCountOperate a;
        a = new ClassWriteCountOperate();
        a.Write = this;
        a.Init();
        return a;
    }

    protected virtual ClassWriteSetOperate CreateSetOperate()
    {
        ClassWriteSetOperate a;
        a = new ClassWriteSetOperate();
        a.Write = this;
        a.Init();
        return a;
    }

    public virtual Console Console { get; set; }
    public virtual ClassNodeClass NodeClass { get; set; }
    public virtual Array Error { get; set; }
    public virtual ClassTokenCode TokenCode { get; set; }
    public virtual Array SourceText { get; set; }
    public virtual long Start { get; set; }
    public virtual Data Result { get; set; }
    public virtual ClassWriteArg Arg { get; set; }
    protected virtual ClassWriteOperate Operate { get; set; }
    protected virtual ClassWriteCountOperate CountOperate { get; set; }
    protected virtual ClassWriteSetOperate SetOperate { get; set; }

    public virtual bool Execute()
    {
        this.Arg = new ClassWriteArg();
        this.Arg.Init();

        ClassWriteArg arg;
        arg = this.Arg;

        this.Operate = this.CountOperate;

        this.ResetStage();
        this.ExecuteStage();

        long count;
        count = arg.Index;

        arg.Data = new Data();
        arg.Data.Count = count;
        arg.Data.Init();

        this.Operate = this.SetOperate;

        this.ResetStage();
        this.ExecuteStage();

        this.Result = arg.Data;

        this.Operate = null;
        this.Arg = null;
        return true;
    }

    public virtual bool ResetStage()
    {
        this.Arg.Index = this.Start;
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

        this.ExecuteOption(b);

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

            this.ExecutePart(varClass.Part.Value);

            this.ExecuteRange(varClass.Range);
        }

        return true;
    }

    protected virtual bool ExecutePart(Array array)
    {
        this.Operate.ExecutePartStart();

        long kk;
        kk = 0;

        long count;
        count = array.Count;

        long i;
        i = 0;
        while (i < count)
        {
            ClassNodeComp a;
            a = array.GetAt(i) as ClassNodeComp;

            if (!(a == null))
            {
                this.ExecuteComp(a);

                kk = kk + 1;
            }

            i = i + 1;
        }

        this.Operate.ExecutePartEnd(kk);
        return true;
    }

    protected virtual bool ExecuteComp(ClassNodeComp comp)
    {
        if (!((comp as ClassNodeField) == null))
        {
            this.ExecuteField(comp as ClassNodeField);
        }
        if (!((comp as ClassNodeMaide) == null))
        {
            this.ExecuteMaide(comp as ClassNodeMaide);
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
        this.ExecuteOptionString(name);

        this.ExecuteClassCount(varField.Count);

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
        this.ExecuteOptionString(name);

        this.ExecuteClassCount(varMaide.Count);

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
        this.ExecuteOptionString(varClass);
        return true;
    }

    protected virtual bool ExecuteRange(ClassInfraRange range)
    {
        long start;
        start = this.TokenTextStart(range.Start);
        
        long end;
        end = this.TokenTextEnd(range.End - 1);

        long index;
        index = start;

        long count;
        count = end - start;

        this.ExecuteIndex(index);
        this.ExecuteCount(count);
        return true;
    }

    protected virtual long TokenTextStart(long index)
    {
        ClassTokenCode code;
        code = this.TokenCode;

        ClassTokenToken token;
        token = code.Token.GetAt(index) as ClassTokenToken;

        Text line;
        line = this.SourceText.GetAt(token.Row) as Text;

        long k;
        k = line.Range.Index + token.Range.Index;

        return k;
    }

    protected virtual long TokenTextEnd(long index)
    {
        ClassTokenCode code;
        code = this.TokenCode;

        ClassTokenToken token;
        token = code.Token.GetAt(index) as ClassTokenToken;

        Text line;
        line = this.SourceText.GetAt(token.Row) as Text;

        long k;
        k = line.Range.Index + token.Range.Index + token.Range.Count;

        return k;
    }

    protected virtual bool ExecuteClassCount(ClassNodeCount count)
    {
        long k;
        k = this.CountData(count);

        this.ExecuteByte(k);
        return true;
    }

    protected virtual bool ExecuteOptionString(String value)
    {
        bool b;
        b = (value == null);

        this.ExecuteOption(b);

        if (!b)
        {
            this.ExecuteString(value);
        }
        return true;
    }

    protected virtual bool ExecuteErrorArray(Array array)
    {
        long count;
        count = array.Count;

        this.ExecuteCount(count);

        long i;
        i = 0;
        while (i < count)
        {
            ClassError a;
            a = array.GetAt(i) as ClassError;

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
                long prev;
                prev = start - 1;

                token = code.Token.GetAt(prev) as ClassTokenToken;

                tokenRange = token.Range;

                startRow = token.Row;
                startCol = tokenRange.Index + tokenRange.Count;
                endRow = startRow;
                endCol = startCol;
            }
        }
        if (!ba)
        {
            token = code.Token.GetAt(start) as ClassTokenToken;

            tokenRange = token.Range;

            startRow = token.Row;
            startCol = tokenRange.Index;

            bool bba;
            bba = (start < end);
            if (bba)
            {
                token = code.Token.GetAt(end - 1) as ClassTokenToken;

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

        this.ExecuteIndex(startRow);
        this.ExecuteIndex(startCol);
        this.ExecuteIndex(endRow);
        this.ExecuteIndex(endCol);
        return true;
    }

    protected virtual long CountData(ClassNodeCount count)
    {
        long a;
        a = 0;
        if (!((count as ClassNodePrusateCount) == null))
        {
            a = 0;
        }
        if (!((count as ClassNodePrecateCount) == null))
        {
            a = 1;
        }
        if (!((count as ClassNodePronateCount) == null))
        {
            a = 2;
        }
        if (!((count as ClassNodePrivateCount) == null))
        {
            a = 3;
        }
        return a;
    }

    protected virtual long OptionData(bool b)
    {
        long k;
        k = 0;
        if (!b)
        {
            k = 1;
        }
        return k;
    }

    protected virtual bool ExecuteOption(bool b)
    {
        long k;
        k = this.OptionData(b);

        this.ExecuteByte(k);
        return true;
    }

    protected virtual bool ExecuteString(String value)
    {
        Console console;
        console = this.Console;
 
        long count;
        count = console.StringCount(value);

        this.ExecuteCount(count);

        long i;
        i = 0;
        while (i < count)
        {
            long ka;
            ka = console.StringChar(value, i);

            this.ExecuteByte(ka);

            i = i + 1;
        }
        return true;
    }

    public virtual bool ExecuteIndex(long value)
    {
        return this.ExecuteInt(value);
    }

    public virtual bool ExecuteCount(long value)
    {
        return this.ExecuteInt(value);
    }

    protected virtual bool ExecuteInt(long value)
    {
        this.Operate.ExecuteInt(value);
        return true;
    }

    protected virtual bool ExecuteByte(long value)
    {
        this.Operate.ExecuteByte(value);
        return true;
    }
}