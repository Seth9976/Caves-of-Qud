using System;
using Unity.IntegerTime;

namespace UnityEngine.InputForUI
{
	// Token: 0x02000010 RID: 16
	internal interface IEventProperties
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000035 RID: 53
		DiscreteTime timestamp { get; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000036 RID: 54
		EventSource eventSource { get; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000037 RID: 55
		uint playerId { get; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000038 RID: 56
		EventModifiers eventModifiers { get; }
	}
}
