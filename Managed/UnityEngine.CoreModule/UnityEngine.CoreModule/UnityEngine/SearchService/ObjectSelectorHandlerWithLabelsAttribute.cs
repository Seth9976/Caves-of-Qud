using System;

namespace UnityEngine.SearchService
{
	// Token: 0x0200035E RID: 862
	[Obsolete("ObjectSelectorHandlerWithLabelsAttribute has been deprecated. Use SearchContextAttribute instead.", true)]
	[AttributeUsage(AttributeTargets.Field)]
	public class ObjectSelectorHandlerWithLabelsAttribute : Attribute
	{
		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x060028A5 RID: 10405 RVA: 0x0004E823 File Offset: 0x0004CA23
		public string[] labels { get; }

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x060028A6 RID: 10406 RVA: 0x0004E82B File Offset: 0x0004CA2B
		public bool matchAll { get; }

		// Token: 0x060028A7 RID: 10407 RVA: 0x0004E833 File Offset: 0x0004CA33
		public ObjectSelectorHandlerWithLabelsAttribute(params string[] labels)
		{
			this.labels = labels;
			this.matchAll = true;
		}

		// Token: 0x060028A8 RID: 10408 RVA: 0x0004E84B File Offset: 0x0004CA4B
		public ObjectSelectorHandlerWithLabelsAttribute(bool matchAll, params string[] labels)
		{
			this.labels = labels;
			this.matchAll = matchAll;
		}
	}
}
