using System;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs.LowLevel.Unsafe;

namespace UnityEngine.Animations
{
	// Token: 0x0200004E RID: 78
	internal struct ProcessAnimationJobStruct<T> where T : struct, IAnimationJob
	{
		// Token: 0x060003BB RID: 955 RVA: 0x00007F4C File Offset: 0x0000614C
		public static IntPtr GetJobReflectionData()
		{
			bool flag = ProcessAnimationJobStruct<T>.jobReflectionData == IntPtr.Zero;
			if (flag)
			{
				ProcessAnimationJobStruct<T>.jobReflectionData = JobsUtility.CreateJobReflectionData(typeof(T), new ProcessAnimationJobStruct<T>.ExecuteJobFunction(ProcessAnimationJobStruct<T>.Execute), null, null);
			}
			return ProcessAnimationJobStruct<T>.jobReflectionData;
		}

		// Token: 0x060003BC RID: 956 RVA: 0x00007F9C File Offset: 0x0000619C
		public unsafe static void Execute(ref T data, IntPtr animationStreamPtr, IntPtr methodIndex, ref JobRanges ranges, int jobIndex)
		{
			AnimationStream animationStream;
			UnsafeUtility.CopyPtrToStructure<AnimationStream>((void*)animationStreamPtr, out animationStream);
			JobMethodIndex jobMethodIndex = (JobMethodIndex)methodIndex.ToInt32();
			JobMethodIndex jobMethodIndex2 = jobMethodIndex;
			JobMethodIndex jobMethodIndex3 = jobMethodIndex2;
			if (jobMethodIndex3 != JobMethodIndex.ProcessRootMotionMethodIndex)
			{
				if (jobMethodIndex3 != JobMethodIndex.ProcessAnimationMethodIndex)
				{
					throw new NotImplementedException("Invalid Animation jobs method index.");
				}
				data.ProcessAnimation(animationStream);
			}
			else
			{
				data.ProcessRootMotion(animationStream);
			}
		}

		// Token: 0x04000150 RID: 336
		private static IntPtr jobReflectionData;

		// Token: 0x0200004F RID: 79
		// (Invoke) Token: 0x060003BE RID: 958
		public delegate void ExecuteJobFunction(ref T data, IntPtr animationStreamPtr, IntPtr unusedPtr, ref JobRanges ranges, int jobIndex);
	}
}
