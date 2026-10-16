using System;

namespace UnityEngine
{
	// Token: 0x0200000A RID: 10
	public interface ISubsystem
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000020 RID: 32
		bool running { get; }

		// Token: 0x06000021 RID: 33
		void Start();

		// Token: 0x06000022 RID: 34
		void Stop();

		// Token: 0x06000023 RID: 35
		void Destroy();
	}
}
