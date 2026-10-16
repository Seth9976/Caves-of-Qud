using System;
using UnityEngine.Playables;

namespace UnityEngine
{
	// Token: 0x0200000D RID: 13
	internal struct ActivePlayable
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002078 File Offset: 0x00000278
		public readonly PlayableSettings settings { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002080 File Offset: 0x00000280
		public readonly PlayableHandle clipPlayableHandle { get; }
	}
}
