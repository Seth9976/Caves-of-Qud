using System;

namespace XRL.World.AI
{
	// Token: 0x02000B56 RID: 2902
	public class AllyClone : IAllyReasonSourced
	{
		// Token: 0x060074FA RID: 29946 RVA: 0x002BEFA8 File Offset: 0x002BD1A8
		public override string GetText(GameObject Actor)
		{
			return "I am become " + this.Name + ".";
		}
	}
}
