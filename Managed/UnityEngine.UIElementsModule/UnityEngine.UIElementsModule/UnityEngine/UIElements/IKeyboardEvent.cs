using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200024D RID: 589
	public interface IKeyboardEvent
	{
		// Token: 0x17000313 RID: 787
		// (get) Token: 0x0600116B RID: 4459
		EventModifiers modifiers { get; }

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x0600116C RID: 4460
		char character { get; }

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x0600116D RID: 4461
		KeyCode keyCode { get; }

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x0600116E RID: 4462
		bool shiftKey { get; }

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x0600116F RID: 4463
		bool ctrlKey { get; }

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06001170 RID: 4464
		bool commandKey { get; }

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06001171 RID: 4465
		bool altKey { get; }

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06001172 RID: 4466
		bool actionKey { get; }
	}
}
