﻿using Logic.Domain.Level5.Contract.Script.Gss1.DataClasses;

namespace Logic.Domain.Level5.Contract.Script.Gss1;

public interface IGss1ScriptWriter
{
    void Write(Gss1ScriptFile script, Stream output);

    void Write(Gss1ScriptContainer container, Stream output);
}