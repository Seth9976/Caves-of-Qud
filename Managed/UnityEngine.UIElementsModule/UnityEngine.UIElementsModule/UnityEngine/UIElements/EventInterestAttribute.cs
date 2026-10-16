using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020005BE RID: 1470
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public class EventInterestAttribute : Attribute
	{
		// Token: 0x06002B8A RID: 11146 RVA: 0x0009D2AB File Offset: 0x0009B4AB
		public EventInterestAttribute(params Type[] eventTypes)
		{
			this.eventTypes = eventTypes;
		}

		// Token: 0x06002B8B RID: 11147 RVA: 0x0009D2C3 File Offset: 0x0009B4C3
		public EventInterestAttribute(EventInterestOptions interests)
		{
			this.categoryFlags = (EventCategoryFlags)interests;
		}

		// Token: 0x06002B8C RID: 11148 RVA: 0x0009D2C3 File Offset: 0x0009B4C3
		internal EventInterestAttribute(EventInterestOptionsInternal interests)
		{
			this.categoryFlags = (EventCategoryFlags)interests;
		}

		// Token: 0x040013D6 RID: 5078
		internal Type[] eventTypes;

		// Token: 0x040013D7 RID: 5079
		internal EventCategoryFlags categoryFlags = EventCategoryFlags.None;
	}
}
