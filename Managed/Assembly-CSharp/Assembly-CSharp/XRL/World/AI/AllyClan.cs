using System;
using XRL.Language;

namespace XRL.World.AI
{
	// Token: 0x02000B5C RID: 2908
	public class AllyClan : IAllyReasonSourced
	{
		// Token: 0x06007509 RID: 29961 RVA: 0x002BF0CD File Offset: 0x002BD2CD
		public override string GetText(GameObject Actor)
		{
			return "I am part of " + Grammar.MakePossessive(this.Name) + " clan.";
		}
	}
}
