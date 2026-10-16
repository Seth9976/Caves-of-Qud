using System;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Animations
{
	// Token: 0x02000048 RID: 72
	[JobProducerType(typeof(ProcessAnimationJobStruct<>))]
	[MovedFrom("UnityEngine.Experimental.Animations")]
	public interface IAnimationJob
	{
		// Token: 0x060003B1 RID: 945
		void ProcessAnimation(AnimationStream stream);

		// Token: 0x060003B2 RID: 946
		void ProcessRootMotion(AnimationStream stream);
	}
}
