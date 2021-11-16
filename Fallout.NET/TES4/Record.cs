using Fallout.NET.Core;
using Fallout.NET.TES4.Records;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Fallout.NET.TES4
{
    public class Record
    {
        protected string type;
        protected uint dataSize;
        protected uint flags;
        protected uint id;
        protected uint revision;
        protected uint version;
        protected uint unknow;

        protected byte[] data;

        /// <summary>
        /// FormID of the current record
        /// </summary>
        public string FormId { get; private set; }

        /// <summary>
        /// A list of all the current found records
        /// </summary>
        /// @todo: Probably move this elsewhere.
        public static List<Record> All = new List<Record>();

        public string Type
        {
            get { return type; }
        }

        public bool Compressed
        {
            get { return (flags & 0x00040000) != 0; }
        }

        public bool Deleted
        {
            get { return (flags & 0x20) != 0; }
        }

        public bool Ignored
        {
            get { return (flags & 0x1000) != 0; }
        }

        public Record()
		{
            All.Add(this);
		}

        ~Record()
		{
            All.Remove(this);
		}

        public virtual void Deserialize(BetterReader reader, string name, GameID gameID)
        {
            type = name;
            dataSize = reader.ReadUInt32();
            flags = reader.ReadUInt32();
            id = reader.ReadUInt32();
            revision = reader.ReadUInt32();

            FormId = id.ToString("X");

            if (gameID != GameID.Oblivion)
            {
                version = reader.ReadUInt16();
                unknow = reader.ReadUInt16();
            }

            if (Deleted)
            {
                reader.ReadBytes((int)dataSize);
                return;
            }

            if (Compressed)
            {
                var decompSize = (int)reader.ReadUInt32();
                var compressedData = reader.ReadBytes((int)dataSize - 4);

                Utils.LogBuffer("\t\tCompressed Data {0}", type);

                var decompressedData = Decompress(compressedData);
                using (var betterReader = new BetterMemoryReader(decompressedData))
                    ExtractSubRecords(betterReader, gameID, (uint)betterReader.Length);
            }
            else
                ExtractSubRecords(reader, gameID, dataSize);
        }

        protected virtual void ExtractSubRecords(BetterReader reader, GameID gameID, uint size)
        {
            reader.ReadBytes((int)size);
        }

        private static byte[] Decompress(byte[] data)
        {
            using (var outMemoryStream = new MemoryStream())
            using (var outZStream = new ComponentAce.Compression.Libs.zlib.ZOutputStream(outMemoryStream))
            using (var inMemoryStream = new MemoryStream(data))
            {
                CopyStream(inMemoryStream, outZStream);
                outZStream.finish();
                return outMemoryStream.ToArray();
            }
        }

        public static void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[2000];
            int len;
            while ((len = input.Read(buffer, 0, 2000)) > 0)
            {
                output.Write(buffer, 0, len);
            }
            output.Flush();
        }

        public static Record CreateRecord(string name) => name switch
		{
            "ACHR" => new ACHRRecord(),
            "ACRE" => new ACRERecord(),
            "ACTI" => new ACTIRecord(),
            "ALCH" => new ALCHRecord(),
            "AMMO" => new AMMORecord(),
            "ANIO" => new ANIORecord(),
            "APPA" => new APPARecord(),
            "ARMO" => new ARMORecord(),
            "BOOK" => new BOOKRecord(),
            "BSGN" => new BSGNRecord(),
            "CELL" => new CELLRecord(),
            "CLAS" => new CLASRecord(),
            "CLMT" => new CLMTRecord(),
            "CLOT" => new CLOTRecord(),
            "CONT" => new CONTRecord(),
            "CREA" => new CREARecord(),
            "CSTY" => new CSTYRecord(),
            "DIAL" => new DIALRecord(),
            "DOOR" => new DOORRecord(),
            "EFSH" => new EFSHRecord(),
            "ENCH" => new ENCHRecord(),
            "EYES" => new EYESRecord(),
            "FACT" => new FACTRecord(),
            "FLOR" => new FLORRecord(),
            "FURN" => new FURNRecord(),
            "GLOB" => new GLOBRecord(),
            "GMST" => new GMSTRecord(),
            "GRAS" => new GRASRecord(),
            "HAIR" => new HAIRRecord(),
            "IDLE" => new IDLERecord(),
            "INFO" => new INFORecord(),
            "INGR" => new INGRRecord(),
            "KEYM" => new KEYMRecord(),
            "LAND" => new LANDRecord(),
            "LIGH" => new LIGHRecord(),
            "LSCR" => new LSCRRecord(),
            "LTEX" => new LTEXRecord(),
            "LVLC" => new LVLCRecord(),
            "LVLI" => new LVLIRecord(),
            "LVSP" => new LVSPRecord(),
            "MGEF" => new MGEFRecord(),
            "MISC" => new MISCRecord(),
            "NPC_" => new NPC_Record(),
            "PACK" => new PACKRecord(),
            "PGRD" => new PGRDRecord(),
            "QUST" => new QUSTRecord(),
            "RACE" => new RACERecord(),
            "REFR" => new REFRRecord(),
            "REGN" => new REGNRecord(),
            "ROAD" => new ROADRecord(),
            "SBSP" => new SBSPRecord(),
            "SCPT" => new SCPTRecord(),
            "SGST" => new SGSTRecord(),
            "SKIL" => new SKILRecord(),
            "SLGM" => new SLGMRecord(),
            "SOUN" => new SOUNRecord(),
            "SPEL" => new SPELRecord(),
            "STAT" => new STATRecord(),
            "TES4" => new TES4Record(),
            "TREE" => new TREERecord(),
            "WATR" => new WATRRecord(),
            "WEAP" => new WEAPRecord(),
            "WRLD" => new WRLDRecord(),
            "WTHR" => new WTHRRecord(),

            // We didn't find a record matching the name, so we just
            // add a blank record.
			_ => new Record(),
		};

        public static Record GetRecordById(string formid)
		{
            // Search through all the records to see if we have found
            // one with the supplied form id.
            var foundRecords = from record in All 
                               where record.FormId == formid 
                               select record;

            // Check if the enumerable actually has a record
            if (foundRecords.Count() >= 1)
            {
                return foundRecords.FirstOrDefault();
            }
            else
                return null;
		}
    }
}
