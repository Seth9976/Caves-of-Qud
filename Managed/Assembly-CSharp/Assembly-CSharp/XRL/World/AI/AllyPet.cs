using System;

namespace XRL.World.AI
{
	// Token: 0x02000B5A RID: 2906
	public class AllyPet : IAllyReason
	{
		// Token: 0x06007505 RID: 29957 RVA: 0x002BF038 File Offset: 0x002BD238
		public override string GetText(GameObject Actor)
		{
			return string.Concat(new string[]
			{
				"I joined ",
				The.Player.t(int.MaxValue, null, null, false, false, false, false, true, true, true, false, null, false, false, false, null, true, null, false),
				" at the start of ",
				The.Player.its,
				" adventure."
			});
		}
	}
}
