using Kryptography.Checksum;
using Logic.Domain.Level5.InternalContract.Checksum;
using System.Diagnostics.CodeAnalysis;
using Logic.Domain.Level5.Contract.Script.Xseq;

namespace Logic.Domain.Level5.Script.Xseq;

internal class XseqFunctionCache : IXseqFunctionCache
{
    private readonly Checksum<ushort> _hash;

    private readonly Dictionary<ushort, string> _lookup = [];

    public XseqFunctionCache(IChecksumFactory checksums)
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