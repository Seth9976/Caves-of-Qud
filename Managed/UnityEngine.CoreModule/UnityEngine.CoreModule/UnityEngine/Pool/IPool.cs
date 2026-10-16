using System;

namespace UnityEngine.Pool
{
	// Token: 0x02000425 RID: 1061
	internal interface IPool
	{
		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06002998 RID: 10648
		int CountInactive { get; }

		// Token: 0x06002999 RID: 10649
		void Clear();
	}
}
