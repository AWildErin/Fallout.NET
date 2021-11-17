using Fallout.NET.Core;
using System;

namespace Fallout.NET.TES4.SubRecords.DOOR
{
    public class DOOR_FNAMSubRecord : SubRecord
    {
        public DOOR_FNAMFlags Flags { get; protected set; }

        public override void Deserialize(BetterReader reader, string name)
        {
            base.Deserialize(reader, name);

            Flags = (DOOR_FNAMFlags)reader.ReadByte();
        }
    }

    [Flags]
    public enum DOOR_FNAMFlags
    {
        Unknown = 0x01,
        AutomaticDoor = 0x02,
        Hidden = 0x04,
        MinimalUse = 0x08,
        SlidingDoor = 0x10
    }
}