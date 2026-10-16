using System;

namespace UnityEngine.Timeline
{
	// Token: 0x02000030 RID: 48
	internal static class SelectionExtensions
	{
		// Token: 0x06000263 RID: 611 RVA: 0x00008B9E File Offset: 0x00006D9E
		public static ObjectId GetObjectId(this Object obj)
		{
			return obj.GetInstanceID();
		}
	}
}
