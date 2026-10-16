using System;

namespace XRL.World.Conversations
{
	// Token: 0x02000BEB RID: 3051
	public class ConversationText : IConversationElement
	{
		// Token: 0x170009B9 RID: 2489
		// (get) Token: 0x06007AB0 RID: 31408 RVA: 0x002E15EB File Offset: 0x002DF7EB
		public override int Propagation
		{
			get
			{
				IConversationElement parent = this.Parent;
				if (parent == null)
				{
					return 0;
				}
				return parent.Propagation;
			}
		}
	}
}
