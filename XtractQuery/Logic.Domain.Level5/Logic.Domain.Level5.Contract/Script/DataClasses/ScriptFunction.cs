﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Domain.Level5.Contract.Script.DataClasses
{
    public class ScriptFunction
    {
        public string Name { get; set; }

        public short InstructionIndex { get; set; }
        public short InstructionCount { get; set; }

        public short JumpIndex { get; set; }
        public short JumpCount { get; set; }

        public int ParameterCount { get; set; }

        public short LocalCount { get; set; }
        public short ObjectCount { get; set; }
    }
}
