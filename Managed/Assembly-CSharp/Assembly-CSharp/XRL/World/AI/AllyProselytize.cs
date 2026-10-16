using System;

namespace XRL.World.AI
{
	// Token: 0x02000B58 RID: 2904
	public class AllyProselytize : IAllyReasonSourced
	{
		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x060074FE RID: 29950 RVA: 0x002BEFE6 File Offset: 0x002BD1E6
		public override IAllyReason.ReplaceTarget Replace
		{
			get
			{
				return IAllyReason.ReplaceTarget.Type;
			}
		}

		// Token: 0x060074FF RID: 29951 RVA: 0x002BEFE9 File Offset: 0x002BD1E9
		public override string GetText(GameObject Actor)
		{
			return "I was convinced by the pleas of " + this.Name + ".";
		}
	}
}
