using System;

namespace XRL.World.AI
{
	// Token: 0x02000B63 RID: 2915
	public class AllySummon : IAllyReasonSourced
	{
		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x06007519 RID: 29977 RVA: 0x002BF230 File Offset: 0x002BD430
		public override IAllyReason.ReplaceTarget Replace
		{
			get
			{
				return IAllyReason.ReplaceTarget.Type;
			}
		}

		// Token: 0x0600751A RID: 29978 RVA: 0x002BF233 File Offset: 0x002BD433
		public override string GetText(GameObject Actor)
		{
			return "I was summoned by " + this.Name + ".";
		}
	}
}
