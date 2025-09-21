﻿using Logic.Domain.Kuriimu2.KomponentAdapter.Contract;
using Logic.Domain.Level5.Contract.Script.DataClasses;
using Logic.Domain.Level5.Contract.Script.Gss1;
using Logic.Domain.Level5.Contract.Script.Gss1.DataClasses;

namespace Logic.Domain.Level5.Script.Gss1;

class Gss1ScriptReader(IBinaryFactory binaryFactory, IStreamFactory streamFactory) : IGss1ScriptReader
{
    public Gss1ScriptContainer Read(Stream input)
    {
        using IBinaryReaderX reader = binaryFactory.CreateReader(input, true);

        Gss1Header header = ReadHeader(reader);

        input.Position = header.functionOffset << 2;
        Gss1Function[] functions = ReadFunctions(reader, header.functionEntryCount);

        input.Position = header.jumpOffset << 2;
        Gss1Jump[] jumps = ReadJumps(reader, header.jumpEntryCount);

        input.Position = header.instructionOffset << 2;
        Gss1Instruction[] instructions = ReadInstructions(reader, header.instructionEntryCount);

        input.Position = header.argumentOffset << 2;
        Gss1Argument[] arguments = ReadArguments(reader, header.argumentEntryCount);

        int stringOffset = header.stringOffset << 2;
        Stream stringStream = streamFactory.CreateSubStream(input, stringOffset, input.Length - stringOffset);

        return new Gss1ScriptContainer
        {
            Functions = functions,
            Jumps = jumps,
            Instructions = instructions,
            Arguments = arguments,
            Strings = new ScriptStringTable
            {
                Stream = stringStream,
                BaseOffset = stringOffset
            },
            GlobalVariableCount = header.globalVariableCount
        };
    }

    private static Gss1Header ReadHeader(IBinaryReaderX reader)
    {
        return new Gss1Header
        {
            magic = reader.ReadString(4),
            functionEntryCount = reader.ReadInt16(),
            functionOffset = reader.ReadUInt16(),
            jumpOffset = reader.ReadUInt16(),
            jumpEntryCount = reader.ReadInt16(),
            instructionOffset = reader.ReadUInt16(),
            instructionEntryCount = reader.ReadInt16(),
            argumentOffset = reader.ReadUInt16(),
            argumentEntryCount = reader.ReadInt16(),
            globalVariableCount = reader.ReadInt16(),
            stringOffset = reader.ReadUInt16()
        };
    }

    private static Gss1Function[] ReadFunctions(IBinaryReaderX reader, int count)
    {
        var result = new Gss1Function[count];

        for (var i = 0; i < result.Length; i++)
            result[i] = ReadFunction(reader);

        return result;
    }

    private static Gss1Function ReadFunction(IBinaryReaderX reader)
    {
        return new Gss1Function
        {
            nameOffset = reader.ReadInt32(),
            crc16 = reader.ReadUInt16(),
            instructionOffset = reader.ReadInt16(),
            instructionEndOffset = reader.ReadInt16(),
            jumpOffset = reader.ReadInt16(),
            jumpCount = reader.ReadInt16(),
            localCount = reader.ReadInt16(),
            objectCount = reader.ReadInt16(),
            parameterCount = reader.ReadInt16()
        };
    }

    private static Gss1Jump[] ReadJumps(IBinaryReaderX reader, int count)
    {
        var result = new Gss1Jump[count];

        for (var i = 0; i < result.Length; i++)
            result[i] = ReadJump(reader);

        return result;
    }

    private static Gss1Jump ReadJump(IBinaryReaderX reader)
    {
        return new Gss1Jump
        {
            nameOffset = reader.ReadInt32(),
            crc16 = reader.ReadUInt16(),
            instructionIndex = reader.ReadInt16()
        };
    }

    private static Gss1Instruction[] ReadInstructions(IBinaryReaderX reader, int count)
    {
        var result = new Gss1Instruction[count];

        for (var i = 0; i < result.Length; i++)
            result[i] = ReadInstruction(reader);

        return result;
    }

    private static Gss1Instruction ReadInstruction(IBinaryReaderX reader)
    {
        return new Gss1Instruction
        {
            argOffset = reader.ReadInt16(),
            argCount = reader.ReadInt16(),
            returnParameter = reader.ReadInt16(),
            instructionType = reader.ReadInt16(),
            zero0 = reader.ReadInt32()
        };
    }

    private static Gss1Argument[] ReadArguments(IBinaryReaderX reader, int count)
    {
        var result = new Gss1Argument[count];

        for (var i = 0; i < result.Length; i++)
            result[i] = ReadArgument(reader);

        return result;
    }

    private static Gss1Argument ReadArgument(IBinaryReaderX reader)
    {
        return new Gss1Argument
        {
            type = reader.ReadByte(),
            value = reader.ReadUInt32()
        };
    }
}