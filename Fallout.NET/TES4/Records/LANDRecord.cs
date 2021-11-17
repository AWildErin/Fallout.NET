using Fallout.NET.Core;
using Fallout.NET.TES4.SubRecords;
using Fallout.NET.TES4.SubRecords.LAND;
using System.Collections.Generic;

namespace Fallout.NET.TES4.Records
{
    /// <summary>
    /// Landscape
    /// </summary>
    /// todo: possible rework how this class is.
    /// todo: missing some parameters
	public class LANDRecord : Record
	{
        /// <summary>
        /// Unknown
        /// </summary>
        public BytesSubRecord DATA { get; private set; }

        /// <summary>
        /// Vertex Normal
        /// </summary>
        public VNMLorVCLRSubRecord VNML { get; private set; }

        /// <summary>
        /// Vertex Colour
        /// </summary>
        public VNMLorVCLRSubRecord VCLR { get; private set; }

        /// <summary>
        /// Vertex Height Map
        /// </summary>
        public VHGTSubRecord VHGT { get; private set; }

        public List<ATXTorBTXTSubRecord> BTXT { get; private set; } = new List<ATXTorBTXTSubRecord>();
        public List<ATXTorBTXTSubRecord> ATXT { get; private set; } = new List<ATXTorBTXTSubRecord>();
        public List<VTXTSubRecord> VTXT { get; private set; } = new List<VTXTSubRecord>();
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
                        case "DATA":
                            DATA = new BytesSubRecord();
                            DATA.Deserialize(stream, name);
                            break;
                        case "VNML":
                            VNML = new VNMLorVCLRSubRecord();
                            VNML.Deserialize(stream, name);
                            break;
                        case "VCLR":
                            VCLR = new VNMLorVCLRSubRecord();
                            VCLR.Deserialize(stream, name);
                            break;
                        case "VHGT":
                            VHGT = new VHGTSubRecord();
                            VHGT.Deserialize(stream, name);
                            break;
                        case "BTXT":
                            var btxt = new ATXTorBTXTSubRecord();
                            btxt.Deserialize(stream, name);
                            BTXT.Add(btxt);

                            if(BTXT.Count > 1)
                            {
                                break;
                            }

                            break;
                        case "ATXT":
                            var atxt = new ATXTorBTXTSubRecord();
                            atxt.Deserialize(stream, name);
                            ATXT.Add(atxt);

                            if (ATXT.Count > 1)
                            {
                                break;
                            }

                            break;
                        case "VTXT":
                            var vtxt = new VTXTSubRecord();
                            vtxt.Deserialize(stream, name);
                            VTXT.Add(vtxt);

                            if (VTXT.Count > 1)
                            {
                                break;
                            }

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