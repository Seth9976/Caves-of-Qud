using System;
using UnityEngine.Playables;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Animations
{
	// Token: 0x02000049 RID: 73
	[MovedFrom("UnityEngine.Experimental.Animations")]
	public interface IAnimationJobPlayable : IPlayable
	{
		// Token: 0x060003B3 RID: 947
		T GetJobData<T>() where T : struct, IAnimationJob;

		// Token: 0x060003B4 RID: 948
		void SetJobData<T>(T jobData) where T : struct, IAnimationJob;
	}
}
