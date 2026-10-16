using System;

namespace XRL.World.Conversations
{
	// Token: 0x02000BEC RID: 3052
	public class Node : IConversationElement
	{
		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x06007AB2 RID: 31410 RVA: 0x002E1606 File Offset: 0x002DF806
		public override int Propagation
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x040037F0 RID: 14320
		public bool AllowEscape = true;
	}
}
