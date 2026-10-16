using System;
using XRL.Language;

namespace XRL.World.AI
{
	// Token: 0x02000B5B RID: 2907
	public class AllyPack : IAllyReasonSourced
	{
		// Token: 0x06007507 RID: 29959 RVA: 0x002BF0A9 File Offset: 0x002BD2A9
		public override string GetText(GameObject Actor)
		{
			return "I am part of " + Grammar.MakePossessive(this.Name) + " pack.";
		}
	}
}
