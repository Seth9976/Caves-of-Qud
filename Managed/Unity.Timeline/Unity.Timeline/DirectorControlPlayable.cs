using System;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x02000038 RID: 56
	public class DirectorControlPlayable : PlayableBehaviour
	{
		// Token: 0x06000293 RID: 659 RVA: 0x00009004 File Offset: 0x00007204
		public static ScriptPlayable<DirectorControlPlayable> Create(PlayableGraph graph, PlayableDirector director)
		{
			if (director == null)
			{
				return ScriptPlayable<DirectorControlPlayable>.Null;
			}
			ScriptPlayable<DirectorControlPlayable> scriptPlayable = ScriptPlayable<DirectorControlPlayable>.Create(graph, 0);
			scriptPlayable.GetBehaviour().director = director;
			return scriptPlayable;
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00009036 File Offset: 0x00007236
		public override void OnPlayableDestroy(Playable playable)
		{
			if (this.director != null && this.director.playableAsset != null)
			{
				this.director.Stop();
			}
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00009064 File Offset: 0x00007264
		public override void PrepareFrame(Playable playable, FrameData info)
		{
			if (this.director == null || !this.director.isActiveAndEnabled || this.director.playableAsset == null)
			{
				return;
			}
			this.m_SyncTime |= info.evaluationType == FrameData.EvaluationType.Evaluate || this.DetectDiscontinuity(playable, info);
			this.SyncSpeed((double)info.effectiveSpeed);
			this.SyncStart(playable.GetGraph<Playable>(), playable.GetTime<Playable>());
		}

		// Token: 0x06000296 RID: 662 RVA: 0x000090E1 File Offset: 0x000072E1
		public override void OnBehaviourPlay(Playable playable, FrameData info)
		{
			this.m_SyncTime = true;
			if (this.director != null && this.director.playableAsset != null)
			{
				this.m_AssetDuration = this.director.playableAsset.duration;
			}
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00009124 File Offset: 0x00007324
		public override void OnBehaviourPause(Playable playable, FrameData info)
		{
			if (this.director != null && this.director.playableAsset != null)
			{
				if (info.effectivePlayState == PlayState.Playing || (info.effectivePlayState == PlayState.Paused && this.pauseAction == DirectorControlPlayable.PauseAction.PauseDirector))
				{
					this.director.Pause();
					return;
				}
				this.director.Stop();
			}
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00009188 File Offset: 0x00007388
		public override void ProcessFrame(Playable playable, FrameData info, object playerData)
		{
			if (this.director == null || !this.director.isActiveAndEnabled || this.director.playableAsset == null)
			{
				return;
			}
			if (this.m_SyncTime || this.DetectOutOfSync(playable))
			{
				this.UpdateTime(playable);
				if (this.director.playableGraph.IsValid())
				{
					this.director.playableGraph.Evaluate();
					this.director.playableGraph.SynchronizeEvaluation(playable.GetGraph<Playable>());
				}
				else
				{
					this.director.Evaluate();
				}
			}
			this.m_SyncTime = false;
			this.SyncStop(playable.GetGraph<Playable>(), playable.GetTime<Playable>());
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00009244 File Offset: 0x00007444
		private void SyncSpeed(double speed)
		{
			if (this.director.playableGraph.IsValid())
			{
				int rootPlayableCount = this.director.playableGraph.GetRootPlayableCount();
				for (int i = 0; i < rootPlayableCount; i++)
				{
					Playable rootPlayable = this.director.playableGraph.GetRootPlayable(i);
					if (rootPlayable.IsValid<Playable>())
					{
						rootPlayable.SetSpeed(speed);
					}
				}
			}
		}

		// Token: 0x0600029A RID: 666 RVA: 0x000092AC File Offset: 0x000074AC
		private void SyncStart(PlayableGraph graph, double time)
		{
			if (this.director.state == PlayState.Playing || !graph.IsPlaying() || (this.director.extrapolationMode == DirectorWrapMode.None && time > this.m_AssetDuration))
			{
				return;
			}
			if (graph.IsMatchFrameRateEnabled())
			{
				this.director.Play(graph.GetFrameRate());
				return;
			}
			this.director.Play();
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00009310 File Offset: 0x00007510
		private void SyncStop(PlayableGraph graph, double time)
		{
			if (this.director.state == PlayState.Paused || (graph.IsPlaying() && (this.director.extrapolationMode != DirectorWrapMode.None || time < this.m_AssetDuration)))
			{
				return;
			}
			if (this.director.state == PlayState.Paused)
			{
				return;
			}
			if ((this.director.extrapolationMode == DirectorWrapMode.None && time > this.m_AssetDuration) || !graph.IsPlaying())
			{
				this.director.Pause();
			}
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00009389 File Offset: 0x00007589
		private bool DetectDiscontinuity(Playable playable, FrameData info)
		{
			return Math.Abs(playable.GetTime<Playable>() - playable.GetPreviousTime<Playable>() - info.m_DeltaTime * (double)info.m_EffectiveSpeed) > DiscreteTime.tickValue;
		}

		// Token: 0x0600029D RID: 669 RVA: 0x000093B4 File Offset: 0x000075B4
		private bool DetectOutOfSync(Playable playable)
		{
			double num = playable.GetTime<Playable>();
			if (playable.GetTime<Playable>() >= this.m_AssetDuration)
			{
				switch (this.director.extrapolationMode)
				{
				case DirectorWrapMode.Hold:
					num = this.m_AssetDuration;
					break;
				case DirectorWrapMode.Loop:
					num %= this.m_AssetDuration;
					break;
				case DirectorWrapMode.None:
					num = this.m_AssetDuration;
					break;
				}
			}
			return !Mathf.Approximately((float)num, (float)this.director.time);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000942C File Offset: 0x0000762C
		private void UpdateTime(Playable playable)
		{
			double num = Math.Max(0.1, this.director.playableAsset.duration);
			switch (this.director.extrapolationMode)
			{
			case DirectorWrapMode.Hold:
				this.director.time = Math.Min(num, Math.Max(0.0, playable.GetTime<Playable>()));
				return;
			case DirectorWrapMode.Loop:
				this.director.time = Math.Max(0.0, playable.GetTime<Playable>() % num);
				return;
			case DirectorWrapMode.None:
				this.director.time = Math.Min(num, Math.Max(0.0, playable.GetTime<Playable>()));
				return;
			default:
				return;
			}
		}

		// Token: 0x040000DB RID: 219
		public PlayableDirector director;

		// Token: 0x040000DC RID: 220
		public DirectorControlPlayable.PauseAction pauseAction;

		// Token: 0x040000DD RID: 221
		private bool m_SyncTime;

		// Token: 0x040000DE RID: 222
		private double m_AssetDuration = double.MaxValue;

		// Token: 0x0200007B RID: 123
		public enum PauseAction
		{
			// Token: 0x04000189 RID: 393
			StopDirector,
			// Token: 0x0400018A RID: 394
			PauseDirector
		}
	}
}
