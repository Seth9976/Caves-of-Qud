using System;
using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	// Token: 0x0200044F RID: 1103
	[RequiredByNativeCode]
	[Serializable]
	public abstract class PlayableBehaviour : IPlayableBehaviour, ICloneable
	{
		// Token: 0x06002A51 RID: 10833 RVA: 0x00030B93 File Offset: 0x0002ED93
		public PlayableBehaviour()
		{
		}

		// Token: 0x06002A52 RID: 10834 RVA: 0x00002925 File Offset: 0x00000B25
		public virtual void OnGraphStart(Playable playable)
		{
		}

		// Token: 0x06002A53 RID: 10835 RVA: 0x00002925 File Offset: 0x00000B25
		public virtual void OnGraphStop(Playable playable)
		{
		}

		// Token: 0x06002A54 RID: 10836 RVA: 0x00002925 File Offset: 0x00000B25
		public virtual void OnPlayableCreate(Playable playable)
		{
		}

		// Token: 0x06002A55 RID: 10837 RVA: 0x00002925 File Offset: 0x00000B25
		public virtual void OnPlayableDestroy(Playable playable)
		{
		}

		// Token: 0x06002A56 RID: 10838 RVA: 0x00002925 File Offset: 0x00000B25
		[Obsolete("OnBehaviourDelay is obsolete; use a custom ScriptPlayable to implement this feature", false)]
		public virtual void OnBehaviourDelay(Playable playable, FrameData info)
		{
		}

		// Token: 0x06002A57 RID: 10839 RVA: 0x00002925 File Offset: 0x00000B25
		public virtual void OnBehaviourPlay(Playable playable, FrameData info)
		{
		}

		// Token: 0x06002A58 RID: 10840 RVA: 0x00002925 File Offset: 0x00000B25
		public virtual void OnBehaviourPause(Playable playable, FrameData info)
		{
		}

		// Token: 0x06002A59 RID: 10841 RVA: 0x00002925 File Offset: 0x00000B25
		public virtual void PrepareData(Playable playable, FrameData info)
		{
		}

		// Token: 0x06002A5A RID: 10842 RVA: 0x00002925 File Offset: 0x00000B25
		public virtual void PrepareFrame(Playable playable, FrameData info)
		{
		}

		// Token: 0x06002A5B RID: 10843 RVA: 0x00002925 File Offset: 0x00000B25
		public virtual void ProcessFrame(Playable playable, FrameData info, object playerData)
		{
		}

		// Token: 0x06002A5C RID: 10844 RVA: 0x00051698 File Offset: 0x0004F898
		public virtual object Clone()
		{
			return base.MemberwiseClone();
		}
	}
}
