using System;

namespace XRL.World.AI
{
	// Token: 0x02000B57 RID: 2903
	public class AllyBirth : IAllyReasonSourced
	{
		// Token: 0x060074FC RID: 29948 RVA: 0x002BEFC7 File Offset: 0x002BD1C7
		public override string GetText(GameObject Actor)
		{
			return "I was birthed by " + this.Name + ".";
		}
	}
}
