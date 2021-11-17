using Fallout.NET.Core;

namespace Fallout.NET.TES4.Records
{
	/// <summary>
	/// Alchem. Apparatus
	/// </summary>
	/// todo: fill out record
	/// todo: Is this even in Fallout?
	public class APPARecord : Record
	{
		protected override void ExtractSubRecords(BetterReader reader, GameID gameID, uint size)
		{
			 reader.ReadBytes((int)size);
		}
	}
}