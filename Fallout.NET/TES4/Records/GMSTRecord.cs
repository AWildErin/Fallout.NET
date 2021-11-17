using Fallout.NET.Core;
using System;

namespace Fallout.NET.TES4.Records
{
    /// <summary>
    ///  Game Setting
    /// </summary>
    public class GMSTRecord : Record
    {
        protected STRSubRecord _stringData { get; set; }
        protected UInt32SubRecord _intData { get; set; }
        protected FloatSubRecord _floatData { get; set; }

        /// <summary>
        /// Editor ID
        /// </summary>
        public STRSubRecord EDID { get; private set; }

        /// <summary>
        /// Data
        /// </summary>
        /// <remarks>The value is interpreted as a <c>string</c> if the Editor ID starts with <c>s</c>, or as a <c>float</c> if it starts with <c>f</c>. Otherwise it is interpreted as an <c>int32</c>.</remarks>
        public SubRecord DATA { get; private set; }

        protected override void ExtractSubRecords(BetterReader reader, GameID gameID, uint size)
        {
            var bytes = reader.ReadBytes((int)size);
            var name = string.Empty;

            using (var stream = new BetterMemoryReader(bytes))
            {
                var end = stream.Length;

                while (stream.Position < end)
                {
                    name = stream.ReadString(4);

                    switch (name)
                    {
                        case "EDID":
                            EDID = new STRSubRecord();
                            EDID.Deserialize(stream, name);
                            break;
                        case "DATA":
                            switch (EDID.Value[0])
                            {
                                case 's':
                                    DATA = new STRSubRecord();
                                    ((STRSubRecord)DATA).Deserialize(stream, name);
                                    break;
                                case 'f':
                                    DATA = new FloatSubRecord();
                                    ((FloatSubRecord)DATA).Deserialize(stream, name);
                                    break;
                                default:
                                    DATA = new UInt32SubRecord();
                                    ((UInt32SubRecord)DATA).Deserialize(stream, name);
                                    break;
                            }
                            break;
                    }
                }
            }
        }
    }
}