using System;
using XRL.Language;

namespace XRL.World.AI
{
	// Token: 0x02000B5D RID: 2909
	public class AllyRetinue : IAllyReasonSourced
	{
		// Token: 0x0600750B RID: 29963 RVA: 0x002BF0F1 File Offset: 0x002BD2F1
		public override string GetText(GameObject Actor)
		{
			return "I am part of " + Grammar.MakePossessive(this.Name) + " retinue.";
		}
	}
}
