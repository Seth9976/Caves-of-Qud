using System;

namespace XRL.World.AI
{
	// Token: 0x02000B62 RID: 2914
	public class AllyAscend : IAllyReason
	{
		// Token: 0x06007517 RID: 29975 RVA: 0x002BF1E4 File Offset: 0x002BD3E4
		public override string GetText(GameObject Actor)
		{
			return "I am to ascend the spindle with " + The.Player.an(int.MaxValue, null, null, false, false, false, false, true, false, true, true, false, false, false, null, false, null, false) + ".";
		}
	}
}
