using System;

namespace XRL.World.AI
{
	// Token: 0x02000B60 RID: 2912
	public class AllyRebuke : IAllyReasonSourced
	{
		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x06007511 RID: 29969 RVA: 0x002BF153 File Offset: 0x002BD353
		public override IAllyReason.ReplaceTarget Replace
		{
			get
			{
				return IAllyReason.ReplaceTarget.Type;
			}
		}

		// Token: 0x06007512 RID: 29970 RVA: 0x002BF156 File Offset: 0x002BD356
		public override string GetText(GameObject Actor)
		{
			return "I was rebuked into submission by " + this.Name + ".";
		}
	}
}
