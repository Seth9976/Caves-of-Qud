using System;
using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	// Token: 0x02000448 RID: 1096
	public interface IPlayableBehaviour
	{
		// Token: 0x06002A35 RID: 10805
		[RequiredByNativeCode]
		void OnGraphStart(Playable playable);

		// Token: 0x06002A36 RID: 10806
		[RequiredByNativeCode]
		void OnGraphStop(Playable playable);

		// Token: 0x06002A37 RID: 10807
		[RequiredByNativeCode]
		void OnPlayableCreate(Playable playable);

		// Token: 0x06002A38 RID: 10808
		[RequiredByNativeCode]
		void OnPlayableDestroy(Playable playable);

		// Token: 0x06002A39 RID: 10809
		[RequiredByNativeCode]
		void OnBehaviourPlay(Playable playable, FrameData info);

		// Token: 0x06002A3A RID: 10810
		[RequiredByNativeCode]
		void OnBehaviourPause(Playable playable, FrameData info);

		// Token: 0x06002A3B RID: 10811
		[RequiredByNativeCode]
		void PrepareFrame(Playable playable, FrameData info);

		// Token: 0x06002A3C RID: 10812
		[RequiredByNativeCode]
		void ProcessFrame(Playable playable, FrameData info, object playerData);
	}
}
