using System;
using System.Collections.Generic;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x02000037 RID: 55
	[Obsolete("For best performance use PlayableAsset and PlayableBehaviour.")]
	[Serializable]
	public class BasicPlayableBehaviour : ScriptableObject, IPlayableAsset, IPlayableBehaviour
	{
		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000287 RID: 647 RVA: 0x00008FCF File Offset: 0x000071CF
		public virtual double duration
		{
			get
			{
				return PlayableBinding.DefaultDuration;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000288 RID: 648 RVA: 0x00008FD6 File Offset: 0x000071D6
		public virtual IEnumerable<PlayableBinding> outputs
		{
			get
			{
				return PlayableBinding.None;
			}
		}

		// Token: 0x06000289 RID: 649 RVA: 0x00008FDD File Offset: 0x000071DD
		public virtual void OnGraphStart(Playable playable)
		{
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00008FDF File Offset: 0x000071DF
		public virtual void OnGraphStop(Playable playable)
		{
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00008FE1 File Offset: 0x000071E1
		public virtual void OnPlayableCreate(Playable playable)
		{
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00008FE3 File Offset: 0x000071E3
		public virtual void OnPlayableDestroy(Playable playable)
		{
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00008FE5 File Offset: 0x000071E5
		public virtual void OnBehaviourPlay(Playable playable, FrameData info)
		{
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00008FE7 File Offset: 0x000071E7
		public virtual void OnBehaviourPause(Playable playable, FrameData info)
		{
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00008FE9 File Offset: 0x000071E9
		public virtual void PrepareFrame(Playable playable, FrameData info)
		{
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00008FEB File Offset: 0x000071EB
		public virtual void ProcessFrame(Playable playable, FrameData info, object playerData)
		{
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00008FED File Offset: 0x000071ED
		public virtual Playable CreatePlayable(PlayableGraph graph, GameObject owner)
		{
			return ScriptPlayable<BasicPlayableBehaviour>.Create(graph, this, 0);
		}
	}
}
