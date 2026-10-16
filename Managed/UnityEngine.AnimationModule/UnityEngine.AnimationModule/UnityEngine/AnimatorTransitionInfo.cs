using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000023 RID: 35
	[NativeHeader("Modules/Animation/AnimatorInfo.h")]
	[RequiredByNativeCode]
	public struct AnimatorTransitionInfo
	{
		// Token: 0x06000117 RID: 279 RVA: 0x00003F24 File Offset: 0x00002124
		public bool IsName(string name)
		{
			return Animator.StringToHash(name) == this.m_Name || Animator.StringToHash(name) == this.m_FullPath;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00003F58 File Offset: 0x00002158
		public bool IsUserName(string name)
		{
			return Animator.StringToHash(name) == this.m_UserName;
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000119 RID: 281 RVA: 0x00003F78 File Offset: 0x00002178
		public int fullPathHash
		{
			get
			{
				return this.m_FullPath;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00003F90 File Offset: 0x00002190
		public int nameHash
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00003FA8 File Offset: 0x000021A8
		public int userNameHash
		{
			get
			{
				return this.m_UserName;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00003FC0 File Offset: 0x000021C0
		public DurationUnit durationUnit
		{
			get
			{
				return this.m_HasFixedDuration ? DurationUnit.Fixed : DurationUnit.Normalized;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00003FE0 File Offset: 0x000021E0
		public float duration
		{
			get
			{
				return this.m_Duration;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00003FF8 File Offset: 0x000021F8
		public float normalizedTime
		{
			get
			{
				return this.m_NormalizedTime;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00004010 File Offset: 0x00002210
		public bool anyState
		{
			get
			{
				return this.m_AnyState;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00004028 File Offset: 0x00002228
		internal bool entry
		{
			get
			{
				return (this.m_TransitionType & 2) != 0;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00004048 File Offset: 0x00002248
		internal bool exit
		{
			get
			{
				return (this.m_TransitionType & 4) != 0;
			}
		}

		// Token: 0x0400006F RID: 111
		[NativeName("fullPathHash")]
		private int m_FullPath;

		// Token: 0x04000070 RID: 112
		[NativeName("userNameHash")]
		private int m_UserName;

		// Token: 0x04000071 RID: 113
		[NativeName("nameHash")]
		private int m_Name;

		// Token: 0x04000072 RID: 114
		[NativeName("hasFixedDuration")]
		private bool m_HasFixedDuration;

		// Token: 0x04000073 RID: 115
		[NativeName("duration")]
		private float m_Duration;

		// Token: 0x04000074 RID: 116
		[NativeName("normalizedTime")]
		private float m_NormalizedTime;

		// Token: 0x04000075 RID: 117
		[NativeName("anyState")]
		private bool m_AnyState;

		// Token: 0x04000076 RID: 118
		[NativeName("transitionType")]
		private int m_TransitionType;
	}
}
