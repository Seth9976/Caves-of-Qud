using System;

namespace XRL.World.Conversations
{
	/// <summary>Marks a class for generation of conversation event partials.</summary>
	// Token: 0x02000BE4 RID: 3044
	[AttributeUsage(AttributeTargets.Class)]
	public class ConversationEventAttribute : Attribute
	{
		// Token: 0x040037CC RID: 14284
		public bool Base;

		// Token: 0x040037CD RID: 14285
		public ConversationEvent.Action Action;

		// Token: 0x040037CE RID: 14286
		public ConversationEvent.Instantiation Instantiation = ConversationEvent.Instantiation.Pooling;
	}
}
