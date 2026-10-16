using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020005BF RID: 1471
	[AttributeUsage(AttributeTargets.Class)]
	internal class EventCategoryAttribute : Attribute
	{
		// Token: 0x06002B8D RID: 11149 RVA: 0x0009D2DB File Offset: 0x0009B4DB
		public EventCategoryAttribute(EventCategory category)
		{
			this.category = category;
		}

		// Token: 0x040013D8 RID: 5080
		internal EventCategory category;
	}
}
