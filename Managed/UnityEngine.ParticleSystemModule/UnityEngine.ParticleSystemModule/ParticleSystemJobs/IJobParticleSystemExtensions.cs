using System;

namespace UnityEngine.ParticleSystemJobs
{
	// Token: 0x02000064 RID: 100
	public static class IJobParticleSystemExtensions
	{
		// Token: 0x060006C6 RID: 1734 RVA: 0x00008815 File Offset: 0x00006A15
		public static void EarlyJobInit<T>() where T : struct, IJobParticleSystem
		{
			ParticleSystemJobStruct<T>.Initialize();
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x00008820 File Offset: 0x00006A20
		internal unsafe static IntPtr GetReflectionData<T>() where T : struct, IJobParticleSystem
		{
			ParticleSystemJobStruct<T>.Initialize();
			return *ParticleSystemJobStruct<T>.jobReflectionData.Data;
		}
	}
}
