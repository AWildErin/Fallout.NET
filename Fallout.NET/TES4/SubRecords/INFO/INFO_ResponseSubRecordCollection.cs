using Fallout.NET.TES4.Records;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fallout.NET.TES4.SubRecords.INFO
{
    public class INFO_ResponseSubRecordCollection
    {
        
        /// <summary>
        /// Response Data
        /// </summary>
        public INFO_TRDTSubrecord TRDT { get; set; }

        /// <summary>
        /// Reponse text
        /// </summary>
        public STRSubRecord NAM1 { get; set; }

        /// <summary>
        /// Script Notes
        /// </summary>
        public STRSubRecord NAM2 { get; set; }

        /// <summary>
        /// Edits
        /// </summary>
        public STRSubRecord NAM3 { get; set; }

        /// <summary>
        /// Speaker Animation
        /// </summary>
        /// <remarks>FormID of an <see cref="IDLERecord"/></remarks>
        public FormID SNAM { get; set; }

        /// <summary>
        /// Listener Animation
        /// </summary>
        /// <remarks>FormID of an <see cref="IDLERecord"/></remarks>
        public FormID LNAM { get; set; }
    }
}
