using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000475 RID: 1141
	public interface IStyleValue<T>
	{
		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x060023FD RID: 9213
		// (set) Token: 0x060023FE RID: 9214
		T value { get; set; }

		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x060023FF RID: 9215
		// (set) Token: 0x06002400 RID: 9216
		StyleKeyword keyword { get; set; }
	}
}
