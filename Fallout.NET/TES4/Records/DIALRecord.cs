using Fallout.NET.Core;
using Fallout.NET.TES4.SubRecords;
using Fallout.NET.TES4.SubRecords.DIAL;
using System;
using System.Collections.Generic;

namespace Fallout.NET.TES4.Records
{
    /// <summary>
    /// Dialog Topic
    /// </summary>
    public class DIALRecord : Record
    {
        /// <summary>
        /// Editor ID
        /// </summary>
        public STRSubRecord EDID { get; private set; }

        /// <summary>
        /// Quest
        /// </summary>
        /// <remarks>FormID of a <see cref="QUSTRecord"/></remarks>
        public List<FormID> QSTI { get; private set; } = new List<FormID>();

        /// <summary>
        /// Quest
        /// </summary>
        /// <remarks>FormID of a <see cref="QUSTRecord"/></remarks>
        public List<FormID> QSTR { get; private set; }= new List<FormID>();

        /// <summary>
        /// Name
        /// </summary>
        public STRSubRecord FULL { get; private set; }

        /// <summary>
        /// Priority
        /// </summary>
        public FloatSubRecord PNAM { get; private set; }

        /// <summary>
        /// Dialogue Data
        /// </summary>
        /// <remarks>Holds <see cref="DialogType"/> and <see cref="DialogFlags"/></remarks>
        public DIAL_DATASubRecord DATA { get; private set; }

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
                        case "QSTI":
                            var qsti = new FormID();
                            qsti.Deserialize(stream, name);
                            QSTI.Add(qsti);
                            break;
                        case "QSTR":
                            var qstr = new FormID();
                            qstr.Deserialize(stream, name);
                            QSTR.Add(qstr);
                            break;
                        case "FULL":
                            FULL = new STRSubRecord();
                            FULL.Deserialize(stream, name);
                            break;
                        case "PNAM":
                            PNAM = new FloatSubRecord();
                            PNAM.Deserialize(stream, name);
                            break;
                        case "DATA":
                            DATA = new DIAL_DATASubRecord();
                            DATA.Deserialize(stream, name);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}