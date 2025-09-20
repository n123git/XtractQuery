﻿using Logic.Business.Level5ScriptManagement.InternalContract.Decompression;
using Logic.Domain.Level5.Contract.Compression.DataClasses;
using Logic.Domain.Level5.Contract.Script.DataClasses;
using Logic.Domain.Level5.Contract.Script.Xseq;

namespace Logic.Business.Level5ScriptManagement.Decompression;

class DecompressXseqWorkflow(
    IXseqScriptDecompressor decompressor,
    IXseqScriptCompressor compressor)
    : IDecompressXseqWorkflow
{
    public void Decompress(Stream input, Stream output)
    {
        // Decompress script data
        ScriptContainer container = decompressor.Decompress(input);

        // Write script data
        compressor.Compress(container, output, CompressionType.None);
    }
}