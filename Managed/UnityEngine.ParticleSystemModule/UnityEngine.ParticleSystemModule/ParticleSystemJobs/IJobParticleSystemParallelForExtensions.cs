using System;

namespace UnityEngine.ParticleSystemJobs
{
	// Token: 0x02000065 RID: 101
	public static class IJobParticleSystemParallelForExtensions
	{
		// Token: 0x060006C8 RID: 1736 RVA: 0x00008845 File Offset: 0x00006A45
		public static void EarlyJobInit<T>() where T : struct, IJobParticleSystemParallelFor
		{
			ParticleSystemParallelForJobStruct<T>.Initialize();
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x00008850 File Offset: 0x00006A50
		internal unsafe static IntPtr GetReflectionData<T>() where T : struct, IJobParticleSystemParallelFor
		{
			ParticleSystemParallelForJobStruct<T>.Initialize();
			return *ParticleSystemParallelForJobStruct<T>.jobReflectionData.Data;
		}
	}
}
