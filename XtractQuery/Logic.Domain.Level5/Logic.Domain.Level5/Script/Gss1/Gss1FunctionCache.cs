using Kryptography.Checksum;
using Logic.Domain.Level5.InternalContract.Checksum;
using System.Diagnostics.CodeAnalysis;
using Logic.Domain.Level5.Contract.Script.Gss1;

namespace Logic.Domain.Level5.Script.Gss1;

internal class Gss1FunctionCache : IGss1FunctionCache
{
    private readonly Checksum<ushort> _hash;

    private readonly Dictionary<ushort, string> _lookup = [];

    public Gss1FunctionCache(IChecksumFactory checksums)
    {
        _hash = checksums.CreateCrc16();
    }

    public bool TryAdd(string scriptName, string name)
    {
        string fullName = scriptName.Replace('/', '.').Replace('\\', '.');
        fullName += $".{name}";

        return _lookup.TryAdd(_hash.ComputeValue(name), fullName);
    }

    public bool TryResolve(ushort checksum, [NotNullWhen(true)] out string? functionName)
    {
        return _lookup.TryGetValue(checksum, out functionName);
    }
}