using System;
using System.Runtime.CompilerServices;
using UnityEngine.Purchasing.Interfaces;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000014 RID: 20
	[global::System.Runtime.CompilerServices.NullableContext(2)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class GoogleLastKnownProductService : IGoogleLastKnownProductService
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002778 File Offset: 0x00000978
		// (set) Token: 0x06000034 RID: 52 RVA: 0x00002780 File Offset: 0x00000980
		public string LastKnownOldProductId { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002789 File Offset: 0x00000989
		// (set) Token: 0x06000036 RID: 54 RVA: 0x00002791 File Offset: 0x00000991
		public string LastKnownProductId { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000037 RID: 55 RVA: 0x0000279A File Offset: 0x0000099A
		// (set) Token: 0x06000038 RID: 56 RVA: 0x000027A2 File Offset: 0x000009A2
		public GooglePlayReplacementMode? LastKnownReplacementMode { get; set; } = new GooglePlayReplacementMode?(GooglePlayReplacementMode.UnknownReplacementMode);
	}
}
