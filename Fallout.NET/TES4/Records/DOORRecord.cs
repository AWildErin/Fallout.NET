using Fallout.NET.Core;
using Fallout.NET.TES4.SubRecords;
using Fallout.NET.TES4.SubRecords.DOOR;
using Fallout.NET.TES4.SubRecords.STAT;

namespace Fallout.NET.TES4.Records
{
	public class DOORRecord : Record
	{
		/// <summary>
		/// Editor Id
		/// </summary>
		public STRSubRecord EDID { get; private set; }

		/// <summary>
		/// Name
		/// </summary>
		public STRSubRecord FULL { get; private set; }

		/// <summary>
		/// Model path.
		/// </summary>
		/// @todo: probably change this into a full sub record of it's own with 
		/// MODL, MODS, and MODD
		public STRSubRecord MODL { get; private set; }

		/// <summary>
		/// Script
		/// </summary>
		public FormID SCRI { get; private set; }

		/// <summary>
		/// Open Sound
		/// </summary>
		public FormID SNAM { get; private set; }

		/// <summary>
		/// Close Sound
		/// </summary>
		public FormID ANAM { get; private set; }

		/// <summary>
		/// Door Flags
		/// </summary>
		public DOOR_FNAMSubRecord FNAM { get; private set; }

		/// <summary>
		/// Loop Sound
		/// </summary>
		public FormID BNAM { get; private set; }

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
						case "MODL":
							MODL = new STRSubRecord();
							MODL.Deserialize(stream, name);
							break;
						case "SCRI":
							SCRI = new FormID();
							SCRI.Deserialize(stream, name);
							break;
						case "SNAM":
							SNAM = new FormID();
							SNAM.Deserialize(stream, name);
							break;
						case "ANAM":
							ANAM = new FormID();
							ANAM.Deserialize(stream, name);
							break;
						case "FNAM":
							FNAM = new DOOR_FNAMSubRecord();
							FNAM.Deserialize(stream, name);
							break;
						case "BNAM":
							BNAM = new FormID();
							BNAM.Deserialize(stream, name);
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