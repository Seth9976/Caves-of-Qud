using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020000A8 RID: 168
	public interface IBindable
	{
		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060006C7 RID: 1735
		// (set) Token: 0x060006C8 RID: 1736
		IBinding binding { get; set; }

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060006C9 RID: 1737
		// (set) Token: 0x060006CA RID: 1738
		string bindingPath { get; set; }
	}
}
