using System;
using UnityEngine.ParticleSystemJobs;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020000BB RID: 187
	public interface IAsyncOnParticleUpdateJobScheduledHandler
	{
		// Token: 0x060004EA RID: 1258
		UniTask<ParticleSystemJobData> OnParticleUpdateJobScheduledAsync();
	}
}
