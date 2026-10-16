using System;

namespace XRL.World.AI
{
	// Token: 0x02000B55 RID: 2901
	public class AllyConstructed : IAllyReasonSourced
	{
		// Token: 0x060074F8 RID: 29944 RVA: 0x002BEF89 File Offset: 0x002BD189
		public override string GetText(GameObject Actor)
		{
			return "I was constructed by " + this.Name + ".";
		}
	}
}
