using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020001F1 RID: 497
	internal interface IDragAndDropData
	{
		// Token: 0x06000F59 RID: 3929
		object GetGenericData(string key);

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000F5A RID: 3930
		object userData { get; }

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000F5B RID: 3931
		IEnumerable<Object> unityObjectReferences { get; }

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000F5C RID: 3932
		// (set) Token: 0x06000F5D RID: 3933
		string[] paths { get; set; }
	}
}
