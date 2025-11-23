using System.Diagnostics.CodeAnalysis;

namespace Logic.Domain.Level5.Contract.Script;

public interface IFunctionCache<in TChecksum>
{
    bool TryAdd(string scriptName, string name);

    bool TryResolve(TChecksum checksum, [NotNullWhen(true)] out string? functionName);
}