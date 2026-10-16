using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020002A7 RID: 679
	public interface ITransitionEvent
	{
		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06001386 RID: 4998
		StylePropertyNameCollection stylePropertyNames { get; }

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06001387 RID: 4999
		double elapsedTime { get; }
	}
}
