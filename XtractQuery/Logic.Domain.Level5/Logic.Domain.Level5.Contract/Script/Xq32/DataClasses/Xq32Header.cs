﻿using Logic.Domain.Kuriimu2.KomponentAdapter.Contract.DataClasses;

namespace Logic.Domain.Level5.Contract.Script.Xq32.DataClasses
{
    public class Xq32Header
    {
        [FixedLength(4)]
        public string magic = "XQ32";

        public short functionEntryCount;
        public ushort functionOffset = 0x20 >> 2;

        public ushort jumpOffset;
        public short jumpEntryCount;

        public ushort instructionOffset;
        public short instructionEntryCount;

        public ushort argumentOffset;
        public short argumentEntryCount;

        public short globalVariableCount;
        public ushort stringOffset;
    }
}
