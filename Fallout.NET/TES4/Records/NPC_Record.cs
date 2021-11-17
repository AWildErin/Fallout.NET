using Fallout.NET.Core;
using Fallout.NET.TES4.SubRecords;
using System.Collections.Generic;

namespace Fallout.NET.TES4.Records
{
    /// <summary>
    /// NPC
    /// </summary>
	public class NPC_Record : Record
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
        /// Model
        /// </summary>
        /// todo: turn this into it's own subrecord with the other stuff https://tes5edit.github.io/fopdoc/Fallout3/Records/Subrecords/Model.html
        public STRSubRecord MODL { get; private set; }

        /// <summary>
        /// Object Bounds
        /// </summary>
        public OBNDSubRecord OBND { get; private set; }

        /// <summary>
        /// Configuration
        /// </summary>
        public ACBSSubRecord ACBS { get; private set; }

        /// <summary>
        /// Death Item
        /// </summary>
        /// <remarks>FormID of a <see cref="LVLIRecord"/></remarks>
        public FormID INAM { get; private set; }

        /// <summary>
        /// Voice
        /// </summary>
        /// <remarks>FormID of a <see cref="VTYPRecord"/></remarks>
        public FormID VTCK { get; private set; }

        /// <summary>
        /// Template
        /// </summary>
        /// <remarks>FormID of a <see cref="NPC_Record"/> or <see cref="LVLNRecord"/></remarks>
        public FormID TPLT { get; private set; }

        /// <summary>
        /// Race
        /// </summary>
        /// <remarks>FormID of a <see cref="RACERecord"/></remarks>
        public FormID RNAM { get; private set; }

        /// <summary>
        /// Unarmed Attack Effect
        /// </summary>
        /// <remarks>FormID of an <see cref="ENCHRecord"/> or <see cref="SPELRecord"/></remarks>
        public FormID EITM { get; private set; }

        /// <summary>
        /// Script
        /// </summary>
        /// <remarks>FormID of a <see cref="SCPTRecord"/></remarks>
        public FormID SCRI { get; private set; }

        /// <summary>
        /// Class
        /// </summary>
        /// <remarks>FormID of a <see cref="CLASRecord"/></remarks>
        public FormID CNAM { get; private set; }

        /// <summary>
        /// Hair
        /// </summary>
        /// <remarks>FormID of a <see cref="HAIRRecord"/></remarks>
        public FormID HNAM { get; private set; }

        /// <summary>
        /// Eyes
        /// </summary>
        /// <remarks>FormID of an <see cref="EYESRecord"/></remarks>
        public FormID ENAM { get; private set; }

        /// <summary>
        /// Combat Style
        /// </summary>
        /// <remarks>FormID of a <see cref="CSTYRecord"/></remarks>
        public FormID ZNAM { get; private set; }

        /// <summary>
        /// Package
        /// </summary>
        /// <remarks>List of FormIDs for <see cref="PACKRecord"/></remarks>
        public List<FormID> PKID { get; private set; } = new List<FormID>();

        /// <summary>
        /// Head Part
        /// </summary>
        /// <remarks>List of FormIDs for <see cref="HDPTRecord"/></remarks>
        public List<FormID> PNAM { get; private set; } = new List<FormID>();

        /// <summary>
        /// Faction
        /// </summary>
        /// <remarks>List of <see cref="SNAMSubRecord"/></remarks>
        public List<SNAMSubRecord> SNAM { get; private set; } = new List<SNAMSubRecord>();

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
                        case "OBND":
                            OBND = new OBNDSubRecord();
                            OBND.Deserialize(stream, name);
                            break;
                        case "MODL":
                            MODL = new STRSubRecord();
                            MODL.Deserialize(stream, name);
                            break;
                        case "ACBS":
                            ACBS = new ACBSSubRecord();
                            ACBS.Deserialize(stream, name);
                            break;
                        case "SNAM":
                            var snam = new SNAMSubRecord();
                            snam.Deserialize(stream, name);
                            SNAM.Add(snam);
                            break;
                        case "INAM":
                            INAM = new FormID();
                            INAM.Deserialize(stream, name);
                            break;
                        case "VTCK":
                            VTCK = new FormID();
                            VTCK.Deserialize(stream, name);
                            break;
                        case "TPLT":
                            TPLT = new FormID();
                            TPLT.Deserialize(stream, name);
                            break;
                        case "RNAM":
                            RNAM = new FormID();
                            RNAM.Deserialize(stream, name);
                            break;
                        case "EITM":
                            EITM = new FormID();
                            EITM.Deserialize(stream, name);
                            break;
                        case "SCRI":
                            SCRI = new FormID();
                            SCRI.Deserialize(stream, name);
                            break;
                        case "PKID":
                            var pkid = new FormID();
                            pkid.Deserialize(stream, name);
                            PKID.Add(pkid);
                            break;
                        case "CNAM":
                            CNAM = new FormID();
                            CNAM.Deserialize(stream, name);
                            break;
                        case "PNAM":
                            var pnam = new FormID();
                            pnam.Deserialize(stream, name);
                            PNAM.Add(pnam);
                            break;
                        case "HNAM":
                            HNAM = new FormID();
                            HNAM.Deserialize(stream, name);
                            break;
                        case "ENAM":
                            ENAM = new FormID();
                            ENAM.Deserialize(stream, name);
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