using System;

namespace UnityEngine
{
	// Token: 0x0200000B RID: 11
	public interface ISubsystemDescriptor
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000024 RID: 36
		string id { get; }

		// Token: 0x06000025 RID: 37
		ISubsystem Create();
	}
}
