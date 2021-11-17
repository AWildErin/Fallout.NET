using Fallout.NET.Core;

namespace Fallout.NET.TES4.Records
{
	/// <summary>
	/// Soul Gem
	/// </summary>
	/// todo: fill out record
	public class SLGMRecord : Record
	{
		protected override void ExtractSubRecords(BetterReader reader, GameID gameID, uint size)
		{
			 reader.ReadBytes((int)size);
		}
	}
}