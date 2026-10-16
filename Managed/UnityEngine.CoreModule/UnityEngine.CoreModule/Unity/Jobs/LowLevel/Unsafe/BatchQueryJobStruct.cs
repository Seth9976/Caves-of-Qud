using System;

namespace Unity.Jobs.LowLevel.Unsafe
{
	// Token: 0x02000052 RID: 82
	public struct BatchQueryJobStruct<T> where T : struct
	{
		// Token: 0x06000111 RID: 273 RVA: 0x0000313C File Offset: 0x0000133C
		public static IntPtr Initialize()
		{
			bool flag = BatchQueryJobStruct<T>.jobReflectionData == IntPtr.Zero;
			if (flag)
			{
				BatchQueryJobStruct<T>.jobReflectionData = JobsUtility.CreateJobReflectionData(typeof(T), null, null, null);
			}
			return BatchQueryJobStruct<T>.jobReflectionData;
		}

		// Token: 0x0400010A RID: 266
		internal static IntPtr jobReflectionData;
	}
}
