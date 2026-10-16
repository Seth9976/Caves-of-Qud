using System;

namespace XRL.World.Conversations
{
	/// <remarks>Used to decouple parameters from delegate signature for mod compatibility.</remarks>
	// Token: 0x02000BE2 RID: 3042
	public class DelegateContext
	{
		// Token: 0x060079F3 RID: 31219 RVA: 0x002DD6CE File Offset: 0x002DB8CE
		internal static DelegateContext Set(IConversationElement Element, string Value, GameObject Target)
		{
			DelegateContext.Instance.Element = Element;
			DelegateContext.Instance.Value = Value;
			DelegateContext.Instance.Target = Target;
			return DelegateContext.Instance;
		}

		// Token: 0x040037C4 RID: 14276
		public IConversationElement Element;

		// Token: 0x040037C5 RID: 14277
		public GameObject Target;

		// Token: 0x040037C6 RID: 14278
		public string Value;

		// Token: 0x040037C7 RID: 14279
		public object[] Separated;

		// Token: 0x040037C8 RID: 14280
		public static DelegateContext Instance = new DelegateContext();
	}
}
