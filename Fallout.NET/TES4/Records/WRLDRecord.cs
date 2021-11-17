using Fallout.NET.Core;
using Fallout.NET.TES4.SubRecords;
using Fallout.NET.TES4.SubRecords.WRLD;
using System;

namespace Fallout.NET.TES4.Records
{
    /// <summary>
    /// Worldspace
    /// </summary>
    public class WRLDRecord : Record
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
        /// Parent Worldspace
        /// </summary>
        /// <remarks>FormID for a <see cref="WRLDRecord"/></remarks>
        public FormID WNAM { get; private set; }

        /// <summary>
        /// Sound
        /// </summary>
        /// <remarks>TES4 Only</remarks>
        public STRSubRecord SNAM { get; private set; }

        /// <summary>
        /// Large icon filename
        /// </summary>
        public STRSubRecord ICON { get; private set; }

        /// <summary>
        /// Climate
        /// </summary>
        /// <remarks>FormID for a <see cref="CLMTRecord"/></remarks>
        public FormID CNAM { get; private set; }

        /// <summary>
        /// Water
        /// </summary>
        /// <remarks>FormID for a <see cref="WATRRecord"/></remarks>
        public FormID NAM2 { get; private set; }

        /// <summary>
        /// LOD Water Type
        /// </summary>
        /// <remarks>FormID for a <see cref="WATRRecord"/></remarks>
        public FormID NAM3 { get; private set; }

        /// <summary>
        /// LOD Water Height
        /// </summary>
        public FloatSubRecord NAM4 { get; private set; }

        /// <summary>
        /// Land Data
        /// </summary>
        public WRLD_DNAMSubRecord DNAM { get; private set; }

        /// <summary>
        /// Map Data
        /// </summary>
        public WRLD_MNAMSubRecord MNAM { get; private set; }

        /// <summary>
        /// World Map Offset Data
        /// </summary>
        public WRLD_ONAMSubRecord ONAM { get; private set; }

        /// <summary>
        /// Image Space
        /// </summary>
        /// <remarks>FormID for an <see cref="IMGSRecord"/></remarks>
        public FormID INAM { get; private set; }

        /// <summary>
        /// Flags
        /// </summary>
        /// <remarks>Holds <see cref="WRLD_DATAFlags"/></remarks>
        public WRLD_DATASubRecord DATA { get; private set; }

        /// <summary>
        /// Min Object Bounds
        /// </summary>
        public Vector2fSubRecord NAM0 { get; private set; }

        /// <summary>
        /// Max Object Bounds
        /// </summary>
        public Vector2fSubRecord NAM9 { get; private set; }

        /// <summary>
        /// Canopy Shadow
        /// </summary>
        public STRSubRecord NNAM { get; private set; }

        /// <summary>
        /// Water Noise Texture
        /// </summary>
        public STRSubRecord XNAM { get; private set; }

        /// <summary>
        /// Unknown
        /// </summary>
        public UInt32SubRecord XXXX { get; private set; }

        /// <summary>
        /// Encounter Zone
        /// </summary>
        /// <remarks>FormID for a <see cref="ECZNRecord"/></remarks>
        public FormID XEZN { get; private set; }

        /// <summary>
        /// Parent Worldspace Flags
        /// </summary>
        /// <remarks>Holds <see cref="WRLD_PNAMFlags"/></remarks>
        public WRLD_PNAMSubRecord PNAM { get; private set; }

        /// <summary>
        /// Music
        /// </summary>
        /// <remarks>FormID for a <see cref="MUSCRecord"/></remarks>
        public FormID ZNAM { get; private set; }

        public uint Id => id;

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
                        case "WNAM":
                            WNAM = new FormID();
                            WNAM.Deserialize(stream, name);
                            break;
                        case "CNAM":
                            CNAM = new FormID();
                            CNAM.Deserialize(stream, name);
                            break;
                        case "NAM2":
                            NAM2 = new FormID();
                            NAM2.Deserialize(stream, name);
                            break;
                        case "NAM3":
                            NAM3 = new FormID();
                            NAM3.Deserialize(stream, name);
                            break;
                        case "NAM4":
                            NAM4 = new FloatSubRecord();
                            NAM4.Deserialize(stream, name);
                            break;
                        case "DNAM":
                            DNAM = new WRLD_DNAMSubRecord();
                            DNAM.Deserialize(stream, name);
                            break;
                        case "ICON":
                            ICON = new STRSubRecord();
                            ICON.Deserialize(stream, name);
                            break;
                        case "MNAM":
                            MNAM = new WRLD_MNAMSubRecord();
                            MNAM.Deserialize(stream, name);
                            break;
                        case "ONAM":
                            ONAM = new WRLD_ONAMSubRecord();
                            ONAM.Deserialize(stream, name);
                            break;
                        case "INAM":
                            INAM = new FormID();
                            INAM.Deserialize(stream, name);
                            break;
                        case "DATA":
                            DATA = new WRLD_DATASubRecord();
                            DATA.Deserialize(stream, name);
                            break;
                        case "NAM0":
                            NAM0 = new Vector2fSubRecord();
                            NAM0.Deserialize(stream, name);
                            break;
                        case "NAM9":
                            NAM9 = new Vector2fSubRecord();
                            NAM9.Deserialize(stream, name);
                            break;
                        case "NNAM":
                            NNAM = new STRSubRecord();
                            NNAM.Deserialize(stream, name);
                            break;
                        case "XNAM":
                            XNAM = new STRSubRecord();
                            XNAM.Deserialize(stream, name);
                            break;
                        case "XXXX":
                            //var xxxxSize = stream.ReadUInt16();
                            //var xxxxData = stream.ReadBytes(xxxxSize);
                            //var xxxxDataStr = System.Text.Encoding.ASCII.GetString(xxxxData);
                            XXXX = new UInt32SubRecord();
                            XXXX.Deserialize(stream, name);
                            break;
                        case "OFST":
                            var ofstSize = Convert.ToInt32(stream.ReadUInt16());
                            if (ofstSize == 0)
                            {
                                ofstSize = Convert.ToInt32(XXXX.Value);
                            }
                            var ofstData = stream.ReadBytes(ofstSize);
                            break;
                        case "XEZN":
                            XEZN = new FormID();
                            XEZN.Deserialize(stream, name);
                            break;
                        case "PNAM":
                            PNAM = new WRLD_PNAMSubRecord();
                            PNAM.Deserialize(stream, name);
                            break;
                        case "ZNAM":
                            ZNAM = new FormID();
                            ZNAM.Deserialize(stream, name);
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
