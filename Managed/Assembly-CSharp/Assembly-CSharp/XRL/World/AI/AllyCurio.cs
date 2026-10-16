using System;

namespace XRL.World.AI
{
	// Token: 0x02000B5E RID: 2910
	public class AllyCurio : IAllyReasonSourced
	{
		// Token: 0x0600750D RID: 29965 RVA: 0x002BF115 File Offset: 0x002BD315
		public override string GetText(GameObject Actor)
		{
			return "I like " + this.Name + ".";
		}
	}
}
