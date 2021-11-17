using Fallout.NET.Core;
using Fallout.NET.TES4.SubRecords;
using Fallout.NET.TES4.SubRecords.CELL;

namespace Fallout.NET.TES4.Records
{
    /// <summary>
    /// Cell
    /// </summary>
    public class CELLRecord : Record
    {
        /// <summary>
        /// Editor ID
        /// </summary>
        public STRSubRecord EDID { get; private set; }

        /// <summary>
        /// Name
        /// </summary>
        public STRSubRecord FULL { get; private set; }

        /// <summary>
        /// Cell flags
        /// </summary>
        public CELL_DATASubRecord DATA { get; private set; }

        /// <summary>
        /// Lighting
        /// </summary>
        public BytesSubRecord XCLL { get; private set; }

        /// <summary>
        /// Music type
        /// </summary>
        /// <remarks>Applies to TES4 only</remarks>
        public ByteSubRecord XCMT { get; private set; }

        /// <summary>
        /// Regions
        /// </summary>
        /// todo: This is meant to be an array
        public FormID XCLR { get; private set; }

        /// <summary>
        /// Image Space
        /// </summary>
        /// <remarks>FormID of an <see cref="IMGSRecord"/></remarks>
        public FormID XCIM { get; private set; }

        /// <summary>
        /// Encounter Zone
        /// </summary>
        /// <remarks>FormID of an <see cref="ECZNRecord"/></remarks>
        public FormID XEZN { get; private set; }

        /// <summary>
        /// Climate
        /// </summary>
        /// <remarks>FormID of a <see cref="CLMTRecord"/></remarks>
        public FormID XCCM { get; private set; }

        /// <summary>
        /// Water
        /// </summary>
        /// <remarks>FormID of a <see cref="WATRRecord"/></remarks>
        public FormID XCWT { get; private set; }

        /// <summary>
        /// Owner
        /// </summary>
        /// <remarks>FormID of a <see cref="FACTRecord"/>, <see cref="ACHRRecord"/> or <see cref="NPC_Record"/></remarks>
        public FormID XOWN { get; private set; }

        /// <summary>
        /// Acoustic Space
        /// </summary>
        /// <remarks>FormID of an <see cref="ASPCRecord"/></remarks>
        public FormID XCAS { get; private set; }

        /// <summary>
        /// Music Type
        /// </summary>
        /// <remarks>FormID of a <see cref="MUSCRecord"/><para>Applied to Fallout 3/NV</para></remarks>
        public FormID XCMO { get; private set; }

        /// <summary>
        /// Global Variable (if owner is NPC)
        /// </summary>
        public UInt32SubRecord XGLB { get; private set; }

        /// <summary>
        /// Faction Rank (If owner is faction)
        /// </summary>
        public UInt32SubRecord XRNK { get; private set; }

        /// <summary>
        /// Water height (if not 0.00)
        /// </summary>
        public FloatSubRecord XCLW { get; private set; }

        /// <summary>
        /// (X, Y) grid (only used for exterior cells)
        /// </summary>
        public Vector2iSubRecord XCLC { get; private set; }

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

                        case "FULL":
                            FULL = new STRSubRecord();
                            FULL.Deserialize(stream, name);
                            break;

                        case "DATA":
                            DATA = new CELL_DATASubRecord();
                            DATA.Deserialize(stream, name);
                            break;

                        case "XCLL":
                            XCLL = new BytesSubRecord();
                            XCLL.Deserialize(stream, name);
                            break;

                        case "XCMT":
                            XCMT = new ByteSubRecord();
                            XCMT.Deserialize(stream, name);
                            break;

                        case "XGLB":
                            XGLB = new UInt32SubRecord();
                            XGLB.Deserialize(stream, name);
                            break;

                        case "XRNK":
                            XRNK = new UInt32SubRecord();
                            XRNK.Deserialize(stream, name);
                            break;

                        case "XCLW":
                            XCLW = new FloatSubRecord();
                            XCLW.Deserialize(stream, name);
                            break;

                        case "XCLC":
                            XCLC = new Vector2iSubRecord();
                            XCLC.Deserialize(stream, name);
                            break;

                        case "XCLR":
                            XCLR = new FormID();
                            XCLR.Deserialize(stream, name);
                            break;
                        case "XCIM":
                            XCIM = new FormID();
                            XCIM.Deserialize(stream, name);
                            break;
                        case "XEZN":
                            XEZN = new FormID();
                            XEZN.Deserialize(stream, name);
                            break;
                        case "XCCM":
                            XCCM = new FormID();
                            XCCM.Deserialize(stream, name);
                            break;
                        case "XCWT":
                            XCWT = new FormID();
                            XCWT.Deserialize(stream, name);
                            break;
                        case "XOWN":
                            XOWN = new FormID();
                            XOWN.Deserialize(stream, name);
                            break;
                        case "XCAS":
                            XCAS = new FormID();
                            XCAS.Deserialize(stream, name);
                            break;
                        case "XCMO":
                            XCMO = new FormID();
                            XCMO.Deserialize(stream, name);
                            break;

                        default:
                            var rest = stream.ReadUInt16();
                            stream.ReadBytes(rest);
                            break;
                    }
                }
            }            
        }

        public override string ToString()
        {
            return EDID.ToString();
        }
    }
}
