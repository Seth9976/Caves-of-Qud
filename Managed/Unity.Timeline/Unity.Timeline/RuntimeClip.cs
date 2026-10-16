using System;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x02000022 RID: 34
	internal class RuntimeClip : RuntimeClipBase
	{
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000204 RID: 516 RVA: 0x00007FB0 File Offset: 0x000061B0
		public override double start
		{
			get
			{
				return this.m_Clip.extrapolatedStart;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000205 RID: 517 RVA: 0x00007FBD File Offset: 0x000061BD
		public override double duration
		{
			get
			{
				return this.m_Clip.extrapolatedDuration;
			}
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00007FCA File Offset: 0x000061CA
		public RuntimeClip(TimelineClip clip, Playable clipPlayable, Playable parentMixer)
		{
			this.Create(clip, clipPlayable, parentMixer);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00007FDB File Offset: 0x000061DB
		private void Create(TimelineClip clip, Playable clipPlayable, Playable parentMixer)
		{
			this.m_Clip = clip;
			this.m_Playable = clipPlayable;
			this.m_ParentMixer = parentMixer;
			clipPlayable.Pause<Playable>();
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000208 RID: 520 RVA: 0x00007FF8 File Offset: 0x000061F8
		public TimelineClip clip
		{
			get
			{
				return this.m_Clip;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000209 RID: 521 RVA: 0x00008000 File Offset: 0x00006200
		public Playable mixer
		{
			get
			{
				return this.m_ParentMixer;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600020A RID: 522 RVA: 0x00008008 File Offset: 0x00006208
		public Playable playable
		{
			get
			{
				return this.m_Playable;
			}
		}

		// Token: 0x17000097 RID: 151
		// (set) Token: 0x0600020B RID: 523 RVA: 0x00008010 File Offset: 0x00006210
		public override bool enable
		{
			set
			{
				if (value && this.m_Playable.GetPlayState<Playable>() != PlayState.Playing)
				{
					this.m_Playable.Play<Playable>();
					this.SetTime(this.m_Clip.clipIn);
					return;
				}
				if (!value && this.m_Playable.GetPlayState<Playable>() != PlayState.Paused)
				{
					this.m_Playable.Pause<Playable>();
					if (this.m_ParentMixer.IsValid<Playable>())
					{
						this.m_ParentMixer.SetInputWeight(this.m_Playable, 0f);
					}
				}
			}
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00008089 File Offset: 0x00006289
		public void SetTime(double time)
		{
			this.m_Playable.SetTime(time);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00008097 File Offset: 0x00006297
		public void SetDuration(double duration)
		{
			this.m_Playable.SetDuration(duration);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x000080A8 File Offset: 0x000062A8
		public override void EvaluateAt(double localTime, FrameData frameData)
		{
			this.enable = true;
			if (frameData.timeLooped)
			{
				this.SetTime(this.clip.clipIn);
				this.SetTime(this.clip.clipIn);
			}
			float num;
			if (this.clip.IsPreExtrapolatedTime(localTime))
			{
				num = this.clip.EvaluateMixIn((double)((float)this.clip.start));
			}
			else if (this.clip.IsPostExtrapolatedTime(localTime))
			{
				num = this.clip.EvaluateMixOut((double)((float)this.clip.end));
			}
			else
			{
				num = this.clip.EvaluateMixIn(localTime) * this.clip.EvaluateMixOut(localTime);
			}
			if (this.mixer.IsValid<Playable>())
			{
				this.mixer.SetInputWeight(this.playable, num);
			}
			double num2 = this.clip.ToLocalTime(localTime);
			if (num2 >= -DiscreteTime.tickValue / 2.0)
			{
				this.SetTime(num2);
			}
			this.SetDuration(this.clip.extrapolatedDuration);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x000081B0 File Offset: 0x000063B0
		public override void DisableAt(double localTime, double rootDuration, FrameData frameData)
		{
			double num = Math.Min(localTime, (double)DiscreteTime.FromTicks(this.intervalEnd));
			if (frameData.timeLooped)
			{
				num = Math.Min(num, rootDuration);
			}
			double num2 = this.clip.ToLocalTime(num);
			if (num2 > -DiscreteTime.tickValue / 2.0)
			{
				this.SetTime(num2);
			}
			this.enable = false;
		}

		// Token: 0x040000BE RID: 190
		private TimelineClip m_Clip;

		// Token: 0x040000BF RID: 191
		private Playable m_Playable;

		// Token: 0x040000C0 RID: 192
		private Playable m_ParentMixer;
	}
}
