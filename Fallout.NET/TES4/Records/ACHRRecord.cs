using Fallout.NET.Core;
using Fallout.NET.TES4.SubRecords;

namespace Fallout.NET.TES4.Records
{
    /// <summary>
    /// Placed NPC
    /// </summary>
    public class ACHRRecord : Record
    {
        /// <summary>
        /// Editor ID
        /// </summary>
        public STRSubRecord EDID { get; private set; }

        /// <summary>
        /// Base
        /// </summary>
        /// <remarks>FormID of an <see cref="NPC_Record"/></remarks>
        public FormID NAME { get; private set; }

        /// <summary>
        /// Encounter Zone
        /// </summary>
        /// <remarks>FormID of an <see cref="ECZNRecord"/></remarks>
        public FormID XEZN { get; private set; }

        /// <summary>
        /// Idle
        /// </summary>
        /// <remarks>Patrol data. FormID of an <see cref="IDLERecord"/> or null</remarks>
        public FormID INAM { get; private set; }

        /// <summary>
        /// Topic
        /// </summary>
        /// <remarks>Patrol data. FormID of a <see cref="DIALRecord"/> or null</remarks>
        public FormID TNAM { get; private set; }

        /// <summary>
        /// Merchant Container
        /// </summary>
        /// <remarks>FormID of a <see cref="REFRRecord"/></remarks>
        public FormID XMRC { get; private set; }

        /// <summary>
        /// Linked Reference
        /// </summary>
        /// <remarks>FormID of a <see cref="REFRRecord"/>, <see cref="ACRERecord"/>, <see cref="ACHRRecord"/>, <see cref="PGRERecord"/> or <see cref="PMISRecord"/></remarks>
        public FormID XLKR { get; private set; }

        /// <summary>
        /// Emittance
        /// </summary>
        /// <remarks>FormID of a <see cref="LIGHRecord"/> or <see cref="REGNRecord"/></remarks>
        public FormID XEMI { get; private set; }

        /// <summary>
        /// MultiBound Reference
        /// </summary>
        public FormID XMBR { get; private set; }

        /// <summary>
        /// Position and Rotation
        /// </summary>
        public PosAndRotSubRecord DATA { get; private set; }


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
                        case "NAME":
                            NAME = new FormID();
                            NAME.Deserialize(stream, name);
                            break;
                        case "XEZN":
                            XEZN = new FormID();
                            XEZN.Deserialize(stream, name);
                            break;
                        case "INAM":
                            INAM = new FormID();
                            INAM.Deserialize(stream, name);
                            break;
                        case "TNAM":
                            TNAM = new FormID();
                            TNAM.Deserialize(stream, name);
                            break;
                        case "XMRC":
                            XMRC = new FormID();
                            XMRC.Deserialize(stream, name);
                            break;
                        case "XLKR":
                            XLKR = new FormID();
                            XLKR.Deserialize(stream, name);
                            break;
                        case "XEMI":
                            XEMI = new FormID();
                            XEMI.Deserialize(stream, name);
                            break;
                        case "XMBR":
                            XMBR = new FormID();
                            XMBR.Deserialize(stream, name);
                            break;
                        case "DATA":
                            DATA = new PosAndRotSubRecord();
                            DATA.Deserialize(stream, name);
                            break;
                        default:
                            var rest = stream.ReadUInt16();
                            stream.ReadBytes(rest);
                            break;
                    }
                }
            }
        }
    }
}