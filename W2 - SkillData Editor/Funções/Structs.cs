using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace W2___SkillData_Editor.Funções
{
    class Structs
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct STRUCT_SKILLDATA
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 248)]
            public SkillDataEntry[] Skill;

            public int Checksum;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct SkillDataEntry
        {
            public int SkillPoint;
            public int TargetType;
            public int ManaSpent;
            public int Delay;
            public int Range;
            public int InstanceType;
            public int InstanceValue;
            public int TickType;
            public int TickValue;
            public int AffectType;
            public int AffectValue;
            public int AffectTime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public sbyte[] Act1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public sbyte[] Act2;
            public int InstanceAttribute;
            public int TickAttribute;
            public int Aggressive;
            public int MaxTarget;
            public int PartyCheck;
            public int AffectResist;
            public int Passive;
            public int UseOnMacro;

        }
    }
}
 