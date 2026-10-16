using System;

namespace UnityEngine.SearchService
{
	// Token: 0x0200035F RID: 863
	[Obsolete("ObjectSelectorHandlerWithTagsAttribute has been deprecated. Use SearchContextAttribute instead.", true)]
	[AttributeUsage(AttributeTargets.Field)]
	public class ObjectSelectorHandlerWithTagsAttribute : Attribute
	{
		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x060028A9 RID: 10409 RVA: 0x0004E863 File Offset: 0x0004CA63
		public string[] tags { get; }

		// Token: 0x060028AA RID: 10410 RVA: 0x0004E86B File Offset: 0x0004CA6B
		public ObjectSelectorHandlerWithTagsAttribute(params string[] tags)
		{
			this.tags = tags;
		}
	}
}
