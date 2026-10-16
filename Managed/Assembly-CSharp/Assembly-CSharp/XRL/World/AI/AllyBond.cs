using System;

namespace XRL.World.AI
{
	// Token: 0x02000B5F RID: 2911
	public class AllyBond : IAllyReasonSourced
	{
		// Token: 0x0600750F RID: 29967 RVA: 0x002BF134 File Offset: 0x002BD334
		public override string GetText(GameObject Actor)
		{
			return "I share a bond with " + this.Name + ".";
		}
	}
}
