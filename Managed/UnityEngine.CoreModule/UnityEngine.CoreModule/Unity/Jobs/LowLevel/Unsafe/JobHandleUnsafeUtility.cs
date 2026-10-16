using System;

namespace Unity.Jobs.LowLevel.Unsafe
{
	// Token: 0x02000053 RID: 83
	public static class JobHandleUnsafeUtility
	{
		// Token: 0x06000112 RID: 274 RVA: 0x00003180 File Offset: 0x00001380
		public unsafe static JobHandle CombineDependencies(JobHandle* jobs, int count)
		{
			return JobHandle.CombineDependenciesInternalPtr((void*)jobs, count);
		}
	}
}
