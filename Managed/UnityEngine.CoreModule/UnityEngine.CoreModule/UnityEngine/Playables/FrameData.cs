using System;

namespace UnityEngine.Playables
{
	// Token: 0x02000441 RID: 1089
	public struct FrameData
	{
		// Token: 0x06002A11 RID: 10769 RVA: 0x000510D0 File Offset: 0x0004F2D0
		private bool HasFlags(FrameData.Flags flag)
		{
			return (this.m_Flags & flag) == flag;
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06002A12 RID: 10770 RVA: 0x000510F0 File Offset: 0x0004F2F0
		public ulong frameId
		{
			get
			{
				return this.m_FrameID;
			}
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06002A13 RID: 10771 RVA: 0x00051108 File Offset: 0x0004F308
		public float deltaTime
		{
			get
			{
				return (float)this.m_DeltaTime;
			}
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06002A14 RID: 10772 RVA: 0x00051124 File Offset: 0x0004F324
		public float weight
		{
			get
			{
				return this.m_Weight;
			}
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06002A15 RID: 10773 RVA: 0x0005113C File Offset: 0x0004F33C
		public float effectiveWeight
		{
			get
			{
				return this.m_EffectiveWeight;
			}
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x06002A16 RID: 10774 RVA: 0x00051154 File Offset: 0x0004F354
		[Obsolete("effectiveParentDelay is obsolete; use a custom ScriptPlayable to implement this feature", false)]
		public double effectiveParentDelay
		{
			get
			{
				return this.m_EffectiveParentDelay;
			}
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x06002A17 RID: 10775 RVA: 0x0005116C File Offset: 0x0004F36C
		public float effectiveParentSpeed
		{
			get
			{
				return this.m_EffectiveParentSpeed;
			}
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06002A18 RID: 10776 RVA: 0x00051184 File Offset: 0x0004F384
		public float effectiveSpeed
		{
			get
			{
				return this.m_EffectiveSpeed;
			}
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06002A19 RID: 10777 RVA: 0x0005119C File Offset: 0x0004F39C
		public FrameData.EvaluationType evaluationType
		{
			get
			{
				return this.HasFlags(FrameData.Flags.Evaluate) ? FrameData.EvaluationType.Evaluate : FrameData.EvaluationType.Playback;
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06002A1A RID: 10778 RVA: 0x000511BC File Offset: 0x0004F3BC
		public bool seekOccurred
		{
			get
			{
				return this.HasFlags(FrameData.Flags.SeekOccured);
			}
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x06002A1B RID: 10779 RVA: 0x000511D8 File Offset: 0x0004F3D8
		public bool timeLooped
		{
			get
			{
				return this.HasFlags(FrameData.Flags.Loop);
			}
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x06002A1C RID: 10780 RVA: 0x000511F4 File Offset: 0x0004F3F4
		public bool timeHeld
		{
			get
			{
				return this.HasFlags(FrameData.Flags.Hold);
			}
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x06002A1D RID: 10781 RVA: 0x00051210 File Offset: 0x0004F410
		public PlayableOutput output
		{
			get
			{
				return this.m_Output;
			}
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x06002A1E RID: 10782 RVA: 0x00051228 File Offset: 0x0004F428
		public PlayState effectivePlayState
		{
			get
			{
				bool flag = this.HasFlags(FrameData.Flags.EffectivePlayStateDelayed);
				PlayState playState;
				if (flag)
				{
					playState = PlayState.Delayed;
				}
				else
				{
					bool flag2 = this.HasFlags(FrameData.Flags.EffectivePlayStatePlaying);
					if (flag2)
					{
						playState = PlayState.Playing;
					}
					else
					{
						playState = PlayState.Paused;
					}
				}
				return playState;
			}
		}

		// Token: 0x04000BF9 RID: 3065
		internal ulong m_FrameID;

		// Token: 0x04000BFA RID: 3066
		internal double m_DeltaTime;

		// Token: 0x04000BFB RID: 3067
		internal float m_Weight;

		// Token: 0x04000BFC RID: 3068
		internal float m_EffectiveWeight;

		// Token: 0x04000BFD RID: 3069
		internal double m_EffectiveParentDelay;

		// Token: 0x04000BFE RID: 3070
		internal float m_EffectiveParentSpeed;

		// Token: 0x04000BFF RID: 3071
		internal float m_EffectiveSpeed;

		// Token: 0x04000C00 RID: 3072
		internal FrameData.Flags m_Flags;

		// Token: 0x04000C01 RID: 3073
		internal PlayableOutput m_Output;

		// Token: 0x02000442 RID: 1090
		[Flags]
		internal enum Flags
		{
			// Token: 0x04000C03 RID: 3075
			Evaluate = 1,
			// Token: 0x04000C04 RID: 3076
			SeekOccured = 2,
			// Token: 0x04000C05 RID: 3077
			Loop = 4,
			// Token: 0x04000C06 RID: 3078
			Hold = 8,
			// Token: 0x04000C07 RID: 3079
			EffectivePlayStateDelayed = 16,
			// Token: 0x04000C08 RID: 3080
			EffectivePlayStatePlaying = 32
		}

		// Token: 0x02000443 RID: 1091
		public enum EvaluationType
		{
			// Token: 0x04000C0A RID: 3082
			Evaluate,
			// Token: 0x04000C0B RID: 3083
			Playback
		}
	}
}
