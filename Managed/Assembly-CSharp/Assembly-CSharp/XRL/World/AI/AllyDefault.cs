using System;
using XRL.Language;

namespace XRL.World.AI
{
	// Token: 0x02000B54 RID: 2900
	public class AllyDefault : IAllyReasonSourced
	{
		// Token: 0x060074F6 RID: 29942 RVA: 0x002BEF65 File Offset: 0x002BD165
		public override string GetText(GameObject Actor)
		{
			return "I was compelled to join " + Grammar.MakePossessive(this.Name) + " cause.";
		}
	}
}
