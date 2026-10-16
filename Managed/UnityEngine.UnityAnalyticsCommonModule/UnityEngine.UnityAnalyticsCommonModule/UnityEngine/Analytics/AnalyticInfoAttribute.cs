using System;
using UnityEngine.Internal;

namespace UnityEngine.Analytics
{
	// Token: 0x0200000E RID: 14
	[ExcludeFromDocs]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
	public class AnalyticInfoAttribute : Attribute
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002187 File Offset: 0x00000387
		public int version { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000017 RID: 23 RVA: 0x0000218F File Offset: 0x0000038F
		public string vendorKey { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002197 File Offset: 0x00000397
		public string eventName { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000019 RID: 25 RVA: 0x0000219F File Offset: 0x0000039F
		internal int maxEventsPerHour { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000021A7 File Offset: 0x000003A7
		internal int maxNumberOfElements { get; }

		// Token: 0x0600001B RID: 27 RVA: 0x000021AF File Offset: 0x000003AF
		public AnalyticInfoAttribute(string eventName, string vendorKey = "", int version = 1, int maxEventsPerHour = 1000, int maxNumberOfElements = 1000)
		{
			this.version = version;
			this.vendorKey = vendorKey;
			this.eventName = eventName;
			this.maxEventsPerHour = maxEventsPerHour;
			this.maxNumberOfElements = maxNumberOfElements;
		}
	}
}
