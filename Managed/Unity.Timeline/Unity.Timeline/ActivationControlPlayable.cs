using System;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x02000036 RID: 54
	public class ActivationControlPlayable : PlayableBehaviour
	{
		// Token: 0x06000280 RID: 640 RVA: 0x00008E8C File Offset: 0x0000708C
		public static ScriptPlayable<ActivationControlPlayable> Create(PlayableGraph graph, GameObject gameObject, ActivationControlPlayable.PostPlaybackState postPlaybackState)
		{
			if (gameObject == null)
			{
				return ScriptPlayable<ActivationControlPlayable>.Null;
			}
			ScriptPlayable<ActivationControlPlayable> scriptPlayable = ScriptPlayable<ActivationControlPlayable>.Create(graph, 0);
			ActivationControlPlayable behaviour = scriptPlayable.GetBehaviour();
			behaviour.gameObject = gameObject;
			behaviour.postPlayback = postPlaybackState;
			return scriptPlayable;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00008EC5 File Offset: 0x000070C5
		public override void OnBehaviourPlay(Playable playable, FrameData info)
		{
			if (this.gameObject == null)
			{
				return;
			}
			this.gameObject.SetActive(true);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00008EE2 File Offset: 0x000070E2
		public override void OnBehaviourPause(Playable playable, FrameData info)
		{
			if (this.gameObject != null && info.effectivePlayState == PlayState.Paused)
			{
				this.gameObject.SetActive(false);
			}
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00008F07 File Offset: 0x00007107
		public override void ProcessFrame(Playable playable, FrameData info, object userData)
		{
			if (this.gameObject != null)
			{
				this.gameObject.SetActive(true);
			}
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00008F23 File Offset: 0x00007123
		public override void OnGraphStart(Playable playable)
		{
			if (this.gameObject != null && this.m_InitialState == ActivationControlPlayable.InitialState.Unset)
			{
				this.m_InitialState = (this.gameObject.activeSelf ? ActivationControlPlayable.InitialState.Active : ActivationControlPlayable.InitialState.Inactive);
			}
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00008F54 File Offset: 0x00007154
		public override void OnPlayableDestroy(Playable playable)
		{
			if (this.gameObject == null || this.m_InitialState == ActivationControlPlayable.InitialState.Unset)
			{
				return;
			}
			switch (this.postPlayback)
			{
			case ActivationControlPlayable.PostPlaybackState.Active:
				this.gameObject.SetActive(true);
				return;
			case ActivationControlPlayable.PostPlaybackState.Inactive:
				this.gameObject.SetActive(false);
				return;
			case ActivationControlPlayable.PostPlaybackState.Revert:
				this.gameObject.SetActive(this.m_InitialState == ActivationControlPlayable.InitialState.Active);
				return;
			default:
				return;
			}
		}

		// Token: 0x040000D8 RID: 216
		public GameObject gameObject;

		// Token: 0x040000D9 RID: 217
		public ActivationControlPlayable.PostPlaybackState postPlayback = ActivationControlPlayable.PostPlaybackState.Revert;

		// Token: 0x040000DA RID: 218
		private ActivationControlPlayable.InitialState m_InitialState;

		// Token: 0x02000079 RID: 121
		public enum PostPlaybackState
		{
			// Token: 0x04000181 RID: 385
			Active,
			// Token: 0x04000182 RID: 386
			Inactive,
			// Token: 0x04000183 RID: 387
			Revert
		}

		// Token: 0x0200007A RID: 122
		private enum InitialState
		{
			// Token: 0x04000185 RID: 389
			Unset,
			// Token: 0x04000186 RID: 390
			Active,
			// Token: 0x04000187 RID: 391
			Inactive
		}
	}
}
