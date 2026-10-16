using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Purchasing.Interfaces
{
	// Token: 0x0200009D RID: 157
	[global::System.Runtime.CompilerServices.NullableContext(2)]
	internal interface IGoogleLastKnownProductService
	{
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600038B RID: 907
		// (set) Token: 0x0600038C RID: 908
		string LastKnownOldProductId { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600038D RID: 909
		// (set) Token: 0x0600038E RID: 910
		string LastKnownProductId { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600038F RID: 911
		// (set) Token: 0x06000390 RID: 912
		GooglePlayReplacementMode? LastKnownReplacementMode { get; set; }
	}
}
